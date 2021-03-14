using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SecretSanta.Application.Common.Exceptions;
using SecretSanta.Application.Common.Interfaces;
using SecretSanta.Application.Members.DTOs;
using SecretSanta.Application.Members.Queries.GetMember;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Members.Handlers
{
    public class GetMemberHandler : IRequestHandler<GetMemberQuery, MemberDto>
    {
        private readonly IMapper _mapper;
        private readonly IMemberService _memberService;

        public GetMemberHandler(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(GetMemberQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberService.GetMemberAsync(request.UserName);

            if (member == null) throw new NotFoundException(nameof(Member), request.UserName);

            var memberDto = _mapper.Map<MemberDto>(member);

            return memberDto;
        }
    }
}
