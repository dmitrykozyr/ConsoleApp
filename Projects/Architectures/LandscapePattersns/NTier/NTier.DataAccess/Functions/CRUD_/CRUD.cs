using Microsoft.EntityFrameworkCore;
using NTier.DataAccess.DataContext;
using NTier.DataAccess.Functions.Interfaces;

namespace NTier.DataAccess.Functions.CRUD_
{
    public class CRUD : ICRUD
    {
        public async Task<T> Create<T>(T objectForDb) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    await context.AddAsync<T>(objectForDb);
                    await context.SaveChangesAsync();
                    return objectForDb;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<T> Read<T>(long entityId) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    T result = await context.FindAsync<T>(entityId);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<T>> ReadAll<T>() where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    var result = await context.Set<T>().ToListAsync();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> Update<T>(T objectToUpdate, Int64 entityId) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    var objectFound = await context.FindAsync<T>(entityId);
                    if (objectFound != null)
                    {
                        context.Entry(objectFound).CurrentValues.SetValues(objectToUpdate);
                        await context.SaveChangesAsync();
                    }

                    return objectFound;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete<T>(long entityId) where T : class
        {
            try
            {
                using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    T recordToDelete = await context.FindAsync<T>(entityId);
                    if (recordToDelete != null)
                    {
                        context.Remove(recordToDelete);
                        await context.SaveChangesAsync();
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
