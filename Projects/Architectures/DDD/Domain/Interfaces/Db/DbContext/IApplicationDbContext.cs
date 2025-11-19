using Domain.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Domain.Interfaces.Db.DbContext;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }

    //! Что это?
    DatabaseFacade Database {  get; }
}
