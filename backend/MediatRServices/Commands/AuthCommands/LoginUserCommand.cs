using backend.Dtos.AuthDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.AuthCommands;

public record LoginUserCommand(LoginUserDto LoginUserDto,string UserAgent) : IRequest<Response<RefreshTokenDto>>;