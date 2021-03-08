using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SecretSanta.Application.Common.Interfaces;
using SecretSanta.Application.Members.Commands;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Members.Handlers
{
    /// <summary>
    ///     Handle the creation of a new <see cref="Member" /> given a <see cref="CreateMemberCommand" />.
    /// </summary>
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, string>
    {
        private readonly IMemberService _memberService;

        public CreateMemberCommandHandler(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<string> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var result = await _memberService.CreateMemberAsync(request.UserName, request.Password);
            return result.UserId;
        }
    }
}