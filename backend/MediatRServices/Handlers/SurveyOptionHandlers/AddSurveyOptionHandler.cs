using AutoMapper;
using backend.Data;
using backend.MediatRServices.Commands.SurveyOptionCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyOptionHandlers;

public class AddSurveyOptionHandler : IRequestHandler<AddSurveyOptionCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public AddSurveyOptionHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(AddSurveyOptionCommand request, CancellationToken cancellationToken)
    {
        var (addSurveyOptionDto, userId) = request;
        var survey = await _context.Surveys
            .Include(s => s.SurveyOptions)
            .FirstOrDefaultAsync(s => s.Id == addSurveyOptionDto.SurveyId,cancellationToken);

        if (survey == null)
            return Response<bool>.Failure(SurveyErrors.WrongId);
        
        if(survey.UserId.Equals(userId) == false)
            return Response<bool>.Failure(SurveyOptionErrors.UnAuthorizedAdd);
        
        if(survey.SurveyOptions.Select(so => so.Option).Contains(addSurveyOptionDto.Option))
            return Response<bool>.Failure(SurveyOptionErrors.TheOptionIsAlreadyThere);
        
        survey.SurveyOptions.Add(new SurveyOption
        {
            Option = addSurveyOptionDto.Option,
            Survey = survey
        });

        _context.Update(survey);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}