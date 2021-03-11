using Xunit;

namespace Application.IntegrationTests
{
    /// <summary>
    /// A collection of integration tests.
    /// </summary>
    [CollectionDefinition("Integration collection")]
    public class IntegrationTestsCollection : ICollectionFixture<IntegrationTestsFixture>
    {
    }
}
