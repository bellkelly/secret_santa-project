using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Npgsql;
using Respawn;
using SecretSanta.Application.Common.Interfaces;
using SecretSanta.Domain.Entities;
using SecretSanta.Infrastructure.Persistence;
using SecretSanta.WebAPI;

namespace Application.IntegrationTests
{
    /// <summary>
    /// A fixture or setting up a production-like environment for integration tests.
    /// </summary>
    internal class IntegrationTestsFixture : IDisposable
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;

        /// <summary>
        /// Run once at the beginning of all integration tests to configure services.
        /// </summary>
        public IntegrationTestsFixture()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).AddEnvironmentVariables().Build();

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" && w.ApplicationName == "SecretSanta.WebAPI"));
            services.AddLogging();

            var startup = new Startup(_configuration);
            startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" },
                DbAdapter = DbAdapter.Postgres
            };

            EnsureDatabase();
        }

        public void Dispose()
        {
            DropDatabase();
        }

        /// <summary>
        /// Creates a database if it doesn't already exist and run migrations.
        /// </summary>
        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Sends a <see cref="MediatR" /> <see cref="IRequest" /> asynchronously.
        /// </summary>
        /// <param name="request">The command to send.</param>
        /// <typeparam name="TResponse">The response type expected from a successful command invocation.</typeparam>
        /// <returns></returns>
        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<ISender>();

            return await mediator.Send(request);
        }

        /// <summary>
        /// Asynchronously lookup and return an entity by its primary key(s).
        /// </summary>
        /// <param name="keyValues">The primary key(s) to use when finding the entity.</param>
        /// <typeparam name="TEntity">The entity type to lookup.</typeparam>
        /// <returns></returns>
        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        /// <summary>
        /// Asynchronously lookup and return a <see cref="User"/> with the given username.
        /// </summary>
        /// <param name="userName">The username of a <see cref="User"/>.</param>
        /// <returns></returns>
        public static async Task<User> FindUserAsync(string userName)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUserDbContext>();

            return await context.FindByUsernameAsync(userName);
        }

        /// <summary>
        /// Asynchronously create a <see cref="User"/>.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to create.</param>
        /// <param name="password">A password for the user, defaults to "testPassword".</param>
        /// <returns></returns>
        public static async Task CreateUserAsync(User user, string password="testPassword")
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUserDbContext>();

            await context.CreateAsync(user, password);
        }

        /// <summary>
        /// Create a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <typeparam name="TEntity">The type of the entity being created.</typeparam>
        /// <returns></returns>
        public static async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete the configured database.
        /// </summary>
        private static void DropDatabase()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Reset the configured database.
        /// </summary>
        public static async Task ResetState()
        {
            await using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();
            await _checkpoint.Reset(connection);
        }
    }
}
