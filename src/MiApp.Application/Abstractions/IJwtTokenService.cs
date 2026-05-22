namespace MiApp.Application.Abstractions;

public interface IJwtTokenService
{
    string CreateToken(string email);
}
