using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly TodoContext _context;
        public TokenController(IConfiguration config, SignInManager<User> signInManager, UserManager<User> userManager, TodoContext context)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUser(login);
           // var userg = await _userManager.FindByEmailAsync(login.Username);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                //UserToken userToken = new UserToken()
                //{
                //    UserId=1,
                //    Value= tokenString,
                //};
              
                //_context.UserTokens.Add(userToken);

                // var getUset = await _userManager.FindByNameAsync(user.Username);
              //  var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);

                //if (result.Succeeded)
                //{
                    //var getUset = await _userManager.FindByNameAsync(login.Username);
                    //await _userManager.RemoveAuthenticationTokenAsync(getUset, "MyApp", "RefreshToken");
                    //var newRefreshToken = await _userManager.GenerateUserTokenAsync(getUset, "MyApp", "RefreshToken");
                    //await _userManager.SetAuthenticationTokenAsync(getUset, "MyApp", "RefreshToken", newRefreshToken);

                    response = Ok(new { token = tokenString });
                  //  return await generateJwtToken(login.Username, userg);
                //}
            }

            return response;
        }

        [NonAction]
        private async Task<object> generateJwtToken(string email, User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //new Claim(ClaimTypes.NameIdentifier, user.Id),
             new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Role, roles[0])
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_config["JwtExpireHours"]));
            var token = new JwtSecurityToken(_config["JwtIssuer"],
                                            _config["JwtAudience"],
                                            claims,
                                            expires: expires,
                                            signingCredentials: creds
                                            );
            var response = new
            {
                auth_token = new JwtSecurityTokenHandler().WriteToken(token),
                success = true
            };
            return Ok(response);
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddSeconds(30),
              signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //public bool ValidateCurrentToken(string token)
        //{
        //    var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
        //    var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

        //    var myIssuer = "http://mysite.com";
        //    var myAudience = "http://myaudience.com";

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    try
        //    {
        //        tokenHandler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidIssuer = myIssuer,
        //            ValidAudience = myAudience,
        //            IssuerSigningKey = mySecurityKey
        //        }, out SecurityToken validatedToken);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        private async Task<UserModel> AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            //Validate the User Credentials  
            //Demo Purpose, I have Passed HardCoded User Information  
            var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);
            if (result.Succeeded)
            {
                user = new UserModel { Username = login.Username };
            }
            //else
            //{
            //    ModelState.AddModelError("", "Invalid UserName or Password");
            //    return View();
            //}
            //if (login.Username == "Jignesh")
            //{
            //    user = new UserModel { Username = "Jignesh Trivedi" };
            //}
            return user;
        }
    }
}