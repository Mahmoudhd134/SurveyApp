using backend.Data;
using backend.MediatRServices.Commands.CategoryCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.CategoryHandlers;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public DeleteCategoryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (category == null)
            return Response<bool>.Failure(CategoryErrors.WrongId);

        _context.Categories.Remove(category);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Response<bool>.Failure(DomainErrors.UnKnownError);
        return true;
    }
}