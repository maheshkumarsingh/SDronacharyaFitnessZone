using WebApp.Core.Domain.Entities.Enums;

namespace WebApp.Core.Helpers
{
    public class UsersParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string FirstName { get; set; } = string.Empty;
        public string? CurrentMemberLoginName { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Plan { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string Defaulters { get; set; } = string.Empty;
    }
}
