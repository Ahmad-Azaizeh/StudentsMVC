using Students.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Students.Application.Interfaces
{
    public record AuthResult(bool Success, string? Error = null);
    public interface IAuthService
    {
        Task<UserModel?> ValidateCredentialsAsync(string username, string password);

        Task<AuthResult> RegisterAsync(RegisterModel model);
    }
}
