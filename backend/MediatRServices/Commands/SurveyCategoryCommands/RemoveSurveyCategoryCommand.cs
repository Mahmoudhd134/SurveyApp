using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.SurveyCategoryCommands;

public record RemoveSurveyCategoryCommand(string UserId,int SurveyId,int CategoryId):IRequest<Response<bool>>;