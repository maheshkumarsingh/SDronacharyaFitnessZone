using WebApp.Core.Domain.Entities;

namespace WebApp.Core.ServiceContracts
{
    public interface ITokenService
    {
        public string CreateToken(Member member);
    }
}
