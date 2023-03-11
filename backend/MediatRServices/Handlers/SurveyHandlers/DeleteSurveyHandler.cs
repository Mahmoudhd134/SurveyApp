using backend.Data;
using backend.MediatRServices.Commands.SurveyCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyHandlers;

public class DeleteSurveyHandler : IRequestHandler<DeleteSurveyCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public DeleteSurveyHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(DeleteSurveyCommand request, CancellationToken cancellationToken)
    {
        var (id, userId) = request;
        var survey = await _context.Surveys
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        
        if(survey == null)
            return Response<bool>.Failure(SurveyErrors.WrongId);
        
        if(survey.UserId.Equals(userId) == false)
            return Response<bool>.Failure(SurveyErrors.UnAuthorizedDelete);

        _context.Remove(survey);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}