using System.Security.Claims;

namespace WebApp.UserInterface.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetMemberLoginNameByClaim(this ClaimsPrincipal principal)
        {
            var memberLoginName = (principal.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new Exception("Cannot get memberloginName from token");
            return memberLoginName;
        }
    }
}
