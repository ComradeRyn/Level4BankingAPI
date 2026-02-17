using Level4BankingAPI.Interfaces;

namespace Level4BankingAPI.Models.DTOs;

public record Account(
    string Id,
    string HolderName,
    decimal Amount) : ICsvFormatter
{
    public string CreateBody()
        => $"{Id},{HolderName},{Amount}";

    public string CreateHeader()
        => "Id,HolderName,Amount";
}