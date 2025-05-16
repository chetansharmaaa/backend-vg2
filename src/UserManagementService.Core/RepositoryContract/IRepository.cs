using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.RepositoryContract
{
    public interface IRepository
    {
        Task<User> AddUser(User user);

        Task<User> UpdateUser(int id ,User user);

        Task<User> DeleteUser(int userId);

        Task<User> GetUser(int userId);
        Task<User> GetUserByEmail(string email);

        Task<List<User>> GetAllUsers();

        Task<List<UserPurchase>> GetPurchaseHistory(int userId);

        Task<List<UserClick>> GetClickHistory(int userId);

        Task<decimal> GetTotalCashback(int userId);

        Task<User> LoginUser(User user);

    }
}
