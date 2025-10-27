using Luqma.Core;
using Luqma.Core.Bases;
using Luqma.Core.Middlewares;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities.Identity;
using Luqma.Data.Helpers;
using Luqma.Infrastructure;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.Seeder;
using Luqma.Service;
using Luqma.Service.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Text.Json.Serialization;

namespace Luqma.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Initialize Encryption Key
            var encryptionSettings = new EncryptionSettings();
            builder.Configuration.GetSection(nameof(EncryptionSettings)).Bind(encryptionSettings);
            EncryptionHelper.Initialize(encryptionSettings.Key);
            #endregion
            #region SQL Srver Connection
            builder.Services.AddDbContext<LuqmaDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("LuqmaConnection"))
                .UseLazyLoadingProxies();
            });
            #endregion
            #region Dependancy injection
            builder.Services.AddModuleInfrastructureServices(builder.Configuration)
                            .AddModuleServiceServices()
                            .AddModuleCoreServices();
            #endregion
            #region AllowCORS
            var CORS = "_cors";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CORS,
                                  policy =>
                                  {
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                      policy.AllowAnyOrigin();
                                  });
            });
            #endregion
            // Add services to the container.

            builder.Services.AddControllers()
             .AddJsonOptions(options =>
             {

                 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());


                 options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
             });
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(
                                e => e.ErrorMessage
                                ).ToArray()
                        );
                    return new BadRequestObjectResult(new ApiResponse()
                    {
                        Errors = errors,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Succeeded = false,
                        Message = SharedResponseKeys.ValidationFailed
                    });
                };
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "enter JWT token like this: Bearer {your token}"
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, Array.Empty<string>()
                    }
                });
            });
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddSingleton<IHostEnvironment>(sp => sp.GetRequiredService<IWebHostEnvironment>());
            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(24);
            });
            builder.Services.AddResponseCaching();
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<LuqmaUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<LuqmaRole>>();
                await RoleSeeder.SeedAsync(roleManager);
                await UserSeeder.SeedAsync(userManager);
            }
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:StripeKey"];
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseResponseCaching();
            app.UseCors(CORS);
            app.UseMiddleware<ErrorHandlerMiddleWare>();
            app.UseMiddleware<TokenValidationMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
