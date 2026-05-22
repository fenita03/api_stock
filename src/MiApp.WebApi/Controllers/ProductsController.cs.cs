using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Common;
using MiApp.Application.Products.Commands.CreateProduct;
using MiApp.Application.Products.Commands.DeleteProduct;
using MiApp.Application.Products.Commands.UpdateProduct;
using MiApp.Application.Products.Queries.GetProductById;
using MiApp.Application.Products.Queries.GetProducts;
using MiApp.WebApi.Contracts.Products;

namespace MiApp.WebApi.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Products")]
[Route("api/products")]
public sealed class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await mediator.Send(new GetProductsQuery(), cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return product is null ? NotFound() : Ok(product);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ProductDto>> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await mediator.Send(
                new CreateProductCommand(request.Name, request.Description, request.Price, request.Stock, request.CategoryId),
                cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        catch (ValidationException exception)
        {
            return BadRequest(exception.Errors.Select(error => new { error.PropertyName, error.ErrorMessage }));
        }
        catch (KeyNotFoundException exception)
        {
            return BadRequest(new { exception.Message });
        }
    }

    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> Update(int id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await mediator.Send(
                new UpdateProductCommand(id, request.Name, request.Description, request.Price, request.Stock, request.CategoryId),
                cancellationToken);

            return product is null ? NotFound() : Ok(product);
        }
        catch (ValidationException exception)
        {
            return BadRequest(exception.Errors.Select(error => new { error.PropertyName, error.ErrorMessage }));
        }
        catch (KeyNotFoundException exception)
        {
            return BadRequest(new { exception.Message });
        }
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteProductCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
