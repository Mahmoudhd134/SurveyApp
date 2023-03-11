using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Queries.AuthQueries;

public record IsValidPasswordQuery(string Password):IRequest<Response<bool>>;