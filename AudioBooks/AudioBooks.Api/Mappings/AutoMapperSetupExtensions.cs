using AutoMapper;
using MicroDojoWarrior.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AudioBooks.Api.Mappings
{
    public static class AutoMapperSetupExtensions
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToModelMappingProfile), typeof(ModelToDomainMappingProfile));

            AutoMapperConfig.RegisterMappings();
        }

    }
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToModelMappingProfile());
                cfg.AddProfile(new ModelToDomainMappingProfile());
            });
        }
    }
}
