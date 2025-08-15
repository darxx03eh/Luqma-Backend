using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Data.Entities.Identity;

namespace Luqma.Core.Mapping.Authentications
{
    public partial class AuthenticationProfile
    {
        public void SignUpMapping()
        {
            CreateMap<SignUpCommand, LuqmaUser>()
                .ForMember(to => to.FirstName, from => from.MapFrom(user => user.FirstName))
                .ForMember(to => to.LastName, from => from.MapFrom(user => user.LastName))
                .ForMember(to => to.Email, from => from.MapFrom(user => user.Email))
                .ForMember(to => to.UserName, from => from.MapFrom(user => user.UserName))
                .ForMember(to => to.BirthDate, from => from.MapFrom(user => user.BirthDate))
                .ForMember(to => to.Gender, from => from.MapFrom(user => user.Gender))
                .ForMember(to => to.Salary, from => from.MapFrom(user => user.Salary));
        }
    }
}
