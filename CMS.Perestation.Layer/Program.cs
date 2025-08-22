using CMS.Data.Access.Layer.Data;
using CMS.Data.Access.Layer.Repository;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.Stripe;
using CMS.Perestation.Layer.DbInitilization;
using CMS.Utitlities.Email;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace CMS.Perestation.Layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Read Connection String From Environment Variable
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if (!string.IsNullOrEmpty(connectionString))
            {
                builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"] = connectionString;
            }

            // Add services to the container
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDbInitilizer, DbInitilizer>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            // Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    options.SlidingExpiration = true;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
                    options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET");
                })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Environment.GetEnvironmentVariable("FACEBOOK_APP_ID");
                    facebookOptions.AppSecret = Environment.GetEnvironmentVariable("FACEBOOK_APP_SECRET");
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Initialize database
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IDbInitilizer>();
                service?.Initilizer();
            }

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
            ).WithStaticAssets();

            app.Run();
        }
    }
}
