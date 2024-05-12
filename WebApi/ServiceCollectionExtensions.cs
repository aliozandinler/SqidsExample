using Sqids;
using WebApi.Json;
using WebApi.Mvc;

namespace WebApi;

public static class ServiceCollectionExtensions
{
    public static void AddSqids(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton(x =>
        {
            SqidsOptions sqidsOptions = configuration.GetSection("Sqids").Get<SqidsOptions>() ?? new SqidsOptions();
            return new SqidsEncoder<int>(new SqidsOptions
            {
                Alphabet = sqidsOptions.Alphabet,
                MinLength = sqidsOptions.MinLength,
                BlockList = sqidsOptions.BlockList
            });
        });
        services.AddMvcCore().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new ServiceProviderJsonConverter(new HttpContextAccessor(), services.BuildServiceProvider())));
        services.PostConfigure<RouteOptions>(x => x.ConstraintMap.Add("sqids", typeof(SqidsRouteConstraint)));
    }
}