using MediatR;
using MiApp.Application.Common;

namespace MiApp.Application.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;
