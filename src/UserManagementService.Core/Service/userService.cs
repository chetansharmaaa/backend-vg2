using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Extensionclass;
using UserManagementService.Core.Models;
using UserManagementService.Core.RepositoryContract;
using UserManagementService.Core.ServiceContract;
using UserManagementService.Core.UserDTO;
using UserManagementService.Core.userServiceContract;

namespace UserManagementService.Core.UserService
{
    public class userService : IuserContract
    {
        private readonly IRepository _repository;
        private readonly IAuth _auth;

        public userService(IRepository repository , IAuth auth)
        {
            _repository = repository;
            _auth = auth;
        }

        public async Task<response> AddUser(userAddRequest user)
        {
            try
            {
                var userModel = user.ToModel();
                var addedUser = await _repository.AddUser(userModel);
                return new response
                {
                    Success = true,
                    Message = "User added successfully."
                };
            }
            catch (Exception ex)
            {
                return new response
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<response> DeleteUser(int userId)
        {
            try
            {
                var deletedUser = await _repository.DeleteUser(userId);
                return new response
                {
                    Success = true,
                    Message = "User deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new response
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            var users = await _repository.GetAllUsers();
            return users.Select(u => u.ToResponse()).ToList();
        }

        public async Task<List<UserClickDTO>> getClickHistory(int userId)
        {
            List<UserClick> clicks = await _repository.GetClickHistory(userId);
            return clicks.Select(x => x.ToUserClickDTO()).ToList();
        }

        public async Task<List<UserPurchaseDTO>> getcpurchasehistory(int userId)
        {
            List<UserPurchase> purchases = await _repository.GetPurchaseHistory(userId);
            return purchases.Select(x => x.ToUserPurchaseDTO()).ToList();
        }

        public async Task<decimal> getTotalCashback(int userId)
        {
            return await _repository.GetTotalCashback(userId);

        }

        public async Task<UserResponse> GetUser(int userId)
        {
            var user = await _repository.GetUser(userId);
            return user.ToResponse();
        }

        public async Task<UserLoginResponse> Login(UserLoginDTO user)
        {
            var existingUser = await _repository.GetUserByEmail(user.Email);
            if (existingUser == null || existingUser.PasswordHash != user.PasswordHash)
            {
                return new UserLoginResponse {jwtToken = null };
            }

            var token = _auth.GenerateToken(existingUser);
            return new UserLoginResponse { Success = true , jwtToken = token ,userId = existingUser.Id };

        }

        public async Task<response> UpdateUser(int id , updateUserDTO user)
        {
            try
            {
                var existingUser = await _repository.GetUser(id);
                if (existingUser == null)
                {
                    return new response
                    {
                        Success = false,
                        Message = "User not found."
                    };
                }

                var updatedUser = user.ToModel();
                await _repository.UpdateUser(id,updatedUser);
                return new response
                {
                    Success = true,
                    Message = "User updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new response
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}
