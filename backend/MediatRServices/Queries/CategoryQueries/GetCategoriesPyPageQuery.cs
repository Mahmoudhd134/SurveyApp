using backend.Dtos.CategoryDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Queries.CategoryQueries;

public record GetCategoriesPyPageQuery(string Prefix,int PageIndex,int PageSize):IRequest<Response<IEnumerable<CategoryForPageDto>>>;