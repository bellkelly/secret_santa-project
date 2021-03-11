using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Application.Members.DTOs;
using SecretSanta.Application.Members.Queries.GetMember;

namespace SecretSanta.WebAPI.Controllers
{
    public class MemberController : ApiControllerBase
    {
        [HttpGet("{userName}")]
        public async Task<ActionResult<MemberDto>> Get(string userName) =>
            await Mediator.Send(new GetMemberQuery {UserName = userName});
    }
}
