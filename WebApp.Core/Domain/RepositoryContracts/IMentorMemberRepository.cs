using WebApp.Core.Domain.Entities;

namespace WebApp.Core.Domain.RepositoryContracts
{
    public interface IMentorMemberRepository
    {
        Task<MemberMentor> GetMentors(string memberLoginName);
    }
}
