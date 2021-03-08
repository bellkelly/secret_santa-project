using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SecretSanta.Application.Common.Interfaces;
using SecretSanta.Application.Members.Commands;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Members.Validators
{
    /// <summary>
    ///     Validate the creation of a new <see cref="Member" />
    /// </summary>
    public class CreateMemberValidator : AbstractValidator<CreateMemberCommand>
    {
        private readonly IMemberService _memberService;

        public CreateMemberValidator(IMemberService memberService)
        {
            _memberService = memberService;

            RuleFor(command => command.UserName).NotEmpty().WithMessage("UserName is required.")
                .MustAsync(BeUniqueUserName).WithMessage("The specified UserName already exists.");

            RuleFor(command => command.Password).NotEmpty().WithMessage("Password is required.");
        }

        private async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
        {
            return !await _memberService.MemberExists(userName, cancellationToken);
        }
    }
}