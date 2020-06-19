using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiToken.Entities;
using WebApiToken.Helpers;

namespace WebApiToken.Services
{

    public interface IUserService
    {
        User Authenticate(string username, string password);
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }

    public class UserService : IUserService
    {

        // Data access
        private DataContext _context;

        // Constructor
        public UserService(DataContext context)
        {
            _context = context;
        }



        // Return all users
        public Task<List<User>> GetAll()
        {
            return _context.Users.ToListAsync();
        }

        // Return one user
        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }



        // Authenticate user
        public User Authenticate(string username, string password)
        {

            // Catch empty parameter
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            // Try to find user
            var user = _context.Users.SingleOrDefault(u => u.Username == username);

            // Check if username exists
            if (user == null)
                return null;

            // Check if password is correct
            if (!VerifyPasswordHash(password, user.Password, user.PasswordSalt))
                return null;

            // Authentication successful
            return user;

        }


        // Create a user
        public User Create(User user, string password)
        {

            // Validate input
            if (string.IsNullOrWhiteSpace(password))
                throw new ApiException("Password is required");

            // Check if already exists
            if (_context.Users.Any(x => x.Username == user.Username))
                throw new ApiException("Username \"" + user.Username + "\" is already taken");

            // Create password hash and salt
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Add hash and salt to newly created user account
            user.Password = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Add user account to database
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;

        }

        // Update a user
        public void Update(User user, string password = null)
        {
            throw new NotImplementedException();
        }

        // Delete a user
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }



        // Create hashed password
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            // Validate parameter
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            // Create hash
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // Verify hashed password
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {

            // Validate parameter
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            // Try to verify Hash
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
