using Admin.Core.Models;
using Admin.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Api.Controllers
{
  

        [Route("api/[controller]")]
        [ApiController]
        public class SecurityController : ControllerBase
        {

            private readonly AuthSecurityService service;

            public SecurityController(AuthSecurityService service)
            {
                this.service = service;
            }

            [Authorize(Policy = "AdminPolicy")]
            [Route("roles/readall")]
            [HttpGet]
            public async Task<Microsoft.AspNetCore.Mvc.IActionResult> GetRolesAsync()
            {
                ResponseStatus response;
                try
                {
                    var roles = await service.GetRolesAsync();
                    return Ok(roles);
                }
                catch (Exception ex)
                {
                    response = SetResponse(400, ex.Message, "", "");
                    return BadRequest(response);
                }
            }

            [Authorize(Policy = "AdminPolicy")]
            [Route("users/readall")]
            [HttpGet]
            public async Task<Microsoft.AspNetCore.Mvc.IActionResult> GetUsersAsync()
            {
                ResponseStatus response;
                try
                {
                    var users = await service.GetUsersAsync();
                    return Ok(users);
                }
                catch (Exception ex)
                {
                    response = SetResponse(400, ex.Message, "", "");
                    return BadRequest(response);
                }
            }

            [Authorize(Policy = "AdminPolicy")]
            [Route("post/role/create")]
            [HttpPost]
            public async Task<Microsoft.AspNetCore.Mvc.IActionResult> PostRoleAsync(ApplicationRole role)
            {
                ResponseStatus response;
                try
                {
                    // Use Microsoft.AspNetCore.Identity.IdentityRole
                    IdentityRole roleInfo = new IdentityRole()
                    {
                        Name = role.Name
                    };

                    var res = await service.CreateRoleAsync(roleInfo);
                    if (!res)
                    {
                        response = SetResponse(500, "Role Registration Failed", "", "");
                        return StatusCode(500, response);
                    }

                    response = SetResponse(200, $"{role.Name} is Created successfully", "", "");
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    response = SetResponse(400, ex.Message, "", "");
                    return BadRequest(response);
                }
            }



        [Authorize(Policy = "AdminPolicy")]
        [Route("post/assign/role")]
        [HttpPost]
        public async Task<Microsoft.AspNetCore.Mvc.IActionResult> AssignRoleToUserAsync(UserRole user)
        {
            ResponseStatus response;
            try
            {
                var isRoleAssigned = await service.AssignRoleToUserAsync(user);
                if (isRoleAssigned)
                {
                    response = SetResponse(200, "Role assigned to user successfully", "", "");
                    return Ok(response);
                }
                else
                {
                    response = SetResponse(500, "Role assignment failed", "", "");
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                response = SetResponse(400, ex.Message, "", "");
                return BadRequest(response);
            }
        }


        //[Route("post/register/user")]
        //    [HttpPost]
        //    public async Task<Microsoft.AspNetCore.Mvc.IActionResult> RegisterUserAsync(RegisterUser user)
        //    {
        //        ResponseStatus response;
        //        try
        //        {
        //            var res = await service.RegisterUserAsync(user);
        //            if (!res)
        //            {
        //                response = SetResponse(500, "User Registration Failed", "", "");
        //                return StatusCode(500, response);
        //            }

        //            response = SetResponse(200, $"User {user.Email} is Created successfully", "", "");
        //            return Ok(response);
        //        }
        //        catch (Exception ex)
        //        {
        //            response = SetResponse(400, ex.Message, "", "");
        //            return BadRequest(response);
        //        }
        //    }



        [Route("post/register/user")]
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync(RegisterUser user)
        {
            ResponseStatus response;
            try
            {
                // Your logic to set user role
                // For example, you can set a default role or provide a list of available roles for the user to choose from.
                string defaultRole = "User"; // Set a default role if not provided in the registration request

                // Modify the user object to include the role
                user.Role = string.IsNullOrEmpty(user.Role) ? defaultRole : user.Role;

                // Call the service method passing the modified user object
                var res = await service.RegisterUserAsync(user);

                if (!res)
                {
                    response = SetResponse(500, "User Registration Failed", "", "");
                    return StatusCode(500, response);
                }

                response = SetResponse(200, $"User {user.Email} is Created successfully with role {user.Role}", "", "");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = SetResponse(400, ex.Message, "", "");
                return BadRequest(response);
            }
        }




        [Route("post/auth/user")]
            [HttpPost]
            public async Task<Microsoft.AspNetCore.Mvc.IActionResult> AuthUserAsync(LoginUser user)
            {
                ResponseStatus response = new ResponseStatus();
                try
                {
                    var res = await service.AuthUserAsync(user);
                    if (res.LoginStatus == LoginStatus.LoginFailed)
                    {
                        response = SetResponse(401, "UserName or Password is not found", "", "");
                        return Unauthorized(response);
                    }
                    if (res.LoginStatus == LoginStatus.NoRoleToUser)
                    {
                        response = SetResponse(401, "User is not activated with role. Please contact admin on ilkinlikus@gmail.com", "", "");
                        return Unauthorized(response);
                    }
                    if (res.LoginStatus == LoginStatus.LoginSuccessful)
                    {
                        response = SetResponse(200, "Login Sussessful", res.Token, res.Role);
                        response.UserName = user.UserName;
                        return Ok(response);
                    }
                    else
                    {
                        response = SetResponse(500, "Internal Server Error Occured", "", "");
                        return StatusCode(500, response);
                    }
                }
                catch (Exception ex)
                {
                    response = SetResponse(400, ex.Message, "", "");
                    return BadRequest(response);
                }
            }

            /// <summary>
            /// Method to Set the Response
            /// </summary>
            /// <param name="code"></param>
            /// <param name="message"></param>
            /// <returns></returns>
            private ResponseStatus SetResponse(int code, string message, string token, string role)
            {
                ResponseStatus response = new ResponseStatus()
                {
                    StatusCode = code,
                    Message = message,
                    Token = token,
                    Role = role
                };
                return response;
            }
        }
    }

