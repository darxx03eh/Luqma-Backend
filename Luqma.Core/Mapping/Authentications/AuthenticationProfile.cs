using AutoMapper;

namespace Luqma.Core.Mapping.Authentications
{
    public partial class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            SignUpMapping();
        }
    }
}
