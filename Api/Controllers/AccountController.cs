using System.Text;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Errors;
using Api.Extentions;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IProfileRepository _profileRepository;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, RoomContext context, IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;


        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),

            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            // var userRole = _roleRepository.GetRole(user);
            // var roleName = _roleRepository.GetRoleName(userRole);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                // Role = roleName
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            }

            var user = new AppUser
            {

                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var profile = new Profile
            {
                UserEmail = registerDto.Email,
                ContactEmail = registerDto.ContactEmail,
                Description = registerDto.Description,
                // Birthday = registerDto.Birthday,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Phone = registerDto.Phone,
                Location = registerDto.Location
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            await _profileRepository.AddAsync(profile);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            if (result.Succeeded)
            {
                // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                // var callbackUrl = Url.Page(
                //     "/Account/ConfirmEmail",
                //     pageHandler: null,
                //     values: new { area = "Identity", userId = user.Id, code = code },
                //     protocol: Request.Scheme);

                // await _emailSender.SendEmailAsync(registerDto.Email, "Confirm your email",
                //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return new UserDto
                    {
                        ConfirmEmail = true
                    };
                }
                else
                {

                    return new UserDto
                    {

                        Token = _tokenService.CreateToken(user),
                        Email = user.Email
                    };
                }

            }

            return null;

        }

        [HttpPost("forgotpassword")]
        public async Task<ActionResult<bool>> ForgotPassword(ForgotPasswordDto dto)
        {

            if (string.IsNullOrEmpty(dto.Email))
            {
                return BadRequest(new ApiResponse(400));
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var callbackUrl = "http://localhost:4200/account/resetpassword/" + token;
                System.IO.File.WriteAllText("resetLink.txt", callbackUrl);
                //   await _emailSender.SendEmailAsync(dto.Email, "Confirm your email",
                //     $"Please use link to reset password <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return true;
            }

            //we get this far something failed

            return false;
        }

        [HttpPost("resetpassword")]
        public async Task<ActionResult<bool>> ResetPassword(ResetPasswordDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user != null)
                {

                    var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Password);
                    if (!result.Succeeded)
                    {

                        foreach (var error in result.Errors)
                        {
                            System.IO.File.WriteAllText("error.txt", error.Description);
                            ModelState.AddModelError("", error.Description);
                        }
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}