using backend.Dtos.CategoryDtos;
using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.CategoryCommands;

public record EditCategoryCommand(EditCategoryDto EditCategoryDto):IRequest<Response<bool>>;