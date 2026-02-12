using Level4BankingAPI.Interfaces;
using IFormattable = Level4BankingAPI.Interfaces.IFormattable;

namespace Level4BankingAPI.Models.DTOs.Responses;

public record TokenResponse(string Token) : IFormattable;