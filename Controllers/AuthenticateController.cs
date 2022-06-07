using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoList.Authentication;
using TodoList.Helpers;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            //var str = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjFiYTMxODQ3LWZiMDktNDJkNy04Y2E3LWExMDhiMTkxOWNhMyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJhZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFkbWluQGdtYWlsLmNvbSIsImp0aSI6ImQwMjliNzY4LWNkYjMtNDRjNi1hMjhlLTQ4OWE4ZjFiYjJjMiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNjQ0OTQ4Njg0LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjYxOTU1IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.XKEdsx3n6ktyuJo3JqLL5eta5zJnUBYdLAlHLbjYUwY";
            //var handler = new JwtSecurityTokenHandler();
            //var tokenDecode = handler.ReadJwtToken(str);
            //var name = tokenDecode.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            //var email = tokenDecode.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            //var guidDecode = tokenDecode.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            //var guid = "348dd237-23c7-45e2-915b-f1062fdd8ccb";
            //var check = guidDecode == guid;

            var user = await userManager.FindByNameAsync(model.Username);

            if(user == null)
                return Ok(new Response { Status = "Error", Message = "Invalid username or password!" });

            if(!await userManager.CheckPasswordAsync(user, model.Password))
                return Ok(new Response { Status = "Error", Message = "Invalid username or password !" });

            if(!await userManager.IsEmailConfirmedAsync(user))
                return Ok(new Response { Status = "Error", Message = "Email has not been confirmed !" });

            if (await userManager.IsLockedOutAsync(user))
                return Ok(new Response { Status = "Error", Message = "User has been locked !" });

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            //Check item exist
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return Ok(new Response { Status = "Error", Message = "User already exists!" });

            var emailExists = await userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return Ok(new Response { Status = "Error", Message = "Email already exists!" });

            //Create new user
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            //Add user to database
            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return Ok(new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //Send confirmation email
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authenticate" , new { token, email = user.Email }, Request.Scheme);
            SendGridEmailSender emailHelper = new SendGridEmailSender(_configuration);
            bool emailResponse = await emailHelper.SendEmail(user.Email, confirmationLink, "Confirm your email");

            if (await roleManager.RoleExistsAsync(UserRoles.Member))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Member);
            }
            if (!emailResponse)
                return Ok(new Response { Status = "Error", Message = "Create user success. Email failed !" });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);

            if (userExists != null)
                return Ok(new Response { Status = "Error", Message = "User already exists!" });

            var emailExists = await userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return Ok(new Response { Status = "Error", Message = "Email already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return Ok(new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Ok(new Response { Status = "Error", Message = "Email does not exist !" });

            var result = await userManager.ConfirmEmailAsync(user, token);
            return Ok(new Response { Status = "Success", Message = "Email confirmation successful !" });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasword)
        {
            var user = await userManager.FindByEmailAsync(forgotPasword.Email);
            if (user == null)
                return Ok(new Response { Status = "Error", Message = "User not found !" });

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Authenticate", new { token, email = user.Email }, Request.Scheme);

            EmailHelper emailHelper = new EmailHelper(_configuration);
            bool emailResponse = emailHelper.SendEmail(user.Email, link, "Reset your account password");

            if (!emailResponse)
                return Ok(new Response { Status = "Error", Message = "Email failed !" });

            return Ok(new Response { Status = "Success", Message = "Email sent successfull !" });
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("reset-password")]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return Ok(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return Ok(new Response { Status = "Error", Message = "Reset password failed !" });

            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                return Ok(new Response { Status = "Error", Message = "User not found !" });

            var resetPassResult = await userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                return Ok(new Response { Status = "Error", Message = "Reset password failed !" });
            }

            return Ok(new Response { Status = "Success", Message = "Password reset successful !" });
        }
    }
}
