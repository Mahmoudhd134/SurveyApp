using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.SurveyCommands;

public record DeleteSurveyCommand(int Id,string UserId):IRequest<Response<bool>>;