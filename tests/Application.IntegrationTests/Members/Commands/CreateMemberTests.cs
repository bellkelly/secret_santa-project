using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using SecretSanta.Application.Common.Exceptions;
using SecretSanta.Application.Members.Commands;
using SecretSanta.Infrastructure.Identity;
using Xunit;

namespace Application.IntegrationTests.Members.Commands
{
    using static IntegrationTestsFixture;

    public class CreateMemberTests : IntegrationTestBase
    {
        [Fact]
        public void ShouldRequireUserName()
        {
            var command = new CreateMemberCommand();
            var expectedErrors = new Dictionary<string, string[]>
            {
                {"UserName", new[] {"UserName is required."}}, {"Password", new[] {"Password is required."}}
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>().Result
                .WithMessage("One or more validation failures have occurred.").And.Errors.Should()
                .BeEquivalentTo(expectedErrors);
        }

        [Fact]
        public async Task ShouldRequireUniqueUser()
        {
            const string duplicateUserName = "Foo";
            await AddAsync(new ApplicationUser { UserName = duplicateUserName });
            var command = new CreateMemberCommand { UserName = duplicateUserName, Password = "Test123!" };
            var expectedErrors = new Dictionary<string, string[]>
            {
                {"UserName", new[] {"The specified UserName already exists."}}
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>().Result
                .WithMessage("One or more validation failures have occurred.").And.Errors.Should()
                .BeEquivalentTo(expectedErrors);
        }

        [Fact]
        public async Task ShouldCreateUser()
        {
            var command = new CreateMemberCommand { UserName = "TestUser", Password = "Test123!" };

            var userId = await SendAsync(command);

            var user = await FindAsync<ApplicationUser>(userId);
            user.Should().NotBeNull();
            user.UserName.Should().Be(command.UserName);
        }
    }
}
