using Autofac;
using Autofac.Extensions.DependencyInjection;
using BeatStore.API.Context;
using BeatStore.API.DTO;
using BeatStore.API.Entities;
using BeatStore.API.Extensions;
using BeatStore.API.Helpers.Enums;
using BeatStore.API.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Converters;
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

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

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

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ApplicationDBContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Add framework services.
            var connectionString = Configuration.GetValue<string>("ConnectionStrings");
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));

            services.AddCors(c =>
            {
                c.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            // Configure ObjectStorageOptions
            var objectStorageOptions = Configuration.GetSection(nameof(ObjectStorageOptions));
            services.Configure<ObjectStorageOptions>(options =>
            {
                options.ConnectionString = objectStorageOptions[nameof(ObjectStorageOptions.ConnectionString)];
                options.ObjectStorageBaseUrl = objectStorageOptions[nameof(ObjectStorageOptions.ObjectStorageBaseUrl)];
                options.AccessKey = objectStorageOptions[nameof(ObjectStorageOptions.AccessKey)];
                options.SecretKey = objectStorageOptions[nameof(ObjectStorageOptions.SecretKey)];
            });

            // Configure JwtIssuerOptions
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
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

            // Configure PayU API options
            var paymentAPIOptions = Configuration.GetSection(nameof(PaymentAPIOptions));
            services.Configure<PaymentAPIOptions>(options =>
            {
                options.client_id = paymentAPIOptions[nameof(PaymentAPIOptions.client_id)];
                options.client_secret = paymentAPIOptions[nameof(PaymentAPIOptions.client_secret)];
                options.notifyUrl = paymentAPIOptions[nameof(PaymentAPIOptions.notifyUrl)];
            });

            // Configure Mailing service options
            var mailingOptions = Configuration.GetSection(nameof(MailingOptions));
            services.Configure<MailingOptions>(options =>
            {
                options.Host = mailingOptions[nameof(MailingOptions.Host)];
                options.Port = Convert.ToInt32(mailingOptions[nameof(MailingOptions.Port)]);
                options.Email = mailingOptions[nameof(MailingOptions.Email)];
                options.DisplayName = mailingOptions[nameof(MailingOptions.DisplayName)];
                options.Password = mailingOptions[nameof(MailingOptions.Password)];
            });

            // add identity
            var identityBuilder = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.SignIn.RequireConfirmedEmail = false;
                o.User.RequireUniqueEmail = false;
            });

            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
            //services.AddDefaultIdentity<AppUser>().AddUserStore<ApplicationDBContext>();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
            });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));
            // order is vital, this *must* be called *after* AddNewtonsoftJson()
            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
