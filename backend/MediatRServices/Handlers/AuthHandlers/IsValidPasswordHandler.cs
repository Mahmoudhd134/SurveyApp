using System.Text.RegularExpressions;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.MediatRServices.Queries.AuthQueries;
using MediatR;

namespace backend.MediatRServices.Handlers.AuthHandlers;

public class IsValidPasswordHandler : IRequestHandler<IsValidPasswordQuery, Response<bool>>
{
    public Task<Response<bool>> Handle(IsValidPasswordQuery request, CancellationToken cancellationToken)
    {
        var pass = request.Password;
        if (string.IsNullOrWhiteSpace(pass))
            return Task.FromResult(new Response<bool>(true, false, DomainErrors.None));

        var isGoodPass = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

        return Task.FromResult(isGoodPass.IsMatch(pass) ? true : Response<bool>.Failure(UserErrors.WeakPasswordError));
    }
}