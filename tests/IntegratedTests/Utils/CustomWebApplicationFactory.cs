using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetControlSystem.Data.Context;
using System.Net.Http.Headers;

namespace IntegratedTests.Utils;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public HttpClient HttpClient { get; private set; } = null!;
    public string TestToken = null!;
    private readonly IdentityUser _identityUser = AuthConfig.GetValidUser();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings-integrated-tests.json")
                .Build();

            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<MyDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<MyDbContext>();

            db.Database.EnsureCreated();

            db.Users.Add(_identityUser);
            db.SaveChanges();
        });
    }

    public async Task Seed<T>(T entity) where T : class
    {
        using var scope = Services.CreateScope();

        if (entity is IdentityUser)
        { 
            var user = entity as IdentityUser;
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        
            var result = await userManager.CreateAsync(user, user.PasswordHash);
            return;
        }
        var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
        db.Set<T>().Add(entity);
        await db.SaveChangesAsync();        
    }

    public async Task ResetDatabaseAsync()
    {
        var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();
    }

    public async Task InitializeAsync()
    {
        HttpClient = CreateClient();
        TestToken = AuthConfig.GenerateToken(_identityUser);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TestToken);
    }

    public async Task DisposeAsync()
    {
        await ResetDatabaseAsync();
        HttpClient.Dispose();
    }
}