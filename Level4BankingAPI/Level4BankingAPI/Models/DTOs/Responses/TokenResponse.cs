using Level4BankingAPI.Interfaces;

namespace Level4BankingAPI.Models.DTOs.Responses;

public record TokenResponse(string Token) : ICsvFormatter
{
    public string CreateBody()
        => Token;

    public string CreateHeader()
        => "Token";
}