using backend.Data;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.MediatRServices.Queries.AuthQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.AuthHandlers;

public class IsValidUsernameHandler : IRequestHandler<IsValidUsernameQuery, Response<bool>>
{
    private readonly IdentityContext _context;

    public IsValidUsernameHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(IsValidUsernameQuery request, CancellationToken cancellationToken)
    {
        var username = request.Username;
        if (string.IsNullOrWhiteSpace(username))
            return false;
        
        var found = await _context.Users.AnyAsync(u => u.UserName.ToUpper().Equals(username.ToUpper()),
            cancellationToken);

        return found == false ? true : Response<bool>.Failure(UserErrors.UsernameAlreadyUsedError);
    }
}