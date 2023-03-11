using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.Data;
using backend.Dtos.CategoryDtos;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.ErrorHandlers.Errors;
using backend.MediatRServices.Queries.CategoryQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace backend.MediatRServices.Handlers.CategoryHandlers;

public class GetCategoryPyIdHandler : IRequestHandler<GetCategoryPyIdQuery, Response<CategoryDto>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public GetCategoryPyIdHandler(IdentityContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<CategoryDto>> Handle(GetCategoryPyIdQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        if(id == -1)
            return new CategoryDto
            {
                Id = -1,
                Name = "All Categories"
            };
        
        var categoryDto = await _context.Categories
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        
        if (categoryDto == null)
            return Response<CategoryDto>.Failure(CategoryErrors.WrongId);
        return categoryDto;
    }
}