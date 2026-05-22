using MediatR;

namespace MiApp.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(int Id) : IRequest<bool>;
