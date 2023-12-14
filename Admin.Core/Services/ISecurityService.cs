using Admin.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Services
{
    public interface ISecurityService
    {
        Task<bool> CreateRoleAsync(IdentityRole role);
        Task<List<ApplicationRole>> GetRolesAsync();
        Task<List<Users>> GetUsersAsync();
        Task<bool> RegisterUserAsync(RegisterUser register);
        Task<bool> AssignRoleToUserAsync(UserRole user);
        Task<AuthStatus> AuthUserAsync(LoginUser inputModel);
        Task<string> GetUserFromTokenAsync(string token);
        string GetRoleFormToken(string token);
    }
}
