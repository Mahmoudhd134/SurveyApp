using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.SurveyCategoryCommands;

public record AddSurveyCategoryWithNameCommand(string UserId,int SurveyId,string CategoryName):IRequest<Response<bool>>;