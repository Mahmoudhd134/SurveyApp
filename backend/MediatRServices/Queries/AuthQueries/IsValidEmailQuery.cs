using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Queries.AuthQueries;

public record IsValidEmailQuery(string Email):IRequest<Response<bool>>;