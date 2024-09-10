using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.Models;

namespace MyApi.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> AuthenticateUserAsync(string email, string password);
    }

    public class UserService : IUserService
    {
        private static readonly List<User> Users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "One", Email = "test.one@example.com", PasswordHash = new PasswordService().HashPassword("passone"), DateOfBirth = new DateTime(2001, 1, 1), LastLoginDate = DateTime.Now, FailedLoginAttempts = 0 },
            new User { Id = 1, FirstName = "Test", LastName = "Two", Email = "test.two@example.com", PasswordHash = new PasswordService().HashPassword("passtwo"), DateOfBirth = new DateTime(2002, 2, 2), LastLoginDate = DateTime.Now, FailedLoginAttempts = 0 },
            new User { Id = 1, FirstName = "Test", LastName = "Three", Email = "test.three@example.com", PasswordHash = new PasswordService().HashPassword("passthree"), DateOfBirth = new DateTime(2003, 3, 3), LastLoginDate = DateTime.Now, FailedLoginAttempts = 0 },
        };

        private readonly IPasswordService _passwordService;

        public UserService(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        public Task<User> GetUserAsync(int id) => Task.FromResult(Users.FirstOrDefault(u => u.Id == id));

        public Task<User> CreateUserAsync(User user)
        {
            user.Id = Users.Max(u => u.Id) + 1;
            user.PasswordHash = _passwordService.HashPassword(user.PasswordHash);
            Users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User> AuthenticateUserAsync(string email, string password)
        {
            var user = Users.FirstOrDefault(u => u.Email == email);
            if (user != null && _passwordService.VerifyPassword(password, user.PasswordHash))
            {
                user.LastLoginDate = DateTime.Now;
                user.FailedLoginAttempts = 0;
                return Task.FromResult(user);
            }

            if (user != null)
            {
                user.FailedLoginAttempts++;
            }
            return Task.FromResult<User>(null);
        }
    }
}