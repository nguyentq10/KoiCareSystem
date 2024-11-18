using Repositories.Entities;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserService
    {
        private UserRepository _repo = new();

        public User? Authenticate(string email, string password)
        {
            return _repo.GetOne(email, password);
        }

        public User? GetUserByEmail(string email)
        {
            return _repo.GetOneByEmail(email);
        }

        public void AddUser(User user)
        {
            _repo.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            _repo.UpdateUser(user);
        }

        public void DeleteUser(User user) {
            _repo.DeleteUser(user);
        }

        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }

        public User? GetUserById(string userId)
        {
            return _repo.GetUserById(userId);
        }
    }
}
