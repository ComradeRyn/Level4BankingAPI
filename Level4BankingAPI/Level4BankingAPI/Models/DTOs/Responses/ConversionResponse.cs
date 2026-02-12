using System.Text;
using Level4BankingAPI.Interfaces;
using IFormattable = Level4BankingAPI.Interfaces.IFormattable;

namespace Level4BankingAPI.Models.DTOs.Responses;

public record ConversionResponse(Dictionary<string, decimal> ConvertedCurrencies) : IFormattable
{
    public string Format()
    {
        var buffer = new StringBuilder();
        buffer.Append("CurrencyType,Value");
        foreach (var keyValuePair in ConvertedCurrencies)
        {
            buffer.Append($"\n{keyValuePair.Key},{keyValuePair.Value}");
        }

        return buffer.ToString();
    }
}