using backend.Data;
using backend.MediatRServices.Commands.SurveyOptionCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyOptionHandlers;

public class RemoveSurveyOptionHandler : IRequestHandler<RemoveSurveyOptionCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public RemoveSurveyOptionHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(RemoveSurveyOptionCommand request, CancellationToken cancellationToken)
    {
        var (id, userId) = request;
        var surveyOption = await _context.SurveyOptions
            .Include(so => so.SurveyAnswers)
            .Include(so => so.Survey)
            .FirstOrDefaultAsync(so => so.Id == id, cancellationToken);
        
        if(surveyOption == null)
            return Response<bool>.Failure(SurveyOptionErrors.WrongId);
        
        if(surveyOption.Survey.UserId.Equals(userId) == false)
            return Response<bool>.Failure(SurveyOptionErrors.UnAuthorizedDelete);

        _context.SurveyAnswers.RemoveRange(surveyOption.SurveyAnswers);
        _context.SurveyOptions.Remove(surveyOption);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}