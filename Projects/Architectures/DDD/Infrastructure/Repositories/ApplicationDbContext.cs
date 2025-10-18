using Domain.Interfaces.Db;
using Domain.Interfaces.Db.DbContext;
using Domain.Models.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

//! Зарегистрировать
public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    //!
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<Customer> Customers { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //!
        //var domainEvents = ChangeTracker.Entries<Entity>()
        //    .Select(e => e.Entity)
        //    .Where(e => e.GetDomainEvents().Any())
        //    .SelectMany(e => e.GetDomainevents());

        var result = await base.SaveChangesAsync(cancellationToken);

        //foreach (var domainEvent in domainEvents)
        //{
        //    await _publisher.Publish(domainEvent, cancellationToken);
        //}

        return result;
    }
}
