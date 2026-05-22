using FluentValidation;
using MediatR;
using MiApp.Application.Abstractions;
using MiApp.Application.Common;
using MiApp.Domain.Entities;

namespace MiApp.Application.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IValidator<CreateCategoryCommand> validator)
    : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var category = new Category { Name = request.Name };
        await categoryRepository.AddAsync(category, cancellationToken);
        await categoryRepository.SaveChangesAsync(cancellationToken);

        return new CategoryDto(category.Id, category.Name);
    }
}
