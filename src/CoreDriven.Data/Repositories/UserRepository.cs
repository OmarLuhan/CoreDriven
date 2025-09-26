using CoreDriven.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreDriven.Data.Repositories;
public interface IUserRepository:IGenericRepository<User>
{
   Task DeleteAsync(Guid id);
}

public class UserRepository(Entities.AppContext context) : GenericRepository<User>(context), IUserRepository
{
   private readonly Entities.AppContext _context = context;


   public async Task DeleteAsync(Guid id)
   {
      User? entity = Query().FirstOrDefault(u => u.Id == id);
      if (entity != null)
      {
         _context.Users.Remove(entity);
         await _context.SaveChangesAsync();
      }

      throw new KeyNotFoundException($"User with ID {id} not found.");
   }

  /// <summary>
  /// Obtiene una lista de usuarios del sistema: 
  /// ejecuta un procedimiento almacenado
  /// 
  /// </summary>
  /// <param name="pageNumber"></param>
  /// <param name="pageSize"></param>
  /// <param name="sortBy"></param>
  /// <param name="sortDesc"></param>
  /// <param name="value"></param>
  /// <returns> IAsyncEnumerable de tipo usuario</returns>
   public IAsyncEnumerable<User> GetAsyncQuery(int pageNumber, int pageSize, string sortBy,
      bool sortDesc, string? value=null)
   {
      var list = _context.Database.SqlQuery<User>($"""
                                                         EXEC GetUsers
                                                              @PageNumber = {pageNumber}
                                                              @PageSize = {pageSize}
                                                              @SortBy = {sortBy}
                                                              @SortDesc = {sortDesc}
                                                              @Value = {value}, 
                                                         """).AsAsyncEnumerable();
      return list;
   }
   public async Task<List<User>> GetAsyncList(int pageNumber, int pageSize, string sortBy,
      bool sortDesc, string? value=null)
   {
      var list = await _context.Database.SqlQuery<User>($"""
                                                   EXEC GetUsers
                                                        @PageNumber = {pageNumber}
                                                        @PageSize = {pageSize}
                                                        @SortBy = {sortBy}
                                                        @SortDesc = {sortDesc}
                                                        @Value = {value}, 
                                                   """).ToListAsync();
      return list;
   }
}