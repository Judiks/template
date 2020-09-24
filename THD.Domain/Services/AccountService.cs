using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using THD.Core.Constants;
using THD.Core.Exceptions;
using THD.Domain.Entities;
using THD.Domain.Helpers;
using THD.Domain.Models.AccountModels.Request;
using THD.Domain.Models.AccountModels.Response;
using THD.Domain.Models.Enums;
using THD.Domain.Repositories;
using THD.Domain.Services.Interfaces;

namespace THD.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtHelper _jwtHelper;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtHelper jwtHelper, IMapper mapper,
            RoleManager<IdentityRole> roleManager, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _jwtHelper = jwtHelper ?? throw new ArgumentNullException(nameof(jwtHelper));
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<JwtAuthentificationResponse> Login(LoginRequest requestModel)
        {
            var user = await FindUser(requestModel.Email);
            if (user is null)
            {
                throw new ApplicationCustomException(ExceptionConstants.WrongEmailOrPassword);
            }
            var result = await _signInManager.PasswordSignInAsync(user, requestModel.Password, requestModel.IsRememberMe, false);
            if (!result.Succeeded)
            {
                throw new ApplicationCustomException(ExceptionConstants.WrongEmailOrPassword);
            }
            IEnumerable<string> userRoles = await _userManager.GetRolesAsync(user);
            JwtAuthentificationResponse response = _jwtHelper.GenerateToken(user, userRoles);
            return response;
        }
        private async Task<ApplicationUser> FindUser(string login)
        {
            ApplicationUser userByEmail = await _userManager.FindByEmailAsync(login);
            if (userByEmail != null)
            {
                return userByEmail;
            }
            ApplicationUser userByUserName = await _userManager.FindByNameAsync(login);
            if (userByUserName != null)
            {
                return userByUserName;
            }
            return null;
        }
        public async Task<JwtAuthentificationResponse> RefreshToken(RefreshTokenRequest model)
        {
            JwtSecurityToken refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(model.RefreshToken);
            if (refreshToken.ValidFrom >= DateTime.UtcNow || refreshToken.ValidTo <= DateTime.UtcNow)
            {
                throw new UnauthorizeCustomException(ExceptionConstants.UnauthorizeAccess);
            }
            string userId = refreshToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new UnauthorizeCustomException(ExceptionConstants.UnauthorizeAccess);
            }
            IEnumerable<string> userRoles = await _userManager.GetRolesAsync(user);
            JwtAuthentificationResponse response = _jwtHelper.GenerateToken(user, userRoles);
            return response;
        }


        public async Task<UserResponse> CreateUser(CreateUserRequest createUserRequest)
        {
            if (_userManager.Users.Any(u => u.Email == createUserRequest.Email))
            {
                throw new ApplicationCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.UserAlreadyExist);
            }
            var user = new ApplicationUser();
            user.UserName = createUserRequest.UserName;
            user.Email = createUserRequest.Email;
            user.FirstName = createUserRequest.FirstName;
            user.LastName = createUserRequest.LastName;
            user.PasswordHash = createUserRequest.Password;
            var status = await _userManager.CreateAsync(user, createUserRequest.Password);
            if (!status.Succeeded)
            {
                throw new ApplicationCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.PasswordDoesntValid);
            }
            string roleToCreate = createUserRequest.UserRole.ToString();
            await _userManager.AddToRoleAsync(user, roleToCreate);
            var result = _mapper.Map<ApplicationUser, UserResponse>(user);
            result.UserRole = createUserRequest.UserRole;
            return result;
        }

        public async Task<GetAllUserResponse> GetAllUsers()
        {
            var result = new GetAllUserResponse();
            var userRoles = await _userRepository.GetAlUserRoles();
            var roles = _roleManager.Roles.ToList();
            var users = _userManager.Users.ToList();
            foreach (var role in roles)
            {
                var usersInRole = userRoles.FindAll(user => user.RoleId == role.Id).ToList();
                foreach (var userInRole in usersInRole)
                {
                    var userWithRole = users.Find(user => user.Id == userInRole.UserId);
                    var userResponse = _mapper.Map<ApplicationUser, UserResponse>(userWithRole);
                    userResponse.UserRole = (UserRoleModel)Enum.Parse(typeof(UserRoleModel), role.Name);
                    result.Users.Add(userResponse);
                }
            }
            return result;
        }

        public async Task<UserResponse> GetUserById(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var role = await _userManager.GetRolesAsync(user);
            var result = _mapper.Map<ApplicationUser, UserResponse>(user);
            result.UserRole = (UserRoleModel)Enum.Parse(typeof(UserRoleModel), role[0]);
            return result;

        }

        public async Task<string> UpdateUser(UpdateUserRequest userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.Id);
            if (user is null)
            {
                throw new ApplicationCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.UserDoesntExist);
            }
            user.Email = userModel.Email;
            user.UserName = userModel.UserName;
            user.LastName = userModel.LastName;
            user.FirstName = userModel.FirstName;
            if (!string.IsNullOrEmpty(userModel.Password))
            {
                bool IsPasswordValid = PasswordValidation(userModel.Password);
                if (!IsPasswordValid)
                {
                    throw new ApplicationCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.PasswordDoesntValid);
                }

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userModel.Password);
            }
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new ApplicationCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.FrienldyErrorMessage);
            }
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            string roleToUpdate = userModel.UserRole.ToString();
            await _userManager.AddToRoleAsync(user, roleToUpdate);
            return user.Id;

        }

        public async Task<string> DeleteUser(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            string currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUserId == id)
            {
                throw new ApplicationCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.CantDeleteYourself);
            }
            if (user is null)
            {
                throw new ApplicationCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.UserDoesntExist);
            }
            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                throw new ApplicationCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.FrienldyErrorMessage);
            }

            var users = _userManager.Users.ToList();
            if (!users.Any())
            {
                throw new ApplicationCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.FrienldyErrorMessage);
            }
            return id;
        }

        private bool PasswordValidation(string password)
        {
            var hasNumber = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,20}$");
            if (!hasNumber.IsMatch(password))
            {
                return false;
            }

            return true;
        }
    }
}
