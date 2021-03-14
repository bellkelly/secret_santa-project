using System.Threading.Tasks;
using Xunit;
using static Application.IntegrationTests.IntegrationTestsFixture;

namespace Application.IntegrationTests
{
    /// <summary>
    /// A base class for integration tests.
    /// Resets the state of the database at the end of each test.
    /// </summary>
    [Collection("Integration collection")]
    public class IntegrationTestBase : IAsyncLifetime
    {
        public async Task InitializeAsync() => await ResetState();

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
