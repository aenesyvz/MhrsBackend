using System.Reflection;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Core.Security;
using Core.Security.JWT;
using Application.Services.Appointments;
using Application.Services.AppointmentTimes;
using Application.Services.Cities;
using Application.Services.Diseases;
using Application.Services.Districts;
using Application.Services.Hospitals;
using Application.Services.MedicineCompanies;
using Application.Services.Medicines;
using Application.Services.Polyclinics;
using Application.Services.PrescriptionDetails;
using Application.Services.Prescriptions;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        MailSettings mailSettings,
        FileLogConfiguration fileLogConfiguration,
        TokenOptions tokenOptions
       // ElasticSearchConfig elasticSearchConfig
    )
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMailService, MailKitMailService>(_ => new MailKitMailService(mailSettings));
        services.AddSingleton<ILogger, SerilogFileLogger>(_ => new SerilogFileLogger(fileLogConfiguration));
       // services.AddSingleton<IElasticSearch, ElasticSearchManager>(_ => new ElasticSearchManager(elasticSearchConfig));

        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IAppointmentsService, AppointmentsManager>();
        services.AddScoped<IAppointmentTimesService, AppointmentTimesManager>();
        services.AddScoped<ICitiesService, CitiesManager>();
        services.AddScoped<IDiseasesService, DiseasesManager>();
        services.AddScoped<IDistrictsService, DistrictsManager>();
        services.AddScoped<IHospitalsService, HospitalsManager>();
        services.AddScoped<IMedicineCompaniesService, MedicineCompaniesManager>();
        services.AddScoped<IMedicinesService,MedicinesManager>();
        services.AddScoped<IPolyclinicsService, PolyclinicsManager>();
        services.AddScoped<IPrescriptionDetailsService, PrescriptionDetailsManager>();
        services.AddScoped<IPrescriptionsService, PrescriptionsManager>();
      

       // services.AddYamlResourceLocalization();

        services.AddSecurityServices<Guid, int,Guid>(tokenOptions);

        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
