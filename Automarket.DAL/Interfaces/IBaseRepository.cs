using Automarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Automarket.DAL.Interfaces;

public interface IBaseRepository<T>
{
    Task Create(T entity);
    
   IQueryable<T> GetAll();
   Task Delete(T entity);

    Task<T> Update(T entity);
}