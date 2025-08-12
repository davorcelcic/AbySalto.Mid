
using AbySalto.Mid.Application;
using AbySalto.Mid.Infrastructure;
using AbySalto.Mid.WebApi.Data;
using AbySalto.Mid.WebApi.Models;
using AbySalto.Mid.WebApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext with InMemory provider
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

            // Add authentication services
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/Account/Login";
                });

            // Register services
            builder.Services.AddScoped<UserService>();

            builder.Services
                .AddPresentation()
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Desk Link");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            // Seed data
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                SeedDataAsync(context).GetAwaiter().GetResult(); ;
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            // Method to inser few users for testing purposes
            async Task SeedDataAsync(ApplicationDbContext context)
            {
                if (await context.Users.AnyAsync())
                    return;
                var user = new User
                {
                    UserName = "korisnik1",
                    PasswordHash = PasswordHashGenerator.GeneratePasswordHash("korisnik1"),
                    Email = "korisnik1@gmail.com"
                };
                context.Users.Add(user);
                var user2 = new User
                {
                    UserName = "korisnik2",
                    PasswordHash = PasswordHashGenerator.GeneratePasswordHash("korisnik2"),
                    Email = "korisnik2@gmail.com"
                };
                context.Users.Add(user2);
                await context.SaveChangesAsync();
            }
        }
    }

    public class PasswordHashGenerator
    {
        public static string GeneratePasswordHash(string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            // You can pass null or an object representing your user if needed
            var hashedPassword = passwordHasher.HashPassword(null, password);
            return hashedPassword;
        }
    }
}
