using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using SecretSanta.Application.Common.Exceptions;
using SecretSanta.Application.Users.Commands;
using SecretSanta.Domain.Entities;
using Xunit;

namespace Application.IntegrationTests.Users.Commands
{
    using static IntegrationTestsFixture;

    public class CreateUserTests : IntegrationTestBase
    {
        [Fact]
        public void ShouldRequireFields()
        {
            var command = new CreateUserCommand();
            var expectedErrors = new Dictionary<string, string[]>
            {
                {"UserName", new[] {"UserName is required."}}, {"Password", new[] {"Password is required."}}, {"Email", new[] {"Email is required."}}
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>().Result
                .WithMessage("One or more validation failures have occurred.").And.Errors.Should()
                .BeEquivalentTo(expectedErrors);
        }

        [Fact]
        public async Task ShouldRequireUniqueUser()
        {
            const string duplicateUserName = "Foo";
            const string duplicateEmail = "foo@localhost";
            await CreateUserAsync(new User { UserName = duplicateUserName, Email = duplicateEmail });
            var command = new CreateUserCommand { UserName = duplicateUserName, Password = "Test123!", Email = duplicateEmail };
            var expectedErrors = new Dictionary<string, string[]>
            {
                {"UserName", new[] {"The specified UserName already exists."}},
                {"Email", new[] {"The specified Email already exists."}}
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>().Result
                .WithMessage("One or more validation failures have occurred.").And.Errors.Should()
                .BeEquivalentTo(expectedErrors);
        }

        [Fact]
        public void ShouldRequireValidEmail()
        {
            var command = new CreateUserCommand { UserName = "Foo", Email = "foo", Password = "Test123!" };
            var expectedErrors = new Dictionary<string, string[]>
            {
                {"Email", new[] {"A valid email address is required."}}
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>().Result
                .WithMessage("One or more validation failures have occurred.").And.Errors.Should()
                .BeEquivalentTo(expectedErrors);
        }

        [Theory]
        [InlineData(7)]
        [InlineData(65)]
        public void ShouldRequireValidPassword(int passwordLength)
        {
            var password = new string('x', passwordLength);
            var command = new CreateUserCommand { UserName = "Foo", Email = "foo@localhost", Password = password };
            var expectedErrors = new Dictionary<string, string[]>
            {
                {"Password", new[] {"Password must be between 8 and 64 characters."}}
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>().Result
                .WithMessage("One or more validation failures have occurred.").And.Errors.Should()
                .BeEquivalentTo(expectedErrors);
        }

        [Fact]
        public async Task ShouldCreateUser()
        {
            var command = new CreateUserCommand { UserName = "TestUser", Password = "Test123!", Email = "Test@example.com" };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Should().Be(command.UserName);
        }
    }
}
