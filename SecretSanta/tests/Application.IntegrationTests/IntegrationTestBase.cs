using System;
using Xunit;

namespace Application.IntegrationTests
{
    /// <summary>
    ///     A base class for integration tests.
    ///     Resets the state of the database at the end of each test.
    /// </summary>
    [Collection("Integration collection")]
    public class IntegrationTestBase : IDisposable
    {
        public async void Dispose()
        {
            await IntegrationTestsFixture.ResetState();
        }
    }
}