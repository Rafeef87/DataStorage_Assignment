using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync(UserRegistrationForm form);
    Task<IEnumerable<User?>> GetAllUsersAsync();
    Task<User?> GetUserAsync(int id);
    Task<User?> GetUserAsync(string firstName);
    Task<bool> UpdateUserAsync(UserUpdateForm from);
    Task<bool> DeleteUserAsync(int id);
}