using AutoMapper;
using AutoMapper.QueryableExtensions;
using backend.Data;
using backend.Dtos.CategoryDtos;
using backend.MediatRServices.ErrorHandlers;
using backend.MediatRServices.Queries.CategoryQueries;
using MediatR;

namespace backend.MediatRServices.Handlers.CategoryHandlers;

public class
    GetCategoriesPyPageHandler : IRequestHandler<GetCategoriesPyPageQuery, Response<IEnumerable<CategoryForPageDto>>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesPyPageHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<Response<IEnumerable<CategoryForPageDto>>> Handle(GetCategoriesPyPageQuery request,
        CancellationToken cancellationToken)
    {
        var (prefix, index, size) = request;

        var result = _context.Categories
            .Where(c => c.Name.ToUpper().StartsWith(prefix.ToUpper()))
            .Skip(index * size)
            .Take(size)
            .ProjectTo<CategoryForPageDto>(_mapper.ConfigurationProvider).ToList();

        return Task.FromResult(Response<IEnumerable<CategoryForPageDto>>.Success(result));
    }
}