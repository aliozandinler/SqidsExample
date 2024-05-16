using Microsoft.AspNetCore.Mvc;
using Sqids;
using WebApi.Models;
using WebApi.Mvc;

namespace WebApi.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly SqidsEncoder<int> _sqidsEncoder;

    private readonly IEnumerable<UserModel> _users = new UserModel[]
    {
        new()
        {
            Id = 1000,
            NullableId = 1111,
            Name = "A"
        },
        new()
        {
            Id = 2000,
            Name = "B"
        }
    };

    public UserController(SqidsEncoder<int> sqidsEncoder)
    {
        _sqidsEncoder = sqidsEncoder;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserModel>> Get()
    {
        return Ok(_users);
    }

    [HttpGet]
    [Route("{id:sqids}")]
    public ActionResult<UserModel> GetById([FromRoute] [ModelBinder(typeof(SqidsModelBinder))] int? id)
    {
        return Ok(_users.SingleOrDefault(c => c.Id == id));
    }

    [HttpGet]
    [Route("{id:sqids}")]
    public ActionResult<UserModel> GetByNullableId([FromRoute] [ModelBinder(typeof(SqidsModelBinder))] int? id)
    {
        if (id == null)
            return BadRequest();

        return Ok(_users.SingleOrDefault(c => c.NullableId == id));
    }
    
    [HttpGet]
    public ActionResult<string> GetSqids()
    {
        return Ok(_sqidsEncoder.Encode(1000));
    }
}