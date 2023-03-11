using backend.Dtos.AuthDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.AuthCommands;

public record RefreshTokenCommand
    (string UserId, string RefreshToken, string UserAgent) : IRequest<Response<RefreshTokenDto>>;