using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SecretSanta.Application.Common.Interfaces;
using SecretSanta.Application.Users.Commands;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Users.Validators
{
    /// <summary>
    /// Validate the creation of a new <see cref="User" />
    /// </summary>
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserDbContext _userDbContext;

        public CreateUserValidator(IUserDbContext userDbContext)
        {
            _userDbContext = userDbContext;

            RuleFor(command => command.UserName).NotEmpty().WithMessage("UserName is required.");

            RuleFor(command => command.Email).NotEmpty().WithMessage("Email is required.");

            RuleFor(command => command.Password).NotEmpty().WithMessage("Password is required.");

            When(command => !string.IsNullOrEmpty(command.UserName), () => {
                RuleFor(command => command.UserName)
                    .MustAsync(BeUniqueUserName).WithMessage("The specified UserName already exists.");
            });

            When(command => !string.IsNullOrEmpty(command.Email), () => {
                RuleFor(command => command.Email)
                    .EmailAddress().WithMessage("A valid email address is required.")
                    .MustAsync(BeUniqueEmail).WithMessage("The specified Email already exists.");
            });

            When(command => !string.IsNullOrEmpty(command.Password), () => {
                RuleFor(command => command.Password)
                    .Length(8, 64).WithMessage("Password must be between 8 and 64 characters.");
            });
        }

        private async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
        {
            var member = await _userDbContext.FindByUsernameAsync(userName);
            return member == null;
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var member = await _userDbContext.FindByEmailAsync(email);
            return member == null;
        }
    }
}
