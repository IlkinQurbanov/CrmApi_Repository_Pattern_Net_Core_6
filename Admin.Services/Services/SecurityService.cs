﻿using Admin.Core.Models;
using Admin.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Services.Services
{

    public class AuthSecurityService : ISecurityService
    {
        IConfiguration configuration;
        SignInManager<IdentityUser> signInManager;
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        public AuthSecurityService(IConfiguration configuration,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(IdentityRole role)
        {
            bool isRoleCreated = false;
            var res = await roleManager.CreateAsync(role);
            if (res.Succeeded)
            {
                isRoleCreated = true;
            }
            return isRoleCreated;
        }

        public async Task<List<ApplicationRole>> GetRolesAsync()
        {
            List<ApplicationRole> roles = new List<ApplicationRole>();
            roles = (from r in await roleManager.Roles.ToListAsync()
                     select new ApplicationRole()
                     {
                         Name = r.Name,
                         NormalizedName = r.NormalizedName
                     }).ToList();
            return roles;
        }


        //public async Task<List<ApplicationRole>> GetRolesAsync()
        //{
        //    List<ApplicationRole> roles = new List<ApplicationRole>();
        //    roles = (from r in await roleManager.Roles.ToListAsync()
        //             select new ApplicationRole()
        //             {
        //                 Name = r.Name,

        //                 NormalizedName = null
        //             }).ToList();
        //    return roles;
        //}

        public async Task<List<Users>> GetUsersAsync()
        {
            List<Users> users = new List<Users>();
            users = (from u in await userManager.Users.ToListAsync()
                     select new Users()
                     {
                         Email = u.Email,
                         UserName = u.UserName
                     }).ToList();
            return users;
        }
        //public async Task<bool> RegisterUserAsync(RegisterUser register)
        //{
        //    bool IsCreated = false;

        //    var registerUser = new IdentityUser()
        //    {
        //        UserName = register.Email,
        //        Email = register.Email
        //    };

        //    var result = await userManager.CreateAsync(registerUser, register.Password);

        //    if (result.Succeeded)
        //    {
        //        // Assign the user to the specified role
        //        var roleResult = await userManager.AddToRoleAsync(registerUser, register.Role);

        //        // Check if assigning the role was successful
        //        if (roleResult.Succeeded)
        //        {
        //            IsCreated = true;
        //        }
        //    }

        //    return IsCreated;
        //}




        public async Task<bool> RegisterUserAsync(RegisterUser register)
{
    bool IsCreated = false;

    var registerUser = new IdentityUser()
    {
        UserName = register.Email,
        Email = register.Email
    };

    var result = await userManager.CreateAsync(registerUser, register.Password);

    if (result.Succeeded)
    {
        // Assign the user to the specified role
        var roleResult = await userManager.AddToRoleAsync(registerUser, register.Role);

        // Check if assigning the role was successful
        if (roleResult.Succeeded)
        {
            // Add custom logic to associate the department with the user
           // await userManager.AddClaimAsync(registerUser, new Claim("Department", register.Department));

            IsCreated = true;
        }
    }

    return IsCreated;
}


        /// <summary>
        /// Method to Assign Role to User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> AssignRoleToUserAsync(UserRole user)
        {
            bool isRoleAssigned = false;
            // find role associated with the RoleName
            var role = roleManager.FindByNameAsync(user.RoleName).Result;
            // var registeredUser = new IdentityUser() { UserName = user.User.UserName};
            // find user by name
            var registeredUser = await userManager.FindByNameAsync(user.UserName);
            if (role != null)
            {
                var res = await userManager.AddToRoleAsync(registeredUser, role.Name);
                if (res.Succeeded)
                {
                    isRoleAssigned = true;
                }
            }
            return isRoleAssigned;
        }

        /// <summary>
        /// Class to Authenticate User based on User Name
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<AuthStatus> AuthUserAsync(LoginUser inputModel)
        {
            string jwtToken = "";
            LoginStatus loginStatus;
            string roleName = "";
            var result = signInManager.PasswordSignInAsync(inputModel.UserName, inputModel.Password, false, lockoutOnFailure: true).Result;
            if (result.Succeeded)
            {

                // Read the secret key and the expiration from the configuration 
                var secretKey = Convert.FromBase64String(configuration["JWTCoreSettings:SecretKey"]);
                var expiryTimeSpan = Convert.ToInt32(configuration["JWTCoreSettings:ExpiryInMinuts"]);
                // logic to get the user role
                // get the user object based on Email
                // IdentityUser user = new IdentityUser(inputModel.UserName);
                var user = await userManager.FindByEmailAsync(inputModel.UserName);
                var role = await userManager.GetRolesAsync(user);
                // if user is not associated with role then log off
                if (role.Count == 0)
                {
                    await signInManager.SignOutAsync();
                    loginStatus = LoginStatus.NoRoleToUser;
                }
                else
                {
                    //read the rolename
                    roleName = role[0];
                    // set the expity, subject, etc.
                    // note that Issuer and Audience will be null because 
                    // there is no third-party issuer
                    var securityTokenDescription = new SecurityTokenDescriptor()
                    {
                        Issuer = null,
                        Audience = null,
                        Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("userid",user.Id.ToString()),
                        new Claim("role",role[0])
                    }),
                        Expires = DateTime.UtcNow.AddMinutes(expiryTimeSpan),
                        IssuedAt = DateTime.UtcNow,
                        NotBefore = DateTime.UtcNow,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    // Now generate token using JwtSecurityTokenHandler
                    var jwtHandler = new JwtSecurityTokenHandler();
                    var jwToken = jwtHandler.CreateJwtSecurityToken(securityTokenDescription);
                    jwtToken = jwtHandler.WriteToken(jwToken);
                    loginStatus = LoginStatus.LoginSuccessful;
                }
            }
            else
            {
                loginStatus = LoginStatus.LoginFailed;
            }
            var authResponse = new AuthStatus()
            {
                LoginStatus = loginStatus,
                Token = jwtToken,
                Role = roleName
            };
            return authResponse;
        }

        /// <summary>
        /// Thie method willaccept the token as inout parameter and wil receive token from it
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> GetUserFromTokenAsync(string token)
        {
            string userName = "";
            var jwtHandler = new JwtSecurityTokenHandler();
            // read the token values
            var jwtSecurityToken = jwtHandler.ReadJwtToken(token);
            // read claims
            var claims = jwtSecurityToken.Claims;
            // read first claim
            var userIdClaim = claims.First();
            // read the user Id
            var userId = userIdClaim.Value;
            // get the username from the userid
            var identityUser = await userManager.FindByIdAsync(userId);
            userName = identityUser.UserName;
            return userName;
        }

        public string GetRoleFormToken(string token)
        {
            string roleName = "";
            var jwtHandler = new JwtSecurityTokenHandler();
            // read the token values
            var jwtSecurityToken = jwtHandler.ReadJwtToken(token);
            // read claims
            var claims = jwtSecurityToken.Claims;
            // read first two claim
            var roleClaim = claims.Take(2);
            // read the role
            var roleRecord = roleClaim.Last();
            // read the role name
            roleName = roleRecord.Value;
            return roleName;
        }

    }
}
