using System.Text;
using Level4BankingAPI.Interfaces;

namespace Level4BankingAPI.Models.DTOs.Responses;

public record ConversionResponse(Dictionary<string, decimal> ConvertedCurrencies) : ICsvFormatter
{
    public string FormatCsv()
    {
        if (ConvertedCurrencies.Count == 0)
        {
            return "";
        }
        
        var buffer = new StringBuilder();
        foreach (var keyValuePair in ConvertedCurrencies)
        {
            buffer.Append($"{keyValuePair.Key},{keyValuePair.Value}\n");
        }

        buffer.Remove(buffer.Length - 1, 1);

        return buffer.ToString();
    }

    public string CreateHeader()
        => "CurrencyType,Value";
}