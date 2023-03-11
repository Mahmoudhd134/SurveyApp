using backend.Data;
using backend.MediatRServices.Commands.SurveyCategoryCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyCategoryHandlers;

public class RemoveSurveyCategoryHandler : IRequestHandler<RemoveSurveyCategoryCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public RemoveSurveyCategoryHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(RemoveSurveyCategoryCommand request, CancellationToken cancellationToken)
    {
        var (userId, surveyId, categoryId) = request;
        var surveyUserId = await _context.Surveys
            .Where(s => s.Id == surveyId)
            .Select(s => s.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(surveyUserId))
            return Response<bool>.Failure(SurveyErrors.WrongId);

        if (surveyUserId.Equals(userId) == false)
            return Response<bool>.Failure(SurveyCategoryErrors.UnAuthorizedRemove);

        var categoryFound = await _context.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
        if (categoryFound == false)
            return Response<bool>.Failure(CategoryErrors.WrongId);

        var surveyCategory = await _context.SurveyCategories
            .FirstOrDefaultAsync(sc => sc.SurveyId == surveyId && sc.CategoryId == categoryId,
                cancellationToken);
        
        if(surveyCategory == null)
            return Response<bool>.Failure(SurveyCategoryErrors.SurveyDoseNotHaseCategory);

        _context.SurveyCategories.Remove(surveyCategory);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}