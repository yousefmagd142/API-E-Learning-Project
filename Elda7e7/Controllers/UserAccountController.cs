using BusinessLayer.DTO;
using BusinessLayer.Repository;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Elda7e7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountRegisteration _userservices;
        private readonly IConfiguration config;
        private readonly UserManager<UserAccount> _userManager;

        public UserAccountController(IUserAccountRegisteration Userservices, IConfiguration config, UserManager<UserAccount> userManager)
        {
            _userservices = Userservices;
            this.config = config;
            _userManager = userManager;
        }

        
        [HttpGet("{username:alpha}" , Name ="useracountfromusername")]
        public IActionResult GetUserByUserName(string username) 
        {
            UserDto user = _userservices.GetUser(username);
            if (user == null) 
            {
                return BadRequest("there is no username like that");
            }
            return Ok(user);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserAccountDTO newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid data",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            // If the user is trying to assign the Admin role, check their privileges
            if (newUser.IsAdmin)
            {
                // Get the current logged-in user
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);

                if (currentUser == null)
                {
                    return new ObjectResult(new
                    {
                        message = "Permission Denied",
                        reason = "Only authenticated users with Admin privileges can create admin accounts."
                    })
                    {
                        StatusCode = StatusCodes.Status403Forbidden // Set 403 Forbidden
                    };
                }

                // Check if the current user has the 'Admin' role
                bool isAdmin = await _userManager.IsInRoleAsync(currentUser, "Admin");

                if (!isAdmin)
                {
                    return new ObjectResult(new
                    {
                        message = "Permission Denied",
                        reason = "Only admins can create accounts with admin privileges."
                    })
                    {
                        StatusCode = StatusCodes.Status403Forbidden // Set 403 Forbidden
                    };
                }
            }

            // Proceed with user account creation
            UserAccount userAccount = _userservices.RegisterUser(newUser);

            // Create the user with the provided password
            var result = await _userManager.CreateAsync(userAccount, newUser.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                return BadRequest(new
                {
                    message = "Registration failed",
                    errors
                });
            }

            // Assign roles after successful user creation
            if (newUser.IsAdmin)
            {
                // Assign the Admin role to the new user
                var roleResult = await _userManager.AddToRoleAsync(userAccount, "Admin");
                if (!roleResult.Succeeded)
                {
                    var errors = roleResult.Errors.Select(error => error.Description).ToList();
                    return BadRequest(new
                    {
                        message = "Failed to assign Admin role",
                        errors
                    });
                }
            }
            else
            {
                // Assign the default "User" role
                var roleResult = await _userManager.AddToRoleAsync(userAccount, "User");
                if (!roleResult.Succeeded)
                {
                    var errors = roleResult.Errors.Select(error => error.Description).ToList();
                    return BadRequest(new
                    {
                        message = "Failed to assign User role",
                        errors
                    });
                }
            }

            // Return success with the new user's details
            string url = Url.Link("useraccountfromusername", new { username = newUser.UserName });
            return Created(url, newUser);
        }


        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInDTO UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                UserAccount UserFromDb = await _userManager.FindByNameAsync(UserFromRequest.UserName);
                if (UserFromDb != null)
                {
                    bool found =await _userManager.CheckPasswordAsync(UserFromDb, UserFromRequest.Password);
                    if (found)
                    {
                        List<Claim> UserClaims = new List<Claim>();
                        // Token Genrated id change (JWT Predefind Claims )
                        UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //generated id for token
                        UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, UserFromDb.Id));
                        UserClaims.Add(new Claim(ClaimTypes.Name, UserFromDb.UserName));

                        var UserRoles = await _userManager.GetRolesAsync(UserFromDb);

                        foreach (var roleName in UserRoles)
                        {
                            UserClaims.Add(new Claim(ClaimTypes.Role, roleName));
                        }
                        var SignInKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));

                        SigningCredentials signingCred =
                            new SigningCredentials(SignInKey, SecurityAlgorithms.HmacSha256);

                        //this is the design
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            audience: config["JWT:AudienceIP"],
                            issuer: config["JWT:IssuerIP"],
                            expires: DateTime.Now.AddHours(24),
                            claims:UserClaims,
                            signingCredentials: signingCred
                        );
                        //generate token request
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = DateTime.Now.AddHours(24) // mytoken.ValidTo
                        });


                    }
                }
                return BadRequest("Email or Password is invalid");
            }
            return BadRequest(ModelState);
        }

    }
}
