using FluentValidation;
using SecretSanta.Application.Users.Queries;

namespace SecretSanta.Application.Users.Validators
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(query => query.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
}
