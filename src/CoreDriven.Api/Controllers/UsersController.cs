using System.Net;
using CoreDrive.Utils.Response;
using CoreDrive.Utils.Spec;
using CoreDriven.Dto.Users;
using CoreDriven.UseCases.Users;
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
      return Ok(response);
    }
    catch (Exception ex)
    {
      response.Status = HttpStatusCode.InternalServerError;
      response.Message = ex.Message;
      return StatusCode(500, response);
    }
  }
}