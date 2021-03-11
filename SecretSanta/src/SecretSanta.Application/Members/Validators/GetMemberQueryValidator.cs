using FluentValidation;

namespace SecretSanta.Application.Members.Queries.GetMember
{
    public class GetMemberQueryValidator : AbstractValidator<GetMemberQuery>
    {
        public GetMemberQueryValidator()
        {
            RuleFor(query => query.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
}
