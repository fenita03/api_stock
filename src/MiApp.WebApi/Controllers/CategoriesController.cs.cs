using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Categories.Commands.CreateCategory;
using MiApp.Application.Categories.Queries.GetCategories;
using MiApp.Application.Common;
using MiApp.WebApi.Contracts.Categories;

namespace MiApp.WebApi.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Categories")]
[Route("api/categories")]
public sealed class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        var categories = await mediator.Send(new GetCategoriesQuery(), cancellationToken);
        return Ok(categories);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CategoryDto>> Create(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var category = await mediator.Send(new CreateCategoryCommand(request.Name), cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = category.Id }, category);
        }
        catch (ValidationException exception)
        {
            return BadRequest(exception.Errors.Select(error => new { error.PropertyName, error.ErrorMessage }));
        }
    }
}
