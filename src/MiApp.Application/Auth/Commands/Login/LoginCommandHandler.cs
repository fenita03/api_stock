using MediatR;
using MiApp.Application.Abstractions;
using MiApp.Application.Common;

namespace MiApp.Application.Auth.Commands.Login;

public sealed class LoginCommandHandler(IJwtTokenService jwtTokenService) : IRequestHandler<LoginCommand, AuthResponse>
{
    public Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (!string.Equals(request.Email, "admin@test.com", StringComparison.OrdinalIgnoreCase) ||
            request.Password != "123456")
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var token = jwtTokenService.CreateToken(request.Email);
        return Task.FromResult(new AuthResponse(request.Email, token));
    }
}
