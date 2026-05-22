using MediatR;
using MiApp.Application.Common;

namespace MiApp.Application.Auth.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;
