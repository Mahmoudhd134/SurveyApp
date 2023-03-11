using backend.MediatRServices.ErrorHandlers;
using MediatR;

namespace backend.MediatRServices.Commands.CategoryCommands;

public record DeleteCategoryCommand(int Id):IRequest<Response<bool>>;