using backend.Data;
using backend.MediatRServices.Commands.SurveyCategoryCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyCategoryHandlers;

public class AddSurveyCategoryHandler : IRequestHandler<AddSurveyCategoryCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public AddSurveyCategoryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(AddSurveyCategoryCommand request, CancellationToken cancellationToken)
    {
        var (userId, surveyId, categoryId) = request;
        var surveyUserId = await _context.Surveys
            .Where(s => s.Id == surveyId)
            .Select(s => s.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(surveyUserId))
            return Response<bool>.Failure(SurveyErrors.WrongId);

        if (surveyUserId.Equals(userId) == false)
            return Response<bool>.Failure(SurveyCategoryErrors.UnAuthorizedAdd);

        var categoryFound = await _context.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
        if(categoryFound == false)
            return Response<bool>.Failure(CategoryErrors.WrongId);

        var found = await _context.SurveyCategories
            .AnyAsync(sc => 
                sc.CategoryId == categoryId && sc.SurveyId == surveyId, cancellationToken);
        if(found)
            return Response<bool>.Failure(SurveyCategoryErrors.CategoryAlreadyExists);

        _context.SurveyCategories.Add(new SurveyCategory
        {
            SurveyId = surveyId,
            CategoryId = categoryId
        });
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}