using backend.Dtos.AuthDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.AuthCommands;

public record RegisterUserCommand(RegisterUserDto RegisterUserDto) : IRequest<Response<bool>>;