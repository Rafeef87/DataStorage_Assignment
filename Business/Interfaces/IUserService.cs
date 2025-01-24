using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(UserRegistrationForm form);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserAsync(Expression<Func<UserEntity, bool>> expression);
    Task<User> UpdateUserAsync(UserUpdateForm from);
    Task<bool> DeleteUserAsync(int id);
}