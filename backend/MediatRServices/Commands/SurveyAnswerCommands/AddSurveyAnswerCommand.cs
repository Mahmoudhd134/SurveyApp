using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.SurveyAnswerCommands;

public record AddSurveyAnswerCommand(string UserId,int SurveyId,int OptionId) : IRequest<Response<bool>>;