using Microsoft.AspNetCore.Http;
using System.Text.Json;
using WebApp.Core.DTOs;
using WebApp.Core.Helpers;

namespace WebApp.UserInterface.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader<T>(this HttpResponse response, PagedList<T> data)
        {
            var paginationHeader = new PaginationHeader(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
