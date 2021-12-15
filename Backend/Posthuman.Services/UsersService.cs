using AutoMapper;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posthuman.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UsersService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await unitOfWork.Users.GetByIdAsync(id);
            return mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await unitOfWork.Users.GetAllAsync();
            return mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> RegisterUser(UserDTO userDTO)
        {
            var userToRegister = mapper.Map<User>(userDTO);
            await unitOfWork.Users.AddAsync(userToRegister);
            await unitOfWork.CommitAsync();

            return mapper.Map<UserDTO>(userToRegister);
        }

        public async Task<UserDTO> LoginUser(UserDTO userDTO)
        {
            var user = await unitOfWork.Users.GetByEmail(userDTO.Email);
            
            if(user == null)
                throw new ArgumentNullException("user");

            if (user.Password != userDTO.Password)
                throw new Exception("Invalid password");

            return mapper.Map<UserDTO>(user);
        }
    }
}
