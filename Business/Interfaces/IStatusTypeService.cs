using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IStatusTypeService
{
    Task<StatusType> CreateStatusTypeAsync(StatusTypeRegistrationForm form);
    Task<IEnumerable<StatusType>> GetAllStatusTypesAsync();
    Task<StatusType> GetStatusTypeAsync(Expression<Func<StatusTypeEntity, bool>> expression);
    Task<StatusType> UpdateStatusTypeAsync(StatusTypeUpdateForm from);
    Task<bool> DeleteStatusTypeAsync(int id);
}