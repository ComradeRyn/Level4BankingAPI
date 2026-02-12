using IFormattable = Level4BankingAPI.Interfaces.IFormattable;

namespace Level4BankingAPI.Models.DTOs;

public record Account(
    string Id,
    string HolderName,
    decimal Amount) : IFormattable;