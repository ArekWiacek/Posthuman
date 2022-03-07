using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Posthuman.Core.Exceptions;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Models.Enums;
using Posthuman.Shared;

namespace Posthuman.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAvatarsService avatarsService;
        private readonly IEventItemsService eventItemsService;
        private readonly IMapper mapper;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSettings;

        public AuthenticationService(
            IUnitOfWork unitOfWork, 
            IAvatarsService avatarsService,
            IEventItemsService eventItemsService,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings)
        {
            this.unitOfWork = unitOfWork;
            this.avatarsService = avatarsService;
            this.eventItemsService = eventItemsService;
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await unitOfWork.Users.GetByIdAsync(id);
            Validate(user, "Failed to obtain user from database");
            return mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUser(LoginUserDTO dto)
        {
            var user = await unitOfWork.Users.GetByEmail(dto.Email);
            var passwordHash = passwordHasher.HashPassword(user, dto.Password);
            Validate(user, "Failed to obtain user from database");
            return mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await unitOfWork.Users.GetAllAsync();
            ValidateCollection(users, "Failed to obtain users from database or users collection is empty");
            return mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> RegisterUser(RegisterUserDTO dto)
        {
            // Validate incoming data
            await ValidateRegistrationData(dto);

            // Create and save 'User'
            var user = mapper.Map<User>(dto);
            user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);
            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.CommitAsync();

            // Create and add event item (registration)
            var userRegisteredEvent = await eventItemsService.CreateEventItem(
                user.Id, EventType.UserRegistered, EntityType.User, user.Id);
            await unitOfWork.EventItems.AddAsync(userRegisteredEvent);
            await unitOfWork.CommitAsync();

            // Create and save 'Avatar'
            var avatar = await avatarsService.CreateNewAvatar(user.Id, dto.Name);
            await unitOfWork.Avatars.AddAsync(avatar);
            await unitOfWork.CommitAsync();

            // Create and add event item (avatar creation)
            var avatarCreatedEvent = await eventItemsService.CreateEventItem(
                user.Id, EventType.AvatarCreated, EntityType.Avatar, avatar.Id);
            await unitOfWork.EventItems.AddAsync(avatarCreatedEvent);
            await unitOfWork.CommitAsync();

            return mapper.Map<UserDTO>(user);
        }

        private JwtSecurityToken GenerateJwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                null,
                expires,
                credentials);

            return token;
        }

        public async Task<string> GenerateJwtToken(int userId)
        {
            var user = await unitOfWork.Users.GetByIdAsync(userId);
            Validate(user, "Failed to obtain user from database");
            Validate(user.Role, "Failed to obtain user role from database");
            var token = GenerateJwtToken(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = tokenHandler.WriteToken(token);
            return tokenContent;
        }

        public async Task<JwtSecurityToken> ValidateUser(LoginUserDTO dto)
        {
            var user = await unitOfWork.Users.GetByEmail(dto.Email);
            ValidateLoginData(user, dto);
            var token = GenerateJwtToken(user);
            return token;
        }

        public JwtSecurityToken Verify(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(authenticationSettings.JwtKey);
            
            tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }

        private async Task ValidateRegistrationData(RegisterUserDTO dto)
        {
            if (String.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("RegisterUserDTO.Name", "Invalid 'name' parameter");

            if (String.IsNullOrEmpty(dto.Email))
                throw new ArgumentException("RegisterUserDTO.Email", "Parameter 'email' is not provided");
            else
            {
                var userWithTheSameEmail = await unitOfWork.Users.GetByEmail(dto.Email);
                if (userWithTheSameEmail != null)
                    throw new ArgumentException("RegisterUserDTO.Email", $"Email '{dto.Email}' is already taken");
            }

            if (String.IsNullOrEmpty(dto.Password))
                throw new ArgumentException("RegisterUserDTO.Password", "Parameter 'password' is not provided");
            else if (String.IsNullOrEmpty(dto.ConfirmPassword))
                throw new ArgumentException("RegisterUserDTO.ConfirmPassword", "Parameter 'confirmPassword' is not provided");
            else if (dto.Password != dto.ConfirmPassword)
                throw new ArgumentException("Provided passwords are not the equal");
        }

        private void ValidateLoginData(User user, LoginUserDTO loginDto)
        {
            if (user is null)
                throw new BadRequestException("Invalid user name or password");

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid user name or password");
        }

        private void Validate(object entity, string exceptionMessage)
        {
            if (entity == null)
                throw new BadRequestException(exceptionMessage);
        }

        private void ValidateCollection(IEnumerable<object> collection, string exceptionMessage)
        {
            if (collection == null || !collection.Any())
                throw new BadRequestException(exceptionMessage);
        }
    }
}
