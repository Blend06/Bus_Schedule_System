using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Transify.Data;
using Transify.Models.Entities;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Transify.Models.DTOs;
using Transify.Filters;
using Transify.ViewModel.Authenticate;
using Transify.Interfaces;
using Transify.Models.Enums;
using Transify.Models.Utilities;


namespace Transify.Controllers
{
    [Route("Authenticate")]
    public class AuthenticateController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IAuthenticateService _userService;

        public AuthenticateController(IAuthenticateService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<AuthenticateController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.RegisterAsync(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("UserList")]
        [ServiceFilter(typeof(AdminOnlyFilter))]
        public async Task<IActionResult> UserList()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public IActionResult Users()
        {
            return View();
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(ResponseFactory.ErrorResponse(ResponseCodes.NotFound, ResponseMessages.NotFound));

            var model = _mapper.Map<EditUserViewModel>(user);
            return View(model);
        }

        [HttpPost("EditUser")]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var success = await _userService.EditUserAsync(model.UserId, model);
            if (!success)
                return NotFound(ResponseFactory.ErrorResponse(ResponseCodes.NotFound, "User not found."));

            return RedirectToAction("UserList");
        }

        [HttpGet("GetEnums")]
        public IActionResult GetEnums()
        {
            var roles = Enum.GetValues(typeof(UserRole))
                .Cast<UserRole>()
                .Select(role => new { Value = (int)role, Text = role.ToString() });

            var businessTypes = Enum.GetValues(typeof(BusinessType))
                .Cast<BusinessType>()
                .Select(type => new { Value = (int)type, Text = type.ToString() });

            return Ok(new
            {
                Roles = roles,
                BusinessTypes = businessTypes
            });
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(ResponseFactory.ErrorResponse(ResponseCodes.NotFound, "User not found."));

            return View(user);
        }

        [HttpPost("DeleteConfirmed/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.SoftDeleteUserAsync(id);
            return RedirectToAction("UserList");
        }

        [HttpPost("Restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            await _userService.RestoreUserAsync(id);
            return RedirectToAction("UserList");
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid input.");
                return View(model);
            }

            var user = await _userService.LoginAsync(model.Email, model.Password);
            if (user == null)
            {
                _logger.LogWarning("Login failed for user {Email}. Error: {ErrorCode} - {ErrorMessage}", model.Email, ApiStatusEnum.LOGIN_FAILED, "User not found.");

                ModelState.AddModelError(string.Empty, "User not found.");

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Json(ResponseFactory.ErrorResponse(ResponseCodes.Unauthorized, "User not found."));

                return View(model);
            }

            _logger.LogInformation("User {Email} logged in successfully.", model.Email);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(ResponseFactory.SuccessResponse("Login successful.", user));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            _userService.ClearUserSession();

            _logger.LogInformation("User logged out successfully.");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = _userService.GetCurrentUserId();

            if (!userId.HasValue)
            {
                return Unauthorized(ResponseFactory.ErrorResponse(ResponseCodes.Unauthorized, "User is not authorized to access this resource."));
            }

            var user = await _userService.GetCurrentUserAsync(userId.Value);
            if (user == null)
            {
                return NotFound(ResponseFactory.ErrorResponse(ResponseCodes.NotFound, "User not found."));
            }

            return Ok(ResponseFactory.SuccessResponse(ResponseMessages.Success, new
            {
                user.UserId,
                user.FirstName,
                user.LastName
            }));
        }
    }
}