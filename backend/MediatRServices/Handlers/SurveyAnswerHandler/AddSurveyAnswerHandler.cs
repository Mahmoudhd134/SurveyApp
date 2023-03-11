using backend.Data;
using backend.MediatRServices.Commands.SurveyAnswerCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyAnswerHandler;

public class AddSurveyAnswerHandler : IRequestHandler<AddSurveyAnswerCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public AddSurveyAnswerHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(AddSurveyAnswerCommand request, CancellationToken cancellationToken)
    {
        var (userId, surveyId, optionId) = request;

        // var surveyFound = await _context.Surveys.AnyAsync(s => s.Id == surveyId, cancellationToken);
        // if (surveyFound == false)
        //     return Response<bool>.Failure(SurveyErrors.WrongId);
        //
        // var optionFound = await _context.SurveyOptions.AnyAsync(so => so.Id == optionId, cancellationToken);
        // if (optionFound == false)
        //     return Response<bool>.Failure(SurveyOptionErrors.WrongId);
        //
        // var userAnswersThisSurveyBefore = await _context.SurveyAnswers
        //     .AnyAsync(sa => sa.UserId.Equals(userId) && sa.SurveyId == surveyId, cancellationToken);
        // if(userAnswersThisSurveyBefore)
        //     return Response<bool>.Failure(SurveyErrors.DuplicateAnswerError);


        var survey = await _context.Surveys
            .Include(s => s.SurveyOptions)
            .Include(s => s.SurveyAnswers)
            .FirstOrDefaultAsync(s => s.Id == surveyId, cancellationToken);

        if (survey == null)
            return Response<bool>.Failure(SurveyErrors.WrongId);

        var optionFound = survey.SurveyOptions.Select(so => so.Id).Contains(optionId);
        if (optionFound == false)
            return Response<bool>.Failure(SurveyOptionErrors.WrongOption);

        var userAnswersThisSurveyBefore = survey.SurveyAnswers.Select(sa => sa.UserId).Contains(userId);
        if (userAnswersThisSurveyBefore)
            return Response<bool>.Failure(SurveyErrors.DuplicateAnswerError);


        _context.SurveyAnswers.Add(new SurveyAnswer
        {
            SurveyId = surveyId,
            UserId = userId,
            SurveyOptionId = optionId
        });
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}