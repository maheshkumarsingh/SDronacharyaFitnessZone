namespace WebApp.Core.Helpers
{
    public class PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalpages)
    {
        public int CurrentPage { get; set; } = currentPage;
        public int TotalPages { get; set; } = totalpages;
        public int PageSize { get; set; } = itemsPerPage;
        public int TotalCount { get; set; } = totalItems;
    }
}
