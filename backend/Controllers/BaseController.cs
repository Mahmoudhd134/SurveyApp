using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Security.Claims;
using backend.MediatRServices.ErrorHandlers;

[ApiController]
[Route("Api/[controller]")]
public class BaseController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected string Username => User?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
    protected string UserId => User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid))?.Value;

    protected string Useragent => HttpContext.Request.Headers.UserAgent;

    protected ActionResult Return<T>(Response<T> response) =>
        response.IsSuccess
            ? Ok(response.Data)
            : BadRequest(new
            {
                code = response.Error.Code,
                message = response.Error.Message
            });
}