using backend.Data;
using backend.MediatRServices.Commands.SurveyCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.Models;
using backend.Models.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyHandlers;

public class AddSurveyHandler : IRequestHandler<AddSurveyCommand, Response<bool>>
{
    private readonly IdentityContext _context;
    private readonly UserManager<User> _userManager;

    public AddSurveyHandler(IdentityContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<Response<bool>> Handle(AddSurveyCommand request, CancellationToken cancellationToken)
    {
        var (addSurveyDto, username) = request;
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
            return Response<bool>.Failure(UserErrors.WrongUsername);

        var found = await _context.Surveys.AnyAsync(
            s => s.Question.ToUpper().Equals(addSurveyDto.Survey.ToUpper()), cancellationToken);
        if(found)
            return Response<bool>.Failure(SurveyErrors.SameSurveyFound);
            
        var categories = await _context.Categories
            .Where(c => addSurveyDto.Categories.Contains(c.Name))
            .ToListAsync(cancellationToken);

        Survey survey = new()
        {
            Question = addSurveyDto.Survey,
            UserId = user.Id,
            User = user
        };

        survey.SurveyOptions.AddRange(addSurveyDto.Options.Select(o => new SurveyOption
        {
            Survey = survey,
            Option = o
        }));
        
        var surveyCategories = categories.Select(c => new SurveyCategory
        {
            Survey = survey,
            Category = c
        });
        survey.SurveyCategories.AddRange(surveyCategories);
        // _context.Surveys.Update(survey);
        _context.Surveys.Add(survey);
        
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}