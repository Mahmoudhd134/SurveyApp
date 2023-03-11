using AutoMapper;
using backend.Data;
using backend.Dtos.CategoryDtos;
using backend.MediatRServices.Commands.CategoryCommands;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.CategoryHandlers;

public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Response<bool>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public AddCategoryHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var addCategoryDto = request.AddCategoryDto;
        
        var found = await _context.Categories.AnyAsync(c => c.Name.ToUpper().Equals(addCategoryDto.Name.ToUpper()),
            cancellationToken);
        if(found)
            return Response<bool>.Failure(CategoryErrors.NameAlreadyExistsError);

        var category = _mapper.Map<AddCategoryDto,backend.Models.Category>(addCategoryDto);
        await _context.Categories.AddAsync(category, cancellationToken);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Response<bool>.Failure(DomainErrors.UnKnownError);
        return true;
    }
}