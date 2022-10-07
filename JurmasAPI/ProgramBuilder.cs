using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using JurmasAPI.Stores;
using JurmasAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;

namespace JurmasAPI;

public static class WebAppExtension
{
    #region Logger TraceSources

    private static TraceListener s_listener;

    //log to file
    //have IO permission issue when deployed to IIS
    static void ConfigureTraceSourceLogging()
    {
        if (!File.Exists(APIConfig.LOG_LOCATION))
        {
            File.Create(APIConfig.LOG_LOCATION);
        }

        var writer = File.CreateText(APIConfig.LOG_LOCATION);

        s_listener = new TextWriterTraceListener(writer.BaseStream);
    }

    //log to windows event
    static void ConfigureEventLogTraceSourceLogging()
    {
        if (!EventLog.SourceExists(APIConfig.LOG_SOURCE))
        {
            EventLog.CreateEventSource(APIConfig.LOG_SOURCE, APIConfig.LOG_SOURCE_NAME);
        }

        var Event_Log = new EventLog();
        Event_Log.Source = APIConfig.LOG_SOURCE;
        Event_Log.Log = APIConfig.LOG_SOURCE_NAME;

        s_listener = new EventLogTraceListener(Event_Log);
    }

    #endregion Logger TraceSources

    private static string exceptionAt = string.Empty;

    public static WebApplicationBuilder Initialise(this WebApplicationBuilder builder)
    {
        try
        {
            Console.WriteLine(new string('-', 100));
            Console.WriteLine(">>> Initialise ...");

            exceptionAt = "Adding controllers";
            builder.Services.AddControllers();

            exceptionAt = $"Listening port {APIConfig.HOST_PORT}";
            //used when published as self-contained app
            //comment this out when publishing to a hosting service (IIS/Apache etc.)
            //builder.WebHost.UseKestrel(x =>
            //    x.ListenLocalhost(ServerConfig.SERVER_HOST)
            //);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            exceptionAt = "Swagger setup";
            //Swagger UI only available in debug
            //will be displayed in Swagger UI
            //some information still missing
            builder.Services.AddSwaggerGen(x =>
            {

                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Jurmas",
                    Description = "Jurmas REST API",
                    TermsOfService = new Uri("about:blank"),
                    Contact = new OpenApiContact
                    {
                        Name = "Abraham",
                        Email = "",
                        Url = new Uri("https://rumbledot.github.io"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("about:blank"),
                    }
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                x.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                x.AddSecurityRequirement(securityRequirement);

                x.EnableAnnotations();
            });

            exceptionAt = "Adding event logger";
            //ConfigureTraceSourceLogging();
            ConfigureEventLogTraceSourceLogging();
            builder.Services.AddLogging(b =>
            {
                b.AddConfiguration(builder.Configuration.GetSection("Logging"))
                    .ClearProviders()
                    .SetMinimumLevel(LogLevel.Information)
                    .AddTraceSource(new SourceSwitch("TraceSourceLog", SourceLevels.All.ToString()), s_listener);
#if DEBUG
                b.AddDebug();
#endif
            });

            exceptionAt = "Injecting LogFiler";
            builder.Services.AddSingleton<ILogFiler, LogFiler>();

            exceptionAt = "Injecting TokenService";
            builder.Services.AddSingleton<ITokenService, TokenService>();

            //exceptionAt = "Injecting Exo client";
            //builder.Services.AddHttpClient("exo_client", c =>
            //{
            //    c.BaseAddress = new Uri(APIConfig.EXOAPI_HOST);
            //    c.Timeout = new TimeSpan(APIConfig.API_TIMEOUT);
            //});
            //builder.Services.AddScoped<IExoClient, ExoClient>();

            //exceptionAt = "Injecting SQLIte DB";
            //builder.Services.AddSingleton<IDBClient, DBClient>();

            exceptionAt = "Injecting Authentication";
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = APIConfig.JWT_ISSUER,
                        ValidAudience = APIConfig.JWT_ISSUER,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(APIConfig.JWT_KEY))
                    };
                });

            exceptionAt = "Injecting Authorization";
            builder.Services.AddAuthorization(options =>
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build()
            );

            Console.WriteLine(">>> Initialise completed, building application ...");

            return builder;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Initialise failed at {exceptionAt}\n{ex.Message}\n{ex.StackTrace}");
            Console.WriteLine(new string('-', 100));
            throw;
        }
    }

    public static WebApplication WebApplicationBuilder(this WebApplicationBuilder builder)
    {
        try
        {
            Console.WriteLine(new string('-', 100));
            Console.WriteLine(">>> Building ...");

            exceptionAt = "Building services";
            var provider = builder.Services.BuildServiceProvider();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                exceptionAt = "configure swagger in development";
                IdentityModelEventSource.ShowPII = true;

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                                    $"Jurmas API v1"));
            }

            var logger = provider.GetService<ILogFiler>();

            //var maincontroller = provider.GetRequiredService<IMainController>();
            //exceptionAt = "Checking license";
            //maincontroller.CheckLicense();

            //exceptionAt = "Checking MYOB Exo API";
            //maincontroller.CheckExo();

            app.UseHttpsRedirection();

            exceptionAt = "Initialize Authentication and Authorization";
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            Console.WriteLine(">>> Building completed, Running ...");

            return app;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Builder failed at {exceptionAt}\n{ex.Message}\n{ex.StackTrace}");
            Console.WriteLine(new string('-', 100));
            throw;
        }
    }
}