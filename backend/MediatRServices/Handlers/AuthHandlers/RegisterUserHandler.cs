using System.Text.RegularExpressions;
using AutoMapper;
using backend.Data;
using backend.Dtos.AuthDtos;
using backend.MediatRServices.Commands.AuthCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.MediatRServices.Queries.AuthQueries;
using backend.Models.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace backend.MediatRServices.Handlers.AuthHandlers;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Response<bool>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;

    public RegisterUserHandler(IdentityContext context, IMapper mapper, UserManager<User> userManager,
        IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _mediator = mediator;
    }


    async Task<Response<bool>> IRequestHandler<RegisterUserCommand, Response<bool>>.Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var registerUserDto = request.RegisterUserDto;

        var isValidUsername =
            await _mediator.Send(new IsValidUsernameQuery(registerUserDto.Username), cancellationToken);
        if (isValidUsername.IsSuccess == false)
            return Response<bool>.Failure(isValidUsername.Error);

        var isValidPassword =
            await _mediator.Send(new IsValidPasswordQuery(registerUserDto.Password), cancellationToken);
        if (isValidPassword.IsSuccess == false)
            return Response<bool>.Failure(isValidPassword.Error);

        var isValidEmail = await _mediator.Send(new IsValidEmailQuery(registerUserDto.Email), cancellationToken);
        if (isValidEmail.IsSuccess == false)
            return Response<bool>.Failure(isValidEmail.Error);


        var user = _mapper.Map<RegisterUserDto, User>(registerUserDto);
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        if (result.Succeeded)
            return true;
        else
            return Response<bool>.Failure(new Error("User.UnknownError",
                string.Join("\n", result.Errors.Select(e => e.Description))));
    }
}