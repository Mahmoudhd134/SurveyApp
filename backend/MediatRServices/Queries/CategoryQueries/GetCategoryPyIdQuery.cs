using backend.Dtos.CategoryDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Queries.CategoryQueries;

public record GetCategoryPyIdQuery(int Id):IRequest<Response<CategoryDto>>;