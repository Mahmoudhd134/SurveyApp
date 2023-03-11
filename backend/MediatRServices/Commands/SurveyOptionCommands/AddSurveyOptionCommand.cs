using backend.Dtos.SurveyOptionDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.SurveyOptionCommands;

public record AddSurveyOptionCommand(AddSurveyOptionDto AddSurveyOptionDto, string UserId) : IRequest<Response<bool>>;