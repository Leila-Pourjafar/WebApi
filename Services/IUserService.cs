using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.ViewModel;
using Api.Models;
namespace Api.Services
{
    public interface IUserService
    {
        bool IsValidUser(UserModel user);
    }
    public class UserService : IUserService
    {
        private readonly TodoContext _context;
        public UserService(TodoContext context)
        {
            _context = context;
        }
        public bool IsValidUser(UserModel user)
        {
            return _context.Users.Any(x => x.UserName.ToLower().Equals(user.Username.ToLower()) && x.PasswordHash.Equals(user.Password));
        }
    }
}
