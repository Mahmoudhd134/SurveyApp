using AutoMapper;
using backend.Data;
using backend.Dtos.SurveyDtos;
using backend.MediatRServices.Commands.SurveyCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.SurveyHandlers;

public class EditSurveyHandler : IRequestHandler<EditSurveyCommand, Response<bool>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public EditSurveyHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(EditSurveyCommand request, CancellationToken cancellationToken)
    {
        var (editSurveyDto, userId) = request;
        var survey = await _context.Surveys
            .FirstOrDefaultAsync(s => s.Id == editSurveyDto.Id, cancellationToken);

        if (survey == null)
            return Response<bool>.Failure(SurveyErrors.WrongId);

        if (survey.UserId.Equals(userId) == false)
            return Response<bool>.Failure(SurveyErrors.UnAuthorizedEdit);

        _mapper.Map<EditSurveyDto, Survey>(editSurveyDto, survey);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}