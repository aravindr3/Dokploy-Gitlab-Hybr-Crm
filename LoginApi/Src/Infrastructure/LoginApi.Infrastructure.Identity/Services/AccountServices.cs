using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LoginApi.Application.Interfaces;
using LoginApi.Application.Interfaces.UserInterfaces;
using LoginApi.Infrastructure.Identity.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using HyBrForex.Application.Helpers;
using LoginApi.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Security.Cryptography;
using HyBrForex.Infrastructure.Identity.Settings;
using HyBrForex.Application.DTOs.Account.Requests;
using HyBrForex.Infrastructure.Identity.Models;
using HyBrForex.Application.DTOs.Account.Responses;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNetCore.Routing.Template;
using System.Collections.Generic;
using HyBrForex.Application.Wrappers;
using HyBrForex.Application.Interfaces;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Infrastructure.Identity.Services;

namespace LoginApi.Infrastructure.Identity.Services
{
    public class AccountServices(UserManager<ApplicationUser> userManager,IVerticalServices verticalServices, IAuthenticatedUserService authenticatedUser,IDomainRepository domainRepository, SignInManager<ApplicationUser> signInManager, JwtSettings jwtSettings, ITranslator translator, SMTPSettings smtp, IdentityContext identityContext, EncryptionSettings encryptionSettings, IUnitOfWork unitOfWork) : IAccountServices
    {
        public async Task<BaseResult> ChangePassword(ChangePasswordRequest model)
        {

            //var user = await userManager.FindByIdAsync(authenticatedUser.UserId);
            var user = FindByid(authenticatedUser.UserId);
            //  user = await userManager.FindByEmailAsync(user.Email);
            var token = await userManager.GeneratePasswordResetTokenAsync(user!);
            var identityResult = await userManager.ResetPasswordAsync(user, token, model.Password);

            if (identityResult.Succeeded)
                return BaseResult.Ok();

            return identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)).ToList();
        }

        public async Task<BaseResult> ChangeEmail(ChangeEmailRequest model)
        {
            //var user = await userManager.FindByIdAsync(authenticatedUser.UserId);
            var user = FindByid(authenticatedUser.UserId);
            //user = await userManager.FindByEmailAsync(user.Email);
            user.Email = model.Email;

            var identityResult = await userManager.UpdateAsync(user);

            if (identityResult.Succeeded)
                return BaseResult.Ok();

            return identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)).ToList();
        }

        public async Task<BaseResult<string>> EnableTwoFactorAuthentication(String Id)
        {
            var user = FindByid(Id);
            //  user = await userManager.FindByEmailAsync(user.Email);
            if (user == null)
                return new Error(ErrorCode.NotFound, translator.GetString(TranslatorMessages.AccountMessages.Account_NotFound_with_UserName(user.UserName)), nameof(user.UserName));
            string secretkey = TotpGenerator.GenerateSecretKey();
            user.TwoFactorSecretKey = secretkey;
            user.ConcurrencyStamp = Guid.NewGuid().ToString();
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return result.Errors.Select(e => new Error(ErrorCode.ErrorInIdentity, e.Description)).ToList();
            string qrCodeUrl = TotpGenerator.GenerateQrCodeUrl("appname", user.Email, secretkey);
            return qrCodeUrl;
        }
        public async Task<BaseResult<AuthenticationResponse>> VerifyTwoFactorCode(string Id, string otpcode)
        {
            var user = FindByid(Id);
            //user = await userManager.FindByEmailAsync(user.Email);
            if (user == null)
                return new Error(ErrorCode.NotFound, "User Not Found");
            if (!TotpGenerator.ValidateCode(user.TwoFactorSecretKey, otpcode))
                return new Error(ErrorCode.FieldDataInvalid, translator.GetString(TranslatorMessages.AccountMessages.Invalid_Key()), nameof(user.TwoFactorSecretKey));

            //return new Error(ErrorCode.FieldDataInvalid, "Invalid key");


            // Let GetAuthenticationResponse generate the refresh token
            return await GetAuthenticationResponse(user);
        }

        public async Task<BaseResult<AuthenticationResponse>> VerifyTwoFactorCodewithconcurrent(string userId, string otpCode, bool forceLogin)
        {
            // Locate the user
             
            
            
            var user = FindByid(userId);
            if (user == null)
                return new Error(ErrorCode.NotFound, "User Not Found");

            // Validate the OTP code
            if (!TotpGenerator.ValidateCode(user.TwoFactorSecretKey, otpCode))
                return new Error(ErrorCode.FieldDataInvalid, translator.GetString(TranslatorMessages.AccountMessages.Invalid_Key()), nameof(user.TwoFactorSecretKey));



            // Generate new authentication response (JWT token and refresh token).
            var authResponse = await GetAuthenticationResponse(user);
            var newExpiryTime = DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes);

            // Fetch all active sessions for the user.
            var activeSessions = await identityContext.ApplicationLoginInfo
                .Where(s => s.UserId == user.Id && s.IsActive)
                .ToListAsync();

            if (activeSessions.Any())
            {
                if (forceLogin)
                {
                    // Force login: revoke (terminate) all active sessions.
                    await TerminateOtherSessions(user.Id);
                }
                else
                {
                    // Check if any active session's token is still valid.
                    bool validSessionFound = false;
                    foreach (var session in activeSessions)
                    {
                        if (await ValidateJwtTokenConcurrent(session.SessionToken))
                        {
                            validSessionFound = true;
                            break;
                        }
                    }

                    if (validSessionFound)
                    {
                        // At least one active session has a valid token.
                        return new Error(ErrorCode.AlreadyExists, "CONCURRENT_USER_DETECTED");
                    }
                    else
                    {
                        // All tokens are expired; revoke all active sessions.
                        await TerminateOtherSessions(user.Id);
                    }
                }
            }

            // Create a new session entry.
            var newSession = new ApplicationLoginInfo
            {
                UserId = user.Id,
                SessionToken = authResponse.JwToken,
                RefreshToken = authResponse.RefreshToken,
                LoginTime = DateTime.UtcNow,
                LastActivity = DateTime.UtcNow,
                ExpiryTime = newExpiryTime,
                IsActive = true,
                IsRevoked = false
            };

            identityContext.ApplicationLoginInfo.Add(newSession);
            await identityContext.SaveChangesAsync();

            return authResponse;
        }
        public async Task<bool> ValidateJwtTokenConcurrent(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(jwtSettings.Key);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                // Additionally, you may check the expiration explicitly.
                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                // Token validation failed
                return false;
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> ValidateJwtToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                // Ensure the token can be read.
                if (!tokenHandler.CanReadToken(token))
                {
                    return false;
                }

                // Read the token without validating it first to extract userId.
                var jwtToken = tokenHandler.ReadJwtToken(token);

                // Extract the userId claim from the token.
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "userId" || c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return false;
                }

                var userId = userIdClaim.Value;

                // Fetch the user from the database.
                var user = FindByid(userId);
                if (user == null)
                {
                    return false;
                }

                // Fetch all active sessions for the user.
                var activeSessions = await identityContext.ApplicationLoginInfo
                        .Where(s => s.UserId == user.Id && s.IsActive && s.SessionToken == token)
                         .ToListAsync();

                if (activeSessions == null || !activeSessions.Any())
                {
                    return false;
                }

                // Validate the token s signature and expiration.
                var key = Encoding.UTF8.GetBytes(jwtSettings.Key);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var validJwtToken = validatedToken as JwtSecurityToken;
                return validJwtToken?.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckConcurrentUser(string userId)
        {
            var activeSession = await GetActiveSession(userId);
            return activeSession != null;
        }
        private async Task<ApplicationLoginInfo> GetActiveSession(string Id)
        {
            return await identityContext.ApplicationLoginInfo
                .Where(s => s.UserId == Id && s.IsActive)
                .OrderByDescending(s => s.LoginTime)  // Get the latest session
                .FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateUserSession(UserSessionRequest sessionRequest)
        {
            var sessionEntity = new ApplicationLoginInfo
            {
                UserId = sessionRequest.Id,
                SessionToken = sessionRequest.SessionToken,
                RefreshToken = sessionRequest.RefreshToken,
                DeviceInfo = sessionRequest.DeviceInfo,
                IpAddress = sessionRequest.IpAddress,
                LoginTime = sessionRequest.LoginTime,
                LastActivity = sessionRequest.LastActivity,
                ExpiryTime = sessionRequest.ExpiryTime,
                IsActive = sessionRequest.IsActive,
                IsRevoked = sessionRequest.IsRevoked
            };

            identityContext.Update<ApplicationLoginInfo>(sessionEntity);
            return await unitOfWork.SaveChangesAsync();
        }


        public async Task<bool> TerminateOtherSessions(string userId)
        {
            return await RevokeAllSessions(userId);
        }
        public async Task<AuthorizeResponse> AuthorizeUserAsync(AuthorizeRequest request)
        {
            var userSession = await identityContext.ApplicationLoginInfo
                .FirstOrDefaultAsync(s => s.UserId == request.UserId && s.IsActive);

            if (userSession == null)
            {
                return new AuthorizeResponse
                {
                    IsAuthorized = false,
                    Message = "No active session found."
                };
            }

            // Validate JWT Token
            bool isValidToken = await ValidateJwtTokenConcurrent(request.Token);
            if (!isValidToken)
            {
                return new AuthorizeResponse
                {
                    IsAuthorized = false,
                    Message = "Invalid or expired token."
                };
            }

            return new AuthorizeResponse
            {
                IsAuthorized = true,
                Message = "User is authorized."
            };
        }


        private async Task<bool> RevokeAllSessions(string userId)
        {
            // Fetch all active sessions for the user
            var sessions = await identityContext.ApplicationLoginInfo
                .Where(s => s.UserId == userId && s.IsActive)
                .ToListAsync();

            if (sessions.Any())
            {
                foreach (var session in sessions)
                {
                    session.IsActive = false;
                    session.IsRevoked = true;
                }
                identityContext.ApplicationLoginInfo.UpdateRange(sessions);
                await identityContext.SaveChangesAsync();
                return true;
            }

            return false; // No active sessions found
        }

        public async Task<bool> DeactivateSessionToken(string token)
        {
            var session = await identityContext.ApplicationLoginInfo
                .FirstOrDefaultAsync(s => s.SessionToken == token && s.IsActive);
            if (session != null)
            {
                session.IsActive = false;
                session.IsRevoked = true;
                identityContext.ApplicationLoginInfo.Update(session);
                await identityContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<BaseResult<HyBrForex.Application.DTOs.Account.Responses.TwoFactorResponse>> Authenticate(AuthenticationRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new Error(ErrorCode.NotFound, translator.GetString(TranslatorMessages.AccountMessages.Account_NotFound_with_UserName(request.Password)), nameof(request.Email));
            }

            // Decrypt the received password
            string decryptedPassword = DecryptPassword(request.Password);

            var signInResult = await signInManager.CheckPasswordSignInAsync(user, decryptedPassword, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
            {
                return new Error(ErrorCode.FieldDataInvalid, translator.GetString(TranslatorMessages.AccountMessages.Invalid_password()), nameof(request.Password));
            }

            var _data = new HyBrForex.Application.DTOs.Account.Responses.TwoFactorResponse
            {
                RequiresTwoFactor = user.IsTwoFactorEnabled,
                Email = request.Email,
                Id = user.Id
            };

            return new BaseResult<HyBrForex.Application.DTOs.Account.Responses.TwoFactorResponse> { Data = _data, Success = true };
        }
        public async Task<BaseResult<AuthenticationResponse>> AuthenticateByEmail(string Email)
        {
            var user = await userManager.FindByNameAsync(Email);
            if (user == null)
            {
                return new Error(ErrorCode.NotFound, translator.GetString(TranslatorMessages.AccountMessages.Account_NotFound_with_UserName(Email)), nameof(Email));
            }

            return await GetAuthenticationResponse(user);
        }

        public async Task<BaseResult<string>> RegisterGhostAccount()
        {
            var user = new ApplicationUser()
            {
                UserName = GenerateRandomString(7)
            };

            var identityResult = await userManager.CreateAsync(user);

            if (identityResult.Succeeded)
                return user.UserName;

            return identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)).ToList();

            string GenerateRandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                var random = new Random();
                return new string(Enumerable.Repeat(chars, length)
                        .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }

        public async Task<BaseResult> UpdateTwoFactorStatus(string userId, bool isEnabled)
        {
            var user = FindByid(userId);
            //user = await userManager.FindByEmailAsync(user.Email);
            //var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return new Error(ErrorCode.NotFound, "User not found.", nameof(userId));
            user.TwoFactorEnabled = isEnabled;

            user.IsTwoFactorEnabled = isEnabled;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error Code: {error.Code}, Description: {error.Description}");
                }
                return result.Errors.Select(e => new Error(ErrorCode.ErrorInIdentity, e.Description)).ToList();
            }

            return BaseResult.Ok();
        }

        public async Task<BaseResult> DeActivateTwoFactorStatus(string userId)
        {
            var user = FindByid(userId);
            //user = await userManager.FindByEmailAsync(user.Email);
            // var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return new Error(ErrorCode.NotFound, "User not found.", nameof(userId));
            user.TwoFactorEnabled = false;

            user.IsTwoFactorEnabled = false;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error Code: {error.Code}, Description: {error.Description}");
                }
                return result.Errors.Select(e => new Error(ErrorCode.ErrorInIdentity, e.Description)).ToList();
            }

            return BaseResult.Ok();
        }


        private async Task<AuthenticationResponse> GetAuthenticationResponse(ApplicationUser user)
        {
            // Update the security stamp before processing.
            await userManager.UpdateSecurityStampAsync(user);

            // Fetch roles (as names) using userManager.
            var rolesList = await userManager.GetRolesAsync(user);

            // Query the UserRole table to fetch all RoleIds for this user.
            var userRoles = await identityContext.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .ToListAsync();
            var roleIds = userRoles.Select(ur => ur.RoleId).ToList();

            // Fetch branches associated with the user.
            var userBranches = await identityContext.BranchUser
                            .Where(bu => bu.UserId == user.Id)
                            .Select(bu => bu.BranchId) // Ensuring we only select the BranchId
                            .ToListAsync();


            // If no roles are found, return an error response.
            if (roleIds == null || !roleIds.Any())
            {
                throw new Exception("User does not have any roles assigned. Please contact support.");
            }

            // Generate the JWT token.
            var jwToken = await GenerateJwtToken();

            // Generate Refresh Token.
            var refreshToken = TokenHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(jwtSettings.RefreshTokenDurationInMinutes);
            await userManager.UpdateAsync(user);

            // Build and return the successful response.
            return new AuthenticationResponse()
            {
                Id = user.Id.ToString(),
                JwToken = new JwtSecurityTokenHandler().WriteToken(jwToken),
                Email = user.Email,
                UserName = user.UserName,
                Roles = roleIds,  // New property for multiple role IDs
                IsVerified = user.EmailConfirmed,
                RefreshToken = refreshToken,
                VerticalId = user.VerticalId.ToString(),
                VerticalName = verticalServices.GetVerticalById(user.VerticalId)?.Result?.Data?.VerticalName,
                DomainId = domainRepository.GetDomainById((verticalServices.GetVerticalById(user.VerticalId).Result?.Data?.DomainId))?.Result?.Data?.Id,
                DomainName = domainRepository.GetDomainById((verticalServices.GetVerticalById(user.VerticalId).Result?.Data?.DomainId))?.Result?.Data?.DomainName,
                Branches = userBranches  // Add branches here
            };

            // Local function to generate the JWT token.
            async Task<JwtSecurityToken> GenerateJwtToken()
            {
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                return new JwtSecurityToken(
                    issuer: jwtSettings.Issuer,
                    audience: jwtSettings.Audience,
                    claims: (await signInManager.ClaimsFactory.CreateAsync(user)).Claims,
                    expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials);
            }
        }

        public async Task<BaseResult<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest request)
        {
            var user = userManager.Users.FirstOrDefault(u => u.RefreshToken == request.RefreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new BaseResult<RefreshTokenResponse>
                {
                    Success = false,

                };
            }

            var newAccessToken = await GenerateJwtToken(user);
            var newRefreshToken = TokenHelper.GenerateRefreshToken();
            var newExpiryTime = DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes);
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(jwtSettings.RefreshTokenDurationInMinutes);
            await userManager.UpdateAsync(user);
            var activeSessions = await identityContext.ApplicationLoginInfo.FirstOrDefaultAsync
               (s => s.UserId == user.Id && s.IsActive);
               
            if (activeSessions != null)
            {
                activeSessions.SessionToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
                activeSessions.RefreshToken = newRefreshToken;
                activeSessions.ExpiryTime = newExpiryTime;

                identityContext.ApplicationLoginInfo.Update(activeSessions);
                await identityContext.SaveChangesAsync();
            }
            return new BaseResult<RefreshTokenResponse>
            {
                Success = true,
                Data = new RefreshTokenResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    RefreshToken = newRefreshToken
                }
            };
        }

        private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: (await signInManager.ClaimsFactory.CreateAsync(user)).Claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
        }


        public ApplicationUser FindByid(string id)
        {
            ApplicationUser user = new ApplicationUser();
            try
            {

                user = identityContext.Users.ToList().Where(u => Ulid.Parse(u.Id) == Ulid.Parse(id)).FirstOrDefault();

                //if (existingUser != null)
                //{
                //    identityContext.Entry(existingUser).State = EntityState.Detached;
                //}
                //identityContext.Update(user);
                //await identityContext.SaveChangesAsync();




                //var _user = await identityContext.Users.Select(a => new UserDto
                //{
                //    Id = a.Id.ToString(),
                //    UserName = a.UserName,
                //    Email = a.Email,
                //    Name = a.Name,
                //    Created = a.Created,
                //    PhoneNumber = a.PhoneNumber,
                //    AccessFailedCount = a.AccessFailedCount,
                //    ConcurrencyStamp = a.ConcurrencyStamp,
                //    EmailConfirmed = a.EmailConfirmed,
                //    LockoutEnabled = a.LockoutEnabled,
                //    LockoutEnd = a.LockoutEnd,
                //    NormalizedEmail = a.NormalizedEmail,
                //    NormalizedUserName = a.NormalizedUserName,
                //    PasswordHash = a.PasswordHash,
                //    PhoneNumberConfirmed = a.PhoneNumberConfirmed,
                //    SecurityStamp = a.SecurityStamp,
                //    TwoFactorEnabled = a.TwoFactorEnabled

                //}).ToListAsync();

                //if (_user.Count > 0)
                //{
                //    var users = _user.Where(a => a.Id == id).ToList();

                //    if (users.Count > 0)
                //    {
                //        user = new ApplicationUser()
                //        {
                //            Id = (users[0].Id),
                //            UserName = users[0].UserName,
                //            Email = users[0].Email,
                //            Name = users[0].Name,
                //            Created = users[0].Created,
                //            PhoneNumber = users[0].PhoneNumber,
                //            AccessFailedCount = users[0].AccessFailedCount,
                //            ConcurrencyStamp = users[0].ConcurrencyStamp,
                //            EmailConfirmed = users[0].EmailConfirmed,
                //            LockoutEnabled = users[0].LockoutEnabled,
                //            LockoutEnd = users[0].LockoutEnd,
                //            NormalizedEmail = users[0].NormalizedEmail,
                //            NormalizedUserName = users[0].NormalizedUserName,
                //            PasswordHash = users[0].PasswordHash,
                //            PhoneNumberConfirmed = users[0].PhoneNumberConfirmed,
                //            SecurityStamp = users[0].SecurityStamp,
                //            TwoFactorEnabled = users[0].TwoFactorEnabled
                //        };

                //    }
                //}

            }
            catch (Exception ex)
            {


            }


            return user;
        }

        public async Task<BaseResult> SendResetPasswordLink(PasswordResetRequest request, string emailTemplatePath)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new BaseResult { Success = false };
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var ResetPasswordUrl = smtp.ResetPasswordUrl;
            if (string.IsNullOrEmpty(ResetPasswordUrl))
            {
                return new BaseResult { Success = false };
            }

            var expirationTime = DateTime.UtcNow.AddMinutes(5);
            var resetLink = $"{ResetPasswordUrl}/ResetPassword?email={WebUtility.UrlEncode(user.Email)}&token={WebUtility.UrlEncode(token)}&expires={WebUtility.UrlEncode(expirationTime.ToString("o"))}";

            if (!File.Exists(emailTemplatePath))
            {
                return new BaseResult { Success = false };
            }

            string emailBody = await File.ReadAllTextAsync(emailTemplatePath);
            emailBody = emailBody.Replace("Reset_Link", resetLink);

            // Determine email subject based on template name
            string subject = emailTemplatePath.Contains("ResetPasswordTemplate.html") ? "Password Reset" :
                             emailTemplatePath.Contains("WelcomeTemplate.html") ? "Welcome on Board" : "Notification";

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(smtp.Email),
                Subject = subject,
                Body = emailBody,
                IsBodyHtml = true
            };
            mailMessage.To.Add(request.Email);

            using var smtpClient = new SmtpClient
            {
                Host = smtp.Host,
                Port = int.Parse(smtp.Port),
                EnableSsl = bool.Parse(smtp.EnableSsl),
                Credentials = new NetworkCredential(smtp.Email, smtp.Password)
            };

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return new BaseResult { Success = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                return new BaseResult { Success = false };
            }
        }


        public async Task<BaseResult> ValidateResetToken(ValidateResetTokenRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new Error(ErrorCode.NotFound, "User not found.", nameof(request.Email));
            }

            if (DateTime.TryParse(request.ResetPasswordExpirationTime, out var expirationTime))
            {
                if (DateTime.Now > expirationTime)
                {
                    return new Error(ErrorCode.AlreadyExists, "The reset link has expired. Please request a new one.", "Expired Link");
                }
            }
            else
            {
                return new Error(ErrorCode.BadRequest, "Invalid expiration time format.", "Invalid Format");
            }

            var isValid = await userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "ResetPassword", request.Token);
            if (!isValid)
            {
                return new Error(ErrorCode.BadRequest, "Invalid or expired token.", "Invalid Token");
            }

            return BaseResult.Ok();
        }
        public async Task<BaseResult> ResetPassword(HyBrForex.Application.DTOs.Account.Requests.ResetPasswordRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new Error(ErrorCode.NotFound, "User not found.", nameof(request.Email));
            }
            string decryptedPassword = DecryptPassword(request.NewPassword);

            var result = await userManager.ResetPasswordAsync(user, request.Token, decryptedPassword);
            if (result.Succeeded)
            {
                return BaseResult.Ok();
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new Error(ErrorCode.BadRequest, "Password reset failed.", errors);
        }
        private string DecryptPassword(string encryptedPassword)
        {
            // Hex string (from the frontend)
            string keyString = encryptionSettings.SecretKey;

            // Convert Hex string to byte array
            byte[] keyBytes = ConvertHexStringToByteArray(keyString);

            // Decode Base64 encrypted string
            byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.Mode = CipherMode.ECB;  // Match JavaScript mode
                    aes.Padding = PaddingMode.PKCS7; // Match JavaScript padding

                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("Error during decryption: " + ex.Message);
                throw new Exception("Error during password decryption", ex);
            }
        }

        private byte[] ConvertHexStringToByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
        public async Task<List<GetBranchByIdResponse>> GetBranchNamesAsync(List<string> branchIds)
        {
            return await identityContext.BranchMaster
                .Where(b => branchIds.Contains(b.Id))
                .Select(b => new GetBranchByIdResponse
                {
                    BranchId = b.Id,
                    BranchCode = b.BranchCode,
                    BranchName = b.BranchName
                })
                .ToListAsync();
        }

    }
}

