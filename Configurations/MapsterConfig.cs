using Mapster;
using Shifaa.Models;
using Shifaa.DTOs.Response;

namespace Shifaa.Configurations
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfig(this IServiceCollection services)
        {
            // ApplicationUser Mappings
            TypeAdapterConfig<ApplicationUser, ApplicationUserResponse>
                .NewConfig()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");

            TypeAdapterConfig<ApplicationUser, ApplicationUserRequest>
                .NewConfig()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");

            TypeAdapterConfig<ApplicationUser, ApplicationUserResponse>
                .NewConfig()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");

            // Healthcare Models Mappings
            TypeAdapterConfig<Member, ApplicationUserResponse>
                .NewConfig();

            TypeAdapterConfig<Doctor, ApplicationUserResponse>
                .NewConfig();

            TypeAdapterConfig<MedicalCenter, ApplicationUserResponse>
                .NewConfig();

            TypeAdapterConfig<Caregiver, ApplicationUserResponse>
                .NewConfig();
        }
    }
}

