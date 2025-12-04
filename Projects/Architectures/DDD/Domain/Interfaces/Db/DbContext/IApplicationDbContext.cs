using Domain.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Domain.Interfaces.DB.DbContext;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }

    //! Что это?
    DatabaseFacade Database {  get; }
}
