using backend.Data;
using backend.MediatRServices.Commands.SurveyAnswerCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyAnswerHandler;

public class RemoveSurveyAnswerHandler : IRequestHandler<RemoveSurveyAnswerCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public RemoveSurveyAnswerHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(RemoveSurveyAnswerCommand request, CancellationToken cancellationToken)
    {
        var (id, userId) = request;
        var surveyAnswer = await _context.SurveyAnswers
            .FirstOrDefaultAsync(sa => sa.Id == id,cancellationToken);
        
        if(surveyAnswer == null)
            return Response<bool>.Failure(SurveyAnswerErrors.WrongId);
        
        if(surveyAnswer.UserId.Equals(userId) == false)
            return Response<bool>.Failure(SurveyAnswerErrors.UnAuthorizedDelete);

        _context.SurveyAnswers.Remove(surveyAnswer);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}