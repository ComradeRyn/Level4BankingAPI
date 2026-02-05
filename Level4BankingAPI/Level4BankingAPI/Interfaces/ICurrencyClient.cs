using Level4BankingAPI.Models.DTOs.Responses;

namespace Level4BankingAPI.Interfaces;

public interface ICurrencyClient
{
    Task<CurrencyClientResponse> GetConversionRates(string currencyTypes);
}