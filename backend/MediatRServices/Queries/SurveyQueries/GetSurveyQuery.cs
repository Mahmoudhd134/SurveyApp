using backend.Dtos.SurveyDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Queries.SurveyQueries;

public record GetSurveyQuery(int SurveyId,string UserId):IRequest<Response<SurveyDto>>;