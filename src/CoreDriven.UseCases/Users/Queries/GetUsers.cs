using CoreDrive.Utils.Pagination;
using CoreDrive.Utils.Spec;
using CoreDriven.Data.Repositories;
using CoreDriven.Dto.Users;
using CoreDriven.UseCases.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CoreDriven.UseCases.Users.Queries;

public class GetUsers(IUserRepository repository) 
{
    public async Task<PagedList<UserDto>> Execute(BaseQueryParams bqp)
    {
        ArgumentNullException.ThrowIfNull(bqp);
        var query = repository.Query();
        if (!string.IsNullOrEmpty(bqp.Value))
        {
            string search = bqp.Value.Trim();
            query = query.Where(p => p.Name.Contains(search));
        }
        query = bqp.SortDesc
            ? query.OrderByDescending(u => EF.Property<object>(u, bqp.SortBy))
            : query.OrderBy(u => EF.Property<object>(u, bqp.SortBy));
        var queryDto = query.Include(u => u.Role)
            .Select(u => u.ToDto());
        return await PagedList<UserDto>.ToPagedList(
            queryDto,
            bqp.PageNumber,
            bqp.PageSize
        );
    }
}