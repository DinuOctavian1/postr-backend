using Application.Authentication.Common;
using Contracts.Authentication;
using Mapster;

namespace API.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Message, src => src.Message)
                .Map(dest => dest, src => src.User)
                .IgnoreNullValues(true);
        }
    }
}
