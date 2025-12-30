using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.Users;

namespace Luqma.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void GetUsersForFinanceMapping()
        {
            CreateMap<LuqmaUser, GetUsersForFinanceResponse>()
                .ForMember(to => to.Id, from => from.MapFrom(src => src.Id))
                .ForMember(to => to.Name, from => from.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}