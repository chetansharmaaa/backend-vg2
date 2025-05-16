using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserManagementService.Core.Data;
using UserManagementService.Core.Models;
using UserManagementService.Core.RepositoryContract;

namespace UserManagementService.Infra
{
    public class userRepository : IRepository
    {
        private readonly ILogger<userRepository> _logger;
        private readonly CoolpalzContext _db;

        public userRepository(ILogger<userRepository> logger, CoolpalzContext db)
        {
            _logger = logger;
            _db = db;
        }
        public async Task<User>  AddUser(User user)
        {
            try {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(user.FullName))
                {
                    throw new ArgumentException("User full name cannot be empty.", nameof(user.FullName));
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    throw new ArgumentException("User email cannot be empty.", nameof(user.Email));
                }

                if (!IsValidEmail(user.Email))
                {
                    throw new ArgumentException("User email is not in a valid format.", nameof(user.Email));
                }

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return user;

            }
            catch(Exception ex) {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw new Exception("An unexpected error occurred.", ex);

            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public async Task<User> DeleteUser(int userId)
        {
            try
            {
                var user = await _db.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {userId} not found.");
                }

                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the user with ID {userId}.");
                throw new Exception($"An error occurred while deleting the user with ID {userId}.", ex);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await _db.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all users.");
                throw new Exception("An error occurred while retrieving all users.", ex);
            }
        }

        public async Task<User> GetUser(int userId)
        {
            try
            {
                var user = await _db.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {userId} not found.");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the user with ID {userId}.");
                throw new Exception($"An error occurred while retrieving the user with ID {userId}.", ex);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email cannot be empty.", nameof(email));
                }

                if (!IsValidEmail(email))
                {
                    throw new ArgumentException("Email is not in a valid format.", nameof(email));
                }

                var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with email {email} not found.");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the user with email {email}.");
                throw new Exception($"An error occurred while retrieving the user with email {email}.", ex);
            }
        }

        public async Task<User> UpdateUser(int id , User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(user.FullName))
                {
                    throw new ArgumentException("User full name cannot be empty.", nameof(user.FullName));
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    throw new ArgumentException("User email cannot be empty.", nameof(user.Email));
                }
                if (!IsValidEmail(user.Email))
                {
                    throw new ArgumentException("User email is not in a valid format.", nameof(user.Email));
                }

                var existingUser = await _db.Users.FindAsync(id);
                if (existingUser == null)
                {
                    throw new KeyNotFoundException($"User with ID {user.Id} not found.");
                }

                existingUser.FullName = user.FullName;
                existingUser.Email = user.Email;
                existingUser.PasswordHash = user.PasswordHash;
                existingUser.Role = user.Role;
                existingUser.CreatedAt = user.CreatedAt;

                _db.Users.Update(existingUser);
                await _db.SaveChangesAsync();
                return existingUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the user with ID {user.Id}.");
                throw new Exception($"An error occurred while updating the user with ID {user.Id}.", ex);

            }
        }

        public async Task<List<UserPurchase>> GetPurchaseHistory(int userId)
        {
            List<UserPurchase> userPurchase = await _db.UserPurchases.Where(p => p.UserId == userId).Include(p=> p.Link).ThenInclude(l => l.Deal).ToListAsync();
            return userPurchase;
        }

        public async Task<List<UserClick>> GetClickHistory(int userId)
        {
            List<UserClick> userClick = await _db.UserClicks.Where(p => p.UserId == userId).Include(c=>c.Link).ThenInclude(l => l.Deal).ToListAsync();
            return userClick;
        }

        public async Task<decimal> GetTotalCashback(int userId)
        {
            User user = await _db.Users.FindAsync(userId);
            if(user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            return user.TotalCashback ?? 0;
        }

        public async Task<User> LoginUser(User user)
        {
            try
            {
                User response = await _db.Users.SingleOrDefaultAsync(u => u.Email == user.Email);
                if (user == null || user.PasswordHash != user.PasswordHash)
                {
                    throw new UnauthorizedAccessException("Invalid email or password.");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in the user.");
                throw new Exception("An error occurred while logging in the user.", ex);
            }
        }
    }
}
