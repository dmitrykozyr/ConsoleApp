using Domain.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure.Interfaces.Db;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }

    //! Что это?
    DatabaseFacade Database {  get; }
}
