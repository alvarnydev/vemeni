using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApiToken.Helpers;
using WebApiToken.Services;


namespace WebApiToken
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add DataContext to access database
            services.AddDbContext<DataContext>(options =>
                options.UseMySQL(_configuration.GetConnectionString("MySQLConnection")));

            // Add controllers to handle api requests
            services.AddControllers();

            // Add mapper to map objects
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add user service
            services.AddScoped<IUserService, UserService>();

            // Configure apiSettings
            var apiSettingsSection = _configuration.GetSection("ApiSettings");
            services.Configure<ApiSettings>(apiSettingsSection);

            // Configure JWT Authentication
            var apiSettings = apiSettingsSection.Get<ApiSettings>();
            var key = Encoding.ASCII.GetBytes(apiSettings.Secret);

            // Add authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    /*
                    x.Events = new JwtBearerEvents
                    {
                        // On successful validation
                        OnTokenValidated = context =>
                        {

                            // Try to find user
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            var userId = int.Parse(context.SecurityToken.Subject);
                            var user = userService.GetById(userId);

                            // Return unauthorized if user doesn't exist
                            if (user == null)
                            {
                                context.Fail("Unauthorized");
                            }

                            // Return success
                            return Task.CompletedTask;
                        }
                    };
                    */

                    // Some options
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;

                    // How to validate token
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key), // Specify how key is to be validated
                        ValidIssuer = apiSettings.Issuer,
                        ValidAudience = apiSettings.Audience
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Find route
            app.UseRouting();

            // Try to authorize
            app.UseAuthentication(); // Who are you?
            app.UseAuthorization(); // Are you allowed to see this?
            
            // Find correct endpoint
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
