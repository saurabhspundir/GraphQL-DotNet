using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using Metadata.WebAPI.GraphQL;
using Metadata.WebAPI.Models;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Filters;

namespace Metadata.WebAPI
{
  [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region GraphQL
            builder.Services.AddSingleton<ISchema, PropertySchema>(services => new PropertySchema(new SelfActivatingServiceProvider(services)));
            builder.Services.AddGraphQL(b => b
              .AddAutoSchema<PropertySchema>()  // schema
              .AddSystemTextJson());   // serializer
            #endregion
            builder.Configuration.AddJsonFile("appsettings.json", true, true);
            builder.Configuration.AddEnvironmentVariables();
      
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            //added to use EnumMember in response for Permissions Enum(Refer Read me)
            .AddNewtonsoftJson(
                opt =>
                {
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
            ;

            builder.Services.Configure<AppSettingsOptions>(builder.Configuration.GetSection("AppSettings"));
            #region Swashbuckle setup
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Financial Instrument Metadata Service",
                    Version = "v1"
                });
                c.ExampleFilters();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme(Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        Array.Empty<string>()
                    }
                });
            });
            builder.Services.AddSwaggerExamples();
            #endregion

            builder.Services.AddHttpContextAccessor();
            builder.Services.ConfigureInternalService(builder.Configuration);
            builder.Services.AddHttpClient();
            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseGraphQLAltair();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL("graphql");
                endpoints.MapGraphQLVoyager("ui/voyager");
            });
            app.Run();
        }
    }
}
