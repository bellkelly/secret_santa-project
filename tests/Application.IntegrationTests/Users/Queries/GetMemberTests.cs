using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using SecretSanta.Application.Common.Exceptions;
using SecretSanta.Application.Users.DTOs;
using SecretSanta.Application.Users.Queries;
using SecretSanta.Domain.Entities;
using Xunit;

namespace Application.IntegrationTests.Users.Queries
{
    using static IntegrationTestsFixture;

    public class GetMemberTests : IntegrationTestBase
    {
        [Fact]
        public void ShouldRequireUserName()
        {
            var query = new GetUserQuery();
            var expectedErrors = new Dictionary<string, string[]> { { "UserName", new[] { "UserName is required." } } };

            FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>().Result
                .WithMessage("One or more validation failures have occurred.").And.Errors.Should()
                .BeEquivalentTo(expectedErrors);
        }

        [Fact]
        public async Task ShouldGetUser()
        {
            const string userName = "Foo";
            await CreateUserAsync(new User { UserName = userName, Email = "foo@localhost" });
            var query = new GetUserQuery { UserName = userName };

            var member = await SendAsync(query);

            member.Should().BeOfType<UserDto>();
            member.UserName.Should().Be(userName);
        }

        [Fact]
        public void ShouldRaiseNotFoundIfUserDoesNotExist()
        {
            const string userName = "Foo";
            var query = new GetUserQuery { UserName = userName };

            FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<NotFoundException>().Result
                .WithMessage($"Entity \"User\" ({userName}) was not found.");
        }
    }
}
