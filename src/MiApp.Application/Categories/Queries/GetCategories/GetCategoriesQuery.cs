using MediatR;
using MiApp.Application.Common;

namespace MiApp.Application.Categories.Queries.GetCategories;

public sealed record GetCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>;
