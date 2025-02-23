﻿using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IStatusTypeService
{
    Task<bool> CreateStatusTypeAsync(StatusTypeRegistrationForm form);
    Task<IEnumerable<StatusType?>> GetAllStatusTypesAsync();
    Task<StatusType?> GetStatusTypeAsync(int id);
    Task<StatusType?> GetStatusTypeAsync(string statusName);
    Task<bool> UpdateStatusTypeAsync(StatusTypeUpdateForm from);
    Task<bool> DeleteStatusTypeAsync(int id);
}