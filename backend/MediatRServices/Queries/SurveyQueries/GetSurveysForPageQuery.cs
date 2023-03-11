using backend.Dtos.SurveyDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Queries.SurveyQueries;

public record GetSurveysForPageQuery(int CategoryId,string UserId,int PageIndex,int PageSize):IRequest<Response<IEnumerable<SurveyForPageDto>>>;