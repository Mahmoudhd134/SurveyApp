using backend.Data;
using backend.MediatRServices.Commands.SurveyCategoryCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyCategoryHandlers;

public class AddSurveyCategoryWithNameHandler : IRequestHandler<AddSurveyCategoryWithNameCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public AddSurveyCategoryWithNameHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(AddSurveyCategoryWithNameCommand request,
        CancellationToken cancellationToken)
    {
        var (userId, surveyId, categoryName) = request;
        var surveyUserId = await _context.Surveys
            .Where(s => s.Id == surveyId)
            .Select(s => s.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(surveyUserId))
            return Response<bool>.Failure(SurveyErrors.WrongId);

        if (surveyUserId.Equals(userId) == false)
            return Response<bool>.Failure(SurveyCategoryErrors.UnAuthorizedAdd);

        var categoryId = await _context.Categories
            .Where(c => c.Name.Equals(categoryName))
            .Select(c => c.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (categoryId == 0)
            return Response<bool>.Failure(CategoryErrors.WrongName);

        var found = await _context.SurveyCategories
            .AnyAsync(sc =>
                sc.CategoryId == categoryId && sc.SurveyId == surveyId, cancellationToken);
        if (found)
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