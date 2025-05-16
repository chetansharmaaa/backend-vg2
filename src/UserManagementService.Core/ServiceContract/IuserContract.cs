using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Models;
using UserManagementService.Core.UserDTO;

namespace UserManagementService.Core.userServiceContract
{
    public interface IuserContract
    {
        Task<response> AddUser(userAddRequest user);

        Task<response> UpdateUser(int id ,updateUserDTO user);

        Task<response> DeleteUser(int userId);

        Task<UserResponse> GetUser(int userId);

        Task<List<UserResponse>> GetAllUsers();

        Task<List<UserPurchaseDTO>> getcpurchasehistory(int userId);


        Task<List<UserClickDTO>> getClickHistory(int userId);

        Task<decimal> getTotalCashback(int userId);

        Task<UserLoginResponse> Login(UserLoginDTO user);

    }
}
