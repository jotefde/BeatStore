using Autofac;
using Autofac.Extensions.DependencyInjection;
using BeatStore.API.Context;
using BeatStore.API.DTO;
using BeatStore.API.Extensions;
using BeatStore.API.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using System.Text;

namespace BeatStore.API
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223hg45645h5gsedrfgkuiRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public IConfiguration Configuration { get; }
        public ILifetimeScope? AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new UseCaseBuilder());
            builder.RegisterModule(new RepositoryBuilder());
            builder.RegisterModule(new ServiceBuilder());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                            async context =>
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                                var error = context.Features.Get<IExceptionHandlerFeature>();
                                if (error != null)
                                {
                                    await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                                    //await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                                }
                            });
                });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BeatStore API V1");
            });

            app.UseSwagger();
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            UrlHelperExtension.Configure(httpContextAccessor);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Add framework services.
            var connectionString = Configuration.GetValue<string>("ConnectionStrings");
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));

            /*services.AddCors(c =>
            {
                c.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });*/

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var objectStorageOptions = Configuration.GetSection(nameof(ObjectStorageOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            // Configure ObjectStorageOptions
            services.Configure<ObjectStorageOptions>(options =>
            {
                options.ConnectionString = objectStorageOptions[nameof(ObjectStorageOptions.ConnectionString)];
                options.ObjectStorageBaseUrl = objectStorageOptions[nameof(ObjectStorageOptions.ObjectStorageBaseUrl)];
                options.AccessKey = objectStorageOptions[nameof(ObjectStorageOptions.AccessKey)];
                options.SecretKey = objectStorageOptions[nameof(ObjectStorageOptions.SecretKey)];
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SuperBET API", Version = "v1" });
            });

            services.AddControllers();
        }
    }
}
