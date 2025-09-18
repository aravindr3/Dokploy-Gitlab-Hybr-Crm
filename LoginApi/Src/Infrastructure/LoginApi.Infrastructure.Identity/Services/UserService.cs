using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.DTOs.Account.Requests;
using HyBrForex.Application.DTOs.Tenant.Requests;
using HyBrForex.Application.DTOs.User.Requests;
using HyBrForex.Application.DTOs.User.Responses;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Infrastructure.Identity.Models;
using LoginApi.Application.Interfaces.UserInterfaces;
using LoginApi.Infrastructure.Identity.Contexts;
using LoginApi.Infrastructure.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HyBrForex.Infrastructure.Identity.Services
{
    public class UserService(IdentityContext identityContext,IVerticalServices verticalServices,IDomainRepository domainRepository,  UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IAccountServices accountService) : IUserService
    {
        public async Task<BaseResult<UserResponse>> CreateUserAsync(UserRequest request)
        {
            var generatedPassword = GenerateSecurePassword(); // Generate a strong password if not provided

            var user = new ApplicationUser
            {
                VerticalId = request.VerticalId,
                UserName = Regex.Replace(request.Name, @"\s+", ""),
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new Exception("User already exists.");

            var result = await userManager.CreateAsync(user, generatedPassword);
            if (!result.Succeeded)
                throw new Exception("User creation failed.");

            user = await userManager.FindByEmailAsync(request.Email);

            // Assign Multiple Roles
            var roles = await roleManager.Roles.Where(a=> request.Roles.Contains(a.Id)).Select(a=>a.Name).ToListAsync();
            if (roles.Any())
                await userManager.AddToRolesAsync(user, roles);

            // Assign Multiple Branches
            foreach (var branchId in request.BranchIds)
            {
                var branchUser = new BranchUser
                {
                    Id = Ulid.NewUlid().ToString(),
                    BranchId = branchId,
                    UserId = user.Id
                };
                identityContext.BranchUser.Add(branchUser);
            }
            await identityContext.SaveChangesAsync();

            // Automatically Send Reset Password Link
            var passwordResetRequest = new PasswordResetRequest { Email = user.Email };
            var emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "WelcomeTemplate.html");

            await accountService.SendResetPasswordLink(passwordResetRequest, emailTemplatePath);

            return new BaseResult<UserResponse>
            {
                Success = true,
                Data = new UserResponse
                {
                    Id = user.Id,
                    VerticalId = user.VerticalId,
                    UserName = user.UserName,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Roles = request.Roles,
                    BranchIds = request.BranchIds
                }
            };
        }

        public async Task<BaseResult<UserResponse>> UpdateUserAsync(string userId, UserRequest request)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            user.UserName = request.Name;
            user.Email = request.Email;
            user.Name = request.Name;
            user.PhoneNumber = request.PhoneNumber;
            user.VerticalId = request.VerticalId;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception("User update failed.");

            // Update Roles: Remove old roles and add new ones
            var currentRoles = await userManager.GetRolesAsync(user);

            // Get the roles from the request that exist in the system
            var roles = await roleManager.Roles
                .Where(r => request.Roles.Contains(r.Id))
                .Select(r => r.Name)
                .ToListAsync();

            if (roles.Any())
            {
                // Remove the user from all current roles
                await userManager.RemoveFromRolesAsync(user, currentRoles);

                // Add the user to the new roles
                await userManager.AddToRolesAsync(user, roles);
            }

            // Update Branches: Remove old branches and add new ones
            var existingBranches = await identityContext.BranchUser.Where(b => b.UserId == userId).ToListAsync();
            identityContext.BranchUser.RemoveRange(existingBranches);

            foreach (var branchId in request.BranchIds)
            {
                var branchUser = new BranchUser
                {
                    Id = Ulid.NewUlid().ToString(),
                    BranchId = branchId,
                    UserId = user.Id
                };
                identityContext.BranchUser.Add(branchUser);
            }

            await identityContext.SaveChangesAsync();

            return new BaseResult<UserResponse>
            {
                Success = true,
                Data = new UserResponse
                {
                    Id = user.Id,
                    VerticalId = user.VerticalId,
                    VerticalName = verticalServices.GetVerticalById(user?.VerticalId)?.Result?.Data?.VerticalName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Roles = request.Roles,
                    BranchIds = request.BranchIds
                }
            };
        }

        public async Task<BaseResult<bool>> DeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            // Remove from Branches
            var branchUsers = await identityContext.BranchUser.Where(b => b.UserId == userId).ToListAsync();
            identityContext.BranchUser.RemoveRange(branchUsers);
            var roles = await identityContext.UserRoles.Where(a => a.UserId == userId).ToListAsync();
               identityContext.UserRoles.RemoveRange(roles);
            // Remove User
            var result = await userManager.DeleteAsync(user);
            await identityContext.SaveChangesAsync();

            return new BaseResult<bool> { Success = result.Succeeded, Data = result.Succeeded };
        }

        private string GenerateSecurePassword(int length = 12)
        {
            if (length < 8)
                throw new ArgumentException("Password length must be at least 8 characters.");

            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string specialChars = "!@#$%^&*()_-+=";
            const string digits = "1234567890";
            const string allChars = lowerChars + upperChars + specialChars + digits;

            var password = new StringBuilder();
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];

                // Ensure at least one character from each required category
                password.Append(lowerChars[GetRandomIndex(lowerChars.Length, rng)]);
                password.Append(upperChars[GetRandomIndex(upperChars.Length, rng)]);
                password.Append(specialChars[GetRandomIndex(specialChars.Length, rng)]);
                password.Append(digits[GetRandomIndex(digits.Length, rng)]);

                // Fill the rest of the password length with random characters
                rng.GetBytes(randomBytes);
                for (int i = 4; i < length; i++)
                {
                    password.Append(allChars[GetRandomIndex(allChars.Length, rng)]);
                }
            }

            // Shuffle the password to randomize character order
            return new string(password.ToString().ToCharArray().OrderBy(_ => Guid.NewGuid()).ToArray());
        }

        private int GetRandomIndex(int max, RandomNumberGenerator rng)
        {
            byte[] randomByte = new byte[1];
            do
            {
                rng.GetBytes(randomByte);
            }
            while (randomByte[0] >= 256 - (256 % max));

            return randomByte[0] % max;
        }

        public async Task<BaseResult<IEnumerable<GetUserResponse>>> GetAllUsersAsync()
        {
            // Get all users with their roles
            var users = await userManager.Users.ToListAsync();

            var userResponses = new List<GetUserResponse>();

            foreach (var user in users)
            {
                // Fetch roles for the user
                var roles = await userManager.GetRolesAsync(user);

                // Fetch branch names for the user
                var branchNames = await identityContext.BranchUser
                    .Where(bu => bu.UserId == user.Id)
                    .Join(identityContext.BranchMaster, bu => bu.BranchId, b => b.Id,
                        (bu, b) => b.BranchName)
                    .ToListAsync();

                userResponses.Add(new GetUserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    VerticalId = user.VerticalId,
                    VerticalName = verticalServices.GetVerticalById(user.VerticalId)?.Result?.Data?.VerticalName ,
                    DomainId = domainRepository.GetDomainById((verticalServices.GetVerticalById(user.VerticalId).Result?.Data?.DomainId))?.Result?.Data?.Id,
                    DomainName = domainRepository.GetDomainById((verticalServices.GetVerticalById(user.VerticalId).Result?.Data?.DomainId))?.Result?.Data?.DomainName,

                    Roles = roles.ToList(),
                    BranchNames = branchNames.Any() ? branchNames : new List<string> { "N/A" }
                });
            }

            return userResponses;
        }


        public async Task<BaseResult<GetUserResponse>> GetUserByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            // Get roles
            var roles = await userManager.GetRolesAsync(user);

            // Get branch details
            var branchDetails = await identityContext.BranchUser
                .Where(bu => bu.UserId == userId)
                .Join(identityContext.BranchMaster, bu => bu.BranchId, b => b.Id,
                    (bu, b) => new { b.BranchName })
                .ToListAsync();

            var branchNames = branchDetails.Select(b => b.BranchName).ToList();

            return new GetUserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                VerticalId = user.VerticalId,
                VerticalName = verticalServices.GetVerticalById(user.VerticalId)?.Result?.Data?.VerticalName,
                DomainId = domainRepository.GetDomainById((verticalServices.GetVerticalById(user.VerticalId).Result?.Data?.DomainId))?.Result?.Data?.Id,
                DomainName = domainRepository.GetDomainById((verticalServices.GetVerticalById(user.VerticalId).Result?.Data?.DomainId))?.Result?.Data?.DomainName,
                Roles = roles.ToList(),
                BranchNames = branchNames.Count > 0 ? branchNames : new List<string> { "N/A" }
            };
        }

        public async Task<BaseResult<List<GetBranchByTenantResponse>>> GetBranchByTenantAsync(GetBranchByTenantRequest request)
        {
            var result = new BaseResult<List<GetBranchByTenantResponse>>();

            try
            {
                var branches = await identityContext.BranchMaster
                    .Where(b => b.VerticalId == request.VerticalId)
                    .Select(b => new GetBranchByTenantResponse
                    {
                        Id = b.Id,
                        BranchName = b.BranchName
                    })
                    .ToListAsync();

                result.Data = branches ?? new List<GetBranchByTenantResponse>();
                result.Success = true;  // ✅ Explicitly set success to true when there’s no error
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors = new List<Error> { new Error(ErrorCode.NotFound, "An error occurred while retrieving branches.") };
            }

            return result;
        }


    }

}
