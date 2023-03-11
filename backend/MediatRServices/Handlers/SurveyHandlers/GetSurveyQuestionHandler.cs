using backend.Data;
using backend.Dtos.SurveyDtos;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.MediatRServices.Queries.SurveyQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyHandlers;

public class GetSurveyQuestionHandler : IRequestHandler<GetSurveyQuestionQuery, Response<SurveyQuestionDto>>
{
    private readonly IdentityContext _context;

    public GetSurveyQuestionHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<SurveyQuestionDto>> Handle(GetSurveyQuestionQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var surveyQuestion = await _context.Surveys
            .Where(s => s.Id == id)
            .Select(s => s.Question)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(string.IsNullOrWhiteSpace(surveyQuestion))
            return Response<SurveyQuestionDto>.Failure(SurveyErrors.WrongId);

        return new SurveyQuestionDto
        {
            Id = id,
            Question = surveyQuestion
        };
    }
}