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

            TypeAdapterConfig<ApplicationUser, UserResponse>
                .NewConfig()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");

            // Healthcare Models Mappings
            TypeAdapterConfig<Member, UserResponse>
                .NewConfig();

            TypeAdapterConfig<Doctor, UserResponse>
                .NewConfig();

            TypeAdapterConfig<MedicalCenter, UserResponse>
                .NewConfig();

            TypeAdapterConfig<Caregiver, UserResponse>
                .NewConfig();
        }
    }
}

