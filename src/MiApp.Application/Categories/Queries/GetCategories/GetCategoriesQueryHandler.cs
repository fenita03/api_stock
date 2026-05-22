using MediatR;
using MiApp.Application.Abstractions;
using MiApp.Application.Common;

namespace MiApp.Application.Categories.Queries.GetCategories;

public sealed class GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoriesQuery, IReadOnlyList<CategoryDto>>
{
    public async Task<IReadOnlyList<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync(cancellationToken);
        return categories.Select(category => new CategoryDto(category.Id, category.Name)).ToList();
    }
}
