using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Application.Users.Commands;
using SecretSanta.Application.Users.DTOs;
using SecretSanta.Application.Users.Queries;

namespace SecretSanta.WebAPI.Controllers
{
    public class UserController : ApiControllerBase
    {
        [HttpGet("{userName}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> Get(string userName) =>
            await Mediator.Send(new GetUserQuery { UserName = userName });

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtRoute("GetUser", new {userName = result}, null);
        }
    }
}
