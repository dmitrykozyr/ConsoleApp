using Domain.Interfaces.Db;
using Domain.Interfaces.Db.DbContext;

namespace Domain.Services.API;

public class DbService : IDbService
{
    IApplicationDbContext _applicationDbContext;

    public DbService(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }


}
