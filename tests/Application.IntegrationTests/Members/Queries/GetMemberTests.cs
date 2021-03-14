using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using SecretSanta.Application.Common.Exceptions;
using SecretSanta.Application.Members.DTOs;
using SecretSanta.Application.Members.Queries.GetMember;
using SecretSanta.Infrastructure.Identity;
using Xunit;

namespace Application.IntegrationTests.Members.Queries
{
    using static IntegrationTestsFixture;

    public class GetMemberTests : IntegrationTestBase
    {
        [Fact]
        public void ShouldRequireUserName()
        {
            var query = new GetMemberQuery();
            var expectedErrors = new Dictionary<string, string[]> { { "UserName", new[] { "UserName is required." } } };

            FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>().Result
                .WithMessage("One or more validation failures have occurred.").And.Errors.Should()
                .BeEquivalentTo(expectedErrors);
        }

        [Fact]
        public async Task ShouldGetUser()
        {
            const string userName = "Foo";
            await AddAsync(new ApplicationUser { UserName = userName });
            var query = new GetMemberQuery { UserName = userName };

            var member = await SendAsync(query);

            member.Should().BeOfType<MemberDto>();
            member.UserName.Should().Be(userName);
        }

        [Fact]
        public void ShouldRaiseNotFoundIfUserDoesNotExist()
        {
            const string userName = "Foo";
            var query = new GetMemberQuery { UserName = userName };

            FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<NotFoundException>().Result
                .WithMessage($"Entity \"Member\" ({userName}) was not found.");
        }
    }
}
