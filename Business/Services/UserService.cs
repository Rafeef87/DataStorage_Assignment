using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    //CREATE
    public async Task<bool> CreateUserAsync(UserRegistrationForm form)
    {
        if (await _userRepository.AlreadyExistsAsync(x => x.FirstName == form.FirstName))
            return false;
        await _userRepository.BeginTransactionAsync();
        try
        {
            await _userRepository.AddAsync(new UserEntity { FirstName = form.FirstName });
            await _userRepository.SaveAsync();
            await _userRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _userRepository.RollbackTransactionAsync();
            return false;
        }
    }
    //READ
    public async Task<IEnumerable<User?>> GetAllUsersAsync()
    {
        var entties = await _userRepository.GetAllAsync();
        var users = entties.Select(UserFactory.Create);
        return users ?? [];
    }
    public async Task<User?> GetUserAsync(int id)
    {
        var enttiy = await _userRepository.GetAsync(x => x.Id == id);
        return UserFactory.Create(enttiy!);
    }
    public async Task<User?> GetUserAsync(string firstName)
    {
        var enttiy = await _userRepository.GetAsync(x => x.FirstName == firstName);
        return UserFactory.Create(enttiy!);
    }
    //UPDATE
    public async Task<bool> UpdateUserAsync(UserUpdateForm form)
    {
        if (await _userRepository.AlreadyExistsAsync(x => x.FirstName == form.FirstName))
            return false;
        await _userRepository.BeginTransactionAsync();
        try
        {
            _userRepository.Update(new UserEntity { FirstName = form.FirstName });
            await _userRepository.SaveAsync();
            await _userRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _userRepository.RollbackTransactionAsync();
            return false;
        }
    }
    //DELETE
    public async Task<bool> DeleteUserAsync(int id)
    {
        var entity = await _userRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;
        await _userRepository.BeginTransactionAsync();
        try
        {
            _userRepository.Remove(entity);
            await _userRepository.SaveAsync();
            await _userRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _userRepository.RollbackTransactionAsync();
            return false;
        }
        ;
    }
    public async Task<bool> UserExsistsAsync(string userName)
    {
        var result = await _userRepository.AlreadyExistsAsync(x => x.FirstName == userName);
        return result;
    }
}