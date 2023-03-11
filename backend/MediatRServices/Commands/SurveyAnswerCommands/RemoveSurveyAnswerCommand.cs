using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.SurveyAnswerCommands;

public record RemoveSurveyAnswerCommand(int Id,string UserId):IRequest<Response<bool>>;