using backend.Dtos.SurveyDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.SurveyCommands;

public record AddSurveyCommand(AddSurveyDto AddSurveyDto,string Username) : IRequest<Response<bool>>;