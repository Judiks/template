using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using THD.Core.Constants;
using THD.Core.Exceptions;
using THD.Domain.Models.AccountModels.Request;
using THD.Domain.Models.AccountModels.Response;
using THD.Domain.Services.Interfaces;

namespace THD.WEB.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]/[Action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _userService;
        public AccountController(IAccountService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(JwtAuthentificationResponse), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody]LoginRequest request)
        {
            JwtAuthentificationResponse response = await _userService.Login(request);
            return Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(JwtAuthentificationResponse), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshToken([FromBody]RefreshTokenRequest request)
        {
            JwtAuthentificationResponse response = await _userService.RefreshToken(request);
            return Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create([FromBody]CreateUserRequest createUserRequest)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.InvalidModelState);
            }
            var result = await _userService.CreateUser(createUserRequest);
            return Ok(result);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetAllUserResponse), StatusCodes.Status200OK)]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetById(string Id)
        {
            var result = await _userService.GetUserById(Id);
            return Ok(result);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update([FromBody]UpdateUserRequest user)
        {
            var result = await _userService.UpdateUser(user);
            return Ok(result);
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }
    }
}
