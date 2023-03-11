using AutoMapper;
using backend.Data;
using backend.Dtos.CategoryDtos;
using backend.MediatRServices.Commands.CategoryCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.CategoryHandlers;

public class EditCategoryHandler : IRequestHandler<EditCategoryCommand, Response<bool>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public EditCategoryHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var editCategoryDto = request.EditCategoryDto;
        var category =
            await _context.Categories.FirstOrDefaultAsync(c => c.Id == editCategoryDto.Id, cancellationToken);
        
        if(category == null)
            return Response<bool>.Failure(CategoryErrors.WrongId);

        _mapper.Map(editCategoryDto, category);
        _context.Categories.Update(category);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if(result == 0)
            return Response<bool>.Failure(DomainErrors.UnKnownError);
        return true;
    }
}