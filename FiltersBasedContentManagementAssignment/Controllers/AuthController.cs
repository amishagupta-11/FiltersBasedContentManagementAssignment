using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FiltersBasedContentManagementAssignment.Models;
using FiltersBasedContentManagementAssignment.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace FiltersBasedContentManagementAssignment.Controllers
{
    /// <summary>
    /// Controller for handling user authentication and role management functionalities.
    /// Provides endpoints for user login, registration, and role assignment.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ContentManagementDbContext DbContext;
        private readonly JwtSettings JwtSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="context">Database context for accessing user information.</param>
        /// <param name="jwtSettings">Settings for JWT token configuration.</param>
        public AuthController(ContentManagementDbContext context, IOptions<JwtSettings> jwtSettings)
        {
            DbContext = context;
            JwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token if successful.
        /// </summary>
        /// <param name="loginModel">The login credentials containing username and password.</param>
        /// <returns>A JWT token if authentication succeeds; otherwise, an Unauthorized response.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var user = DbContext.Users.FirstOrDefault(u => u.Username == loginModel.Username);
            if (user == null || user.Password != loginModel.Password)
            {
                return Unauthorized("Invalid credentials");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: JwtSettings.Issuer,
                audience: JwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        /// <summary>
        /// Registers a new user with a default "User" role if the username is unique.
        /// </summary>
        /// <param name="registerModel">The registration details containing username and password.</param>
        /// <returns>Success message if registration is successful; otherwise, a BadRequest response.</returns>
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel registerModel)
        {
            var existingUser = DbContext.Users.FirstOrDefault(u => u.Username == registerModel.Username);
            if (existingUser != null)
            {
                return BadRequest("Username is already taken.");
            }

            var hashedPassword = registerModel.Password;

            var user = new User
            {
                Username = registerModel.Username,
                Password = hashedPassword,
                Role = "User"
            };

            DbContext.Users.Add(user);
            DbContext.SaveChanges();

            return Ok("User registered successfully.");
        }

        /// <summary>
        /// Assigns a new role to an existing user. Only accessible by users with "Admin" role.
        /// </summary>
        /// <param name="roleAssignment">The role assignment details containing the username and the role to assign.</param>
        /// <returns>Success message if the role is assigned successfully; otherwise, an error message.</returns>
        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
        public IActionResult AssignRole([FromBody] RoleAssignmentModel roleAssignment)
        {
            var user = DbContext.Users.FirstOrDefault(u => u.Username == roleAssignment.Username);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (user.Username == User.Identity.Name && roleAssignment.Role == "Admin")
            {
                return BadRequest("You cannot assign the admin role to yourself.");
            }

            user.Role = roleAssignment.Role;
            DbContext.Users.Update(user);
            DbContext.SaveChanges();

            return Ok("Role assigned successfully.");
        }
    }
}
