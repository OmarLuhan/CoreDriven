using System.Text.Json;
using CoreDrive.Utils.Pagination;
using Microsoft.AspNetCore.Http;

namespace CoreDrive.Utils.Response;

public static class ResponseHeader
{
    public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentNullException.ThrowIfNull(metaData);

        response.Headers.Append("x-pagination", JsonSerializer.Serialize(new
        {
            metaData.CurrentPage,
            metaData.PageSize,
            metaData.TotalCount,
            metaData.TotalPages,
            HasPrevious = metaData.CurrentPage > 1,
            HasNext = metaData.CurrentPage < metaData.TotalPages
        }));
    }
}