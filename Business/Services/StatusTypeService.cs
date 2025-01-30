﻿using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;
    //CREATE
    public async Task<StatusType> CreateStatusTypeAsync(StatusTypeRegistrationForm form)
    {
        var enttiy = await _statusTypeRepository.GetAsync(x => x.StatusName == form.StatusName);
        enttiy ??= await _statusTypeRepository.CreateAsync(StatusTypeFactory.Create(form));

        return StatusTypeFactory.Create(enttiy);
    }
    //READ
    public async Task<IEnumerable<StatusType>> GetAllStatusTypesAsync()
    {
        var entties = await _statusTypeRepository.GetAllAsync();
        var StatusTypes = entties.Select(StatusTypeFactory.Create);
        return StatusTypes ?? [];
    }
    public async Task<StatusType> GetStatusTypeAsync(Expression<Func<StatusTypeEntity, bool>> expression)
    {

        var enttiy = await _statusTypeRepository.GetAsync(expression);
        var StatusType = StatusTypeFactory.Create(enttiy);
        return StatusType ?? null!;
    }
    //UPDATE
    public async Task<StatusType> UpdateStatusTypeAsync(StatusTypeUpdateForm form)
    {
        var updateEntity = StatusTypeFactory.Create(form);
        var entity = await _statusTypeRepository.UpdateAsync(p => p.Id == form.Id, updateEntity);
        var StatusType = StatusTypeFactory.Create(entity);
        return StatusType ?? null!;
    }
    //DELETE
    public async Task<bool> DeleteStatusTypeAsync(int id)
    {
        var result = await _statusTypeRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}