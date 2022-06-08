using AgroHUB.Framework.Infra.ServiceContainer.ContainerRegistry;
using AutoMapper;

namespace AgroHUB.Financial.Registries.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperProfiles(this IContainerRegistry containerRegistry)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AutoMapApplicationFinancial();
            });

            IMapper mapper = config.CreateMapper();
            containerRegistry.RegisterInstance<IMapper>(mapper);
        }
    }
}
