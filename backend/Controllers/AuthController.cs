using AutoMapper;
using backend.Dtos.AuthDtos;
using backend.Helpers;
using backend.MediatRServices.Commands.AuthCommands;
using backend.MediatRServices.Queries.AuthQueries;
using backend.Models.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace backend.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IMapper _mapper;
    private readonly Expiry _expiry;

    public AuthController(IOptions<Expiry> expiry, IMapper mapper)
    {
        _mapper = mapper;
        _expiry = expiry.Value;
    }

    [Route("Status")]
    [HttpGet]
    public async Task<ActionResult> Status() =>
        Ok(new { message = Username == null ? "You Are Not Logged In" : "Hallo: " + Username });

    [Route("Register")]
    [HttpPost]
    public async Task<ActionResult> Register([FromBody] RegisterUserDto registerUserDto) =>
        Return(await Mediator.Send(new RegisterUserCommand(registerUserDto)));

    [Route("Login")]
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        var response = await Mediator.Send(new LoginUserCommand(loginUserDto, Useragent));
        if (response.IsSuccess == false)
            return BadRequest(new { code = response.Error.Code, message = response.Error.Message });

        SetToHttpCookie(response.Data);
        var tokenDto = _mapper.Map<TokenDto>(response.Data);
        return Ok(tokenDto);
    }

    [Route("RefreshToken")]
    [HttpGet]
    public async Task<ActionResult> RefreshToken()
    {
        if (Request.Cookies.TryGetValue("id", out var id) == false)
            return BadRequest(new { message = "Can not get id, Login Again" });
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest(new { message = "Can not get id, Login Again" });

        if (Request.Cookies.TryGetValue("refreshToken", out var refreshToken) == false)
            return BadRequest(new { message = "Can not get refreshToken, Login Again" });
        if (string.IsNullOrWhiteSpace(refreshToken))
            return BadRequest(new { message = "Can not get refreshToken, Login Again" });

        var response = await Mediator.Send(new RefreshTokenCommand(id, refreshToken, Useragent));

        if (response.IsSuccess == false)
            return BadRequest(new { code = response.Error.Code, message = response.Error.Message });

        SetToHttpCookie(response.Data);
        var tokenDto = _mapper.Map<TokenDto>(response.Data);
        return Ok(tokenDto);
    }

    [HttpGet]
    [Route("isValidUsername/{username}")]
    public async Task<ActionResult> IsValidUsername(string username) =>
        Return(await Mediator.Send(new IsValidUsernameQuery(username)));

    private void SetToHttpCookie(RefreshTokenDto refreshTokenDto)
    {
        var cookiesOption = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.Now.AddDays(_expiry.RefreshTokenExpiryInDays).ToLocalTime(),
            // Expires = DateTime.Now.AddSeconds(15).ToLocalTime(),
            Secure = true,
        };

        Response.Cookies.Append("refreshToken", refreshTokenDto.RefreshToken, cookiesOption);
        Response.Cookies.Append("id", refreshTokenDto.UserId, cookiesOption);
    }
}