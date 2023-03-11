using backend.Data;
using backend.Dtos.SurveyDtos;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.MediatRServices.Queries.SurveyQueries;
using backend.Models.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyHandlers;

public class GetSurveysForPageHandler : IRequestHandler<GetSurveysForPageQuery, Response<IEnumerable<SurveyForPageDto>>>
{
    private readonly IdentityContext _context;
    private readonly UserManager<User> _userManager;

    public GetSurveysForPageHandler(IdentityContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<Response<IEnumerable<SurveyForPageDto>>> Handle(GetSurveysForPageQuery request,
        CancellationToken cancellationToken)
    {
        var (categoryId, userId, pageIndex, pageSize) = request;

        var surveys = await _context.Surveys
            .Include(s => s.SurveyCategories)
            .Where(s => categoryId == -1 ? true : s.SurveyCategories.Select(sc => sc.CategoryId).Contains(categoryId))
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Include(s => s.SurveyOptions)
            .ThenInclude(so => so.SurveyAnswers)
            .ToListAsync(cancellationToken);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Response<IEnumerable<SurveyForPageDto>>.Failure(UserErrors.WrongId);
        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

        var list = new List<SurveyForPageDto>();
        foreach (var survey in surveys)
        {
            var surveyForPageDto = new SurveyForPageDto();
            surveyForPageDto.Id = survey.Id;
            surveyForPageDto.Question = survey.Question;
            if (isAdmin)
                surveyForPageDto.TotalAnswers = survey.SurveyAnswers.Count;

            surveyForPageDto.OptionDtos = new List<OptionDto>();
            var answered = survey.SurveyOptions
                .SelectMany(so => so.SurveyAnswers)
                .Select(sa => sa.UserId)
                .Contains(userId);
            surveyForPageDto.IsAnswered = answered;
            if (answered)
            {
                var x = survey.SurveyOptions.SelectMany(so => so.SurveyAnswers)
                    .FirstOrDefault(sa => sa.UserId == userId);

                if (x != null)
                    surveyForPageDto.AnswerId = x.Id;
            }

            foreach (var surveyOption in survey.SurveyOptions)
            {
                var optionDto = new OptionDto
                {
                    Option = surveyOption.Option,
                    Count = surveyOption.SurveyAnswers.Count,
                    Id = surveyOption.Id
                };
                if (isAdmin == false)
                    optionDto.Count = 0;

                if (answered)
                {
                    var answer = surveyOption.SurveyAnswers
                        .FirstOrDefault(sa => sa.UserId.Equals(userId));

                    if (answer != null)
                    {
                        optionDto.IsAnswered = true;
                    }
                }

                surveyForPageDto.OptionDtos.Add(optionDto);
            }

            list.Add(surveyForPageDto);
        }

        return list;
    }
}