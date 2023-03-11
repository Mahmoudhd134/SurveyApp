using backend.Dtos.AuthDtos;
using backend.MediatRServices.ErrorHandlers;
using backend.Models.IdentityModels;
using MediatR;

namespace backend.MediatRServices.Queries.AuthQueries;

public record GetTokenAndRefreshTokenQuery(User User) : IRequest<Response<RefreshTokenDto>>;