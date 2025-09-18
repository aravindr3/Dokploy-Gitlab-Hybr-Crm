using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Role.Requests;
using HyBrForex.Application.DTOs.Role.Responses;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Infrastructure.Identity.Models;
using LoginApi.Infrastructure.Identity.Contexts;
using LoginApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HyBrForex.Infrastructure.Identity.Services
{
    public class RoleService(IdentityContext identityContext) : IRoleService
    {
        public async Task<BaseResult<RoleResponse>> CreateRoleAsync(RoleRequest request)
        {
            try
            {
                // Check if the role already exists
                var existingRole = await identityContext.ApplicationRoles
                    .FirstOrDefaultAsync(r => r.Name == request.Name);

                if (existingRole != null)
                {
                    return new Error(ErrorCode.NotFound, "Role already exists.");

                }

                // Create new role
                var role = new ApplicationRole(request.Name)
                {
                    Id = Ulid.NewUlid().ToString(),
                    NormalizedName = request.Name.ToUpper()
                };

                identityContext.ApplicationRoles.Add(role);
                await identityContext.SaveChangesAsync();

                return new RoleResponse { Id = role.Id, Name = role.Name! };
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is available)
                return new Error(ErrorCode.NotFound, "Role already exists.");
            }
        }


        public async Task<BaseResult<RoleResponse>> UpdateRoleAsync(string id, RoleRequest request)
        {
            try
            {
                var role = await identityContext.ApplicationRoles.FindAsync(id) ?? throw new KeyNotFoundException("Role not found");
                role.Name = request.Name;
                role.NormalizedName = request.Name.ToUpper();
                identityContext.ApplicationRoles.Update(role);
                await identityContext.SaveChangesAsync();

                return new RoleResponse { Id = role.Id, Name = role.Name! };
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound,"An error occurred while updating the role.");
            }
        }

        public async Task<BaseResult<bool>> DeleteRoleAsync(string id)
        {
            try
            {
                var role = await identityContext.ApplicationRoles.FindAsync(id) ?? throw new KeyNotFoundException("Role not found");
                identityContext.ApplicationRoles.Remove(role);
                await identityContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while deleting the role.");
            }
        }

        public async Task<BaseResult<RoleResponse?>> GetByIdAsync(string id)
        {
            try
            {
                var role = await identityContext.ApplicationRoles.FindAsync(id);
                if (role == null) return null;

                return new RoleResponse
                {
                    Id = role.Id,
                    Name = role.Name ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while fetching the role by ID.");
            }
        }

        public async Task<BaseResult<IEnumerable<RoleResponse>>> GetAllAsync()
        {
            try
            {
                var roles = await identityContext.ApplicationRoles
                    .Select(role => new RoleResponse
                    {
                        Id = role.Id,
                        Name = role.Name ?? string.Empty
                    })
                    .ToListAsync();

                return roles;
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.NotFound, "An error occurred while fetching all roles.");
            }
        }
    }

}
