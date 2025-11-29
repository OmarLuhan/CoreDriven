using CoreDriven.Dto.Users;
using CoreDriven.UseCases.Users;
using CoreDriven.Utils.Response;
using CoreDriven.Utils.Spec;
using Microsoft.AspNetCore.Mvc;

namespace CoreDriven.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController(UserUseCases userCases) : ControllerBase
{
  [HttpGet]
  public async Task<ActionResult<Res<IEnumerable<UserDto>>>> GetAll([FromQuery] BaseQueryParams bqp)
  {
    try
    {
      var users = await userCases.GetUsers.Execute(bqp);
      Response.AddPaginationHeader(users.MetaData);
      Log("Get all users successfully");
      return Ok(Res<IEnumerable<UserDto>>.Ok(users));
    }
    catch (Exception ex)
    {
      return StatusCode(500, Res<IEnumerable<UserDto>>.Fail(ex.Message));
    }
  }
  [HttpPost]
  public async Task<ActionResult<Res<UserDto>>> Create([FromBody] UserCreateDto dto)
  {
    try
    {

      UserDto created = await userCases.CreateUser.Execute(dto);
      var res = Res<UserDto>.Ok(created);
      return Ok(res);
    }
    catch (Exception ex)
    {
      return StatusCode(500, Res<UserDto>.Fail(ex.Message));
    }
  }
}