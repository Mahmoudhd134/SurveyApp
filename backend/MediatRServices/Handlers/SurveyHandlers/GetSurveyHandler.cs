using backend.Data;
using backend.Dtos.CategoryDtos;
using backend.Dtos.SurveyDtos;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.MediatRServices.Queries.SurveyQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyHandlers;

public class GetSurveyHandler : IRequestHandler<GetSurveyQuery, Response<SurveyDto>>
{
    private readonly IdentityContext _context;

    public GetSurveyHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<SurveyDto>> Handle(GetSurveyQuery request, CancellationToken cancellationToken)
    {
        var (surveyId, userId) = request;

        var survey = await _context.Surveys
            .Include(s => s.SurveyCategories)
            .FirstOrDefaultAsync(s => s.Id == surveyId, cancellationToken);

        if (survey == null)
            return Response<SurveyDto>.Failure(SurveyErrors.WrongId);

        await _context.Categories
            .Where(c => survey.SurveyCategories
                .Select(sc => sc.CategoryId).Contains(c.Id))
            .LoadAsync(cancellationToken);

        await _context.SurveyOptions
            .Where(so => so.SurveyId == surveyId)
            .LoadAsync(cancellationToken);

        await _context.SurveyAnswers
            .Where(sa => survey.SurveyOptions
                .Select(so => so.Id).Contains(sa.SurveyOptionId))
            .LoadAsync(cancellationToken);

        await _context.Users
            .Where(u => survey.SurveyAnswers.Select(sa => sa.UserId).Contains(u.Id))
            .LoadAsync(cancellationToken);

        var surveyDto = new SurveyDto();

        surveyDto.Id = survey.Id;
        surveyDto.Question = survey.Question;
        surveyDto.TotalAnswers = survey.SurveyAnswers.Count;
        surveyDto.IsTheMaker = survey.UserId.Equals(userId);
        surveyDto.Categories = survey.SurveyCategories
            .Select(sc => new CategoryDto
            {
                Id = sc.Category.Id,
                Name = sc.Category.Name
            })
            .ToList();

        var answer = survey.SurveyAnswers.FirstOrDefault(sa => sa.UserId.Equals(userId));
        if (answer != null)
        {
            surveyDto.IsAnswered = true;
            surveyDto.AnswerId = answer.Id;
        }

        surveyDto.OptionWithUsers = new List<SurveyOptionWithUsers>();
        foreach (var option in survey.SurveyOptions)
        {
            var optionWithUsers = new SurveyOptionWithUsers
            {
                Id = option.Id,
                IsAnswered = option.SurveyAnswers.Select(sa => sa.UserId).Contains(userId),
                Option = option.Option,
                Count = option.SurveyAnswers.Count,
                UserIds = option.SurveyAnswers.Select(sa => sa.UserId).ToList(),
                Usernames = option.SurveyAnswers.Select(sa => sa.User).Select(u => u.UserName).ToList()
            };
            surveyDto.OptionWithUsers.Add(optionWithUsers);
        }

        return surveyDto;
    }
}