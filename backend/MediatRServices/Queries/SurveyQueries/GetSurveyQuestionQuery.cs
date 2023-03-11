using backend.Dtos.SurveyDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Queries.SurveyQueries;

public record GetSurveyQuestionQuery(int Id):IRequest<Response<SurveyQuestionDto>>;