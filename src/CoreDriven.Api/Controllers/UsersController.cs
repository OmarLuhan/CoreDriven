using System.Net;
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
  public async Task<ActionResult<Response<IEnumerable<UserDto>>>> GetAll([FromQuery] BaseQueryParams bqp)
  {
    Response<IEnumerable<UserDto>> response = new();
    try
    {
      var users = await userCases.GetUsers.Execute(bqp);
      Response.AddPaginationHeader(users.MetaData);
      response.Status = HttpStatusCode.OK;
      response.Success = true;
      response.Data = users;
      Log("Get all users successfully");
      return Ok(response);
    }
    catch (Exception ex)
    {
      response.Status = HttpStatusCode.InternalServerError;
      response.Message = ex.Message;
      return StatusCode(500, response);
    }
  }
  [HttpPost]
  public async Task<ActionResult<Response<UserDto>>> Create([FromBody] UserCreateDto dto)
  {
    Response<UserDto> response = new();
    try
    {

      UserDto created = await userCases.AddUser.Execute(dto);
      response.Status = HttpStatusCode.Created;
      response.Success = true;
      response.Data = created;
      return Ok(response);
    }
    catch (Exception ex)
    {
      response.Message=ex.Message;
      response.Status = HttpStatusCode.InternalServerError;
      return StatusCode(500, response);
    }
  }
}