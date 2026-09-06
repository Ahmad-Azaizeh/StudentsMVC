using Students.Application.Interfaces;
using Students.Application.Models;
using Students.Domain.Constants;
using Students.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Students.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _users;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IRepository<User> users, IPasswordHasher passwordHasher)
        {
            _users = users;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserModel?> ValidateCredentialsAsync(string username, string password)
        {
            var user = (await _users.FindAsync(u => u.Username == username));

            if (user is null)  
                return null; 

            if(!_passwordHasher.Verify(password, user.PasswordHash))
                return null;
            
            return new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            };
        }

        public async Task<AuthResult> RegisterAsync(RegisterModel model)
        {
            var exists = await _users.FindAsync(u => u.Username == model.Username);

            if(exists != null)
                return new AuthResult(false, "This username already exists.");

            var user = new User
            {
                Username = model.Username,
                PasswordHash = _passwordHasher.Hash(model.Password),
                Role = Roles.User
            };

            await _users.AddAsync(user);
            await _users.SaveChangesAsync();

            return new AuthResult(true);
        }
    }
}
