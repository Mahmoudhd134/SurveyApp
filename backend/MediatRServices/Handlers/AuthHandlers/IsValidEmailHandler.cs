using backend.Data;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.MediatRServices.Queries.AuthQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.AuthHandlers;

public class IsValidEmailHandler : IRequestHandler<IsValidEmailQuery, Response<bool>>
{
    private readonly IdentityContext _context;

    public IsValidEmailHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(IsValidEmailQuery request, CancellationToken cancellationToken)
    {
        var email = request.Email;
        var found = await _context.Users.AnyAsync(u => u.Email.ToUpper().Equals(email.ToUpper()), cancellationToken);
        return found == false ? true : Response<bool>.Failure(UserErrors.NotValidEmailError);
    }
}