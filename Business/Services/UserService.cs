using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    //CREATE
    public async Task<User> CreateUserAsync(UserRegistrationForm form)
    {
        var enttiy = await _userRepository.GetAsync(x => x.FirstName == form.FirstName);
        enttiy ??= await _userRepository.CreateAsync(UserFactory.Create(form));

        return UserFactory.Create(enttiy);
    }
    //READ
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var entties = await _userRepository.GetAllAsync();
        var users = entties.Select(UserFactory.Create);
        return users ?? [];
    }
    public async Task<User> GetUserAsync(Expression<Func<UserEntity, bool>> expression)
    {

        var enttiy = await _userRepository.GetAsync(expression);
        var user = UserFactory.Create(enttiy);
        return user ?? null!;
    }
    //UPDATE
    public async Task<User> UpdateUserAsync(UserUpdateForm form)
    {
        var updateEntity = UserFactory.Create(form);
        var entity = await _userRepository.UpdateAsync(p => p.Id == form.Id, updateEntity);
        var user = UserFactory.Create(entity);
        return user ?? null!;
    }
    //DELETE
    public async Task<bool> DeleteUserAsync(int id)
    {
        var result = await _userRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}