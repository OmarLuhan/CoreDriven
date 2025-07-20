using CoreDriven.Data.Entities;

namespace CoreDriven.Data.Repositories;
public interface IUserRepository:IGenericRepository<User>
{
   Task DeleteAsync(Guid id);
}
public class UserRepository(Entities.AppContext context) :GenericRepository<User>(context), IUserRepository
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
}