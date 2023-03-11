using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.SurveyOptionCommands;

public record RemoveSurveyOptionCommand(int Id, string UserId) : IRequest<Response<bool>>;