using backend.Dtos.SurveyDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.SurveyCommands;

public record EditSurveyCommand(EditSurveyDto EditSurveyDto,string UserId):IRequest<Response<bool>>;