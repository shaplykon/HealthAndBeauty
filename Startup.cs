using HealthAndBeauty.Data;
using HealthAndBeauty.Hubs;
using HealthAndBeauty.Services;
using HealthAndBeauty.Services.Mail;
using HealthAndBeauty.Services.UserConnections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HealthAndBeauty
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddRepositories();

            services.AddSingleton<IUserConnectionManager, UserConnectionManager>();
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddSingleton<IMailService, MailService>();

            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));

            services.AddSignalR().AddAzureSignalR("Endpoint=https://hb-notification.service.signalr.net;AccessKey=JegWI1Py0BuzvpV3WVXuTANKcUwmC8g17tkBmztjpDU=;Version=1.0;");

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => 
            { 
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true; 
                
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddControllersWithViews();

            services.AddMvc();
            services.AddProgressiveWebApp(
                options: new WebEssentials.AspNetCore.Pwa.PwaOptions
                {
                    RoutesToPreCache = "/BodyCalculator, /",
                    OfflineRoute="/offline.html"
                }, 
                manifestFileName:"manifest.webmanifest");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseFileServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<NotificationHub>("/notification");

            });
        }
    }
}
