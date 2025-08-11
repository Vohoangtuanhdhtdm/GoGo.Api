using GoGo.Core.Entities;
using GoGo.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Context
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;

        public AccountController(
            IUnitOfWork unitOfWork, 
            UserManager<IdentityUser<Guid>> userManager,
            SignInManager<IdentityUser<Guid>> signInManager,
             IConfiguration configuration
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        #endregion

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
                return BadRequest("User with this email already exists.");

            var identityUser = new IdentityUser<Guid> { Email = request.Email, UserName = request.Email };
            var result = await _userManager.CreateAsync(identityUser, request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Tạo ánh xạ dữ liệu qua UserProfile
            var userProfile = new UserProfile(identityUser.Id, request.FullName, identityUser.Email);
            await _unitOfWork.UserProfiles.AddUserProfileAsync(userProfile);
            await _unitOfWork.SaveChangesAsync();

            return Ok("User created successfully!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
                return Unauthorized("Invalid credentials.");

            var userProfile = await _unitOfWork.UserProfiles.GetUserProfileByIdAsync(user.Id);
            if (userProfile == null)
                return Unauthorized("User profile not found.");

            var userProfileDto = new UserProfileDto(userProfile.Id, userProfile.FullName, user.Email);
            var token = GenerateJwtToken(user);

            return Ok(new AuthResponseDto(token, userProfileDto));
        }
        private string GenerateJwtToken(IdentityUser<Guid> user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

// Các DTO cần thiết, bạn có thể đặt trong một file riêng
public record RegisterRequestDto(string Email, string Password, string FullName);
public record LoginRequestDto(string Email, string Password);
public record UserProfileDto(Guid Id, string FullName, string Email);
public record AuthResponseDto(string Token, UserProfileDto User);