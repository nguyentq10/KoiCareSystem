using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserRepository
    {
        private KoiCareSystemContext? _context;

        public User? GetOne(string email, string password)
        {
            _context = new();
            return _context.Users.FirstOrDefault(x => x.Email.ToLower() == email && x.Password == password);
        }

        public User? GetOneByEmail(string email)
        {
            _context = new();
            return _context.Users.FirstOrDefault(x => x.Email.ToLower() == email);
        }

        public void AddUser(User user)
        {
            _context = new();
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user) {
            _context = new();
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context = new();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            _context = new();
            return _context.Users.ToList();
        }

        public User? GetUserById(string userId)
        {
            _context = new();
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }
    }
}
