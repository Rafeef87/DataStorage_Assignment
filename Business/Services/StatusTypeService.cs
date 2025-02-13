using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;
    //CREATE
    public async Task<bool> CreateStatusTypeAsync(StatusTypeRegistrationForm form)
    {
        if (await _statusTypeRepository.AlreadyExistsAsync(x => x.StatusName == form.StatusName))
            return false;
        await _statusTypeRepository.BeginTransactionAsync();
        try
        {
            await _statusTypeRepository.AddAsync(new StatusTypeEntity { StatusName = form.StatusName });
            await _statusTypeRepository.SaveAsync();
            await _statusTypeRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _statusTypeRepository.RollbackTransactionAsync();
            return false;
        }
    }
    //READ
    public async Task<IEnumerable<StatusType?>> GetAllStatusTypesAsync()
    {
        var entties = await _statusTypeRepository.GetAllAsync();
        var statusTypes = entties.Select(StatusTypeFactory.Create);
        return statusTypes ?? [];
    }
    public async Task<StatusType> GetStatusTypeAsync(Expression<Func<StatusTypeEntity, bool>> expression)
    {

        var enttiy = await _statusTypeRepository.GetAsync(expression);
        var statusType = StatusTypeFactory.Create(enttiy!);
        return statusType ?? null!;
    }
    //UPDATE
    public async Task<bool> UpdateStatusTypeAsync(StatusTypeUpdateForm form)
    {
        if (await _statusTypeRepository.AlreadyExistsAsync(x => x.StatusName == form.StatusName))
            return false;
        await _statusTypeRepository.BeginTransactionAsync();
        try
        {
            _statusTypeRepository.Update(new StatusTypeEntity { StatusName = form.StatusName });
            await _statusTypeRepository.SaveAsync();
            await _statusTypeRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _statusTypeRepository.RollbackTransactionAsync();
            return false;
        }
    }
    //DELETE
    public async Task<bool> DeleteStatusTypeAsync(int id)
    {
        var entity = await _statusTypeRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return false;
        await _statusTypeRepository.BeginTransactionAsync();
        try
        {
            _statusTypeRepository.Remove(entity);
            await _statusTypeRepository.SaveAsync();
            await _statusTypeRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _statusTypeRepository.RollbackTransactionAsync();
            return false;
        }
    }
    public async Task<bool> StatusTypeExsistsAsync(string statusName)
    {
        var result = await _statusTypeRepository.AlreadyExistsAsync(x => x.StatusName == statusName);
        return result;
    }
}