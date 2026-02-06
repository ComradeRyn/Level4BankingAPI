using System.Text;
using Level4BankingAPI.Models.DTOs;
using Level4BankingAPI.Models.DTOs.Responses;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Level4BankingAPI.Formatters;

public class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add("text/csv");
        
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }
    
    protected override bool CanWriteType(Type? type)
        => typeof(Account).IsAssignableFrom(type) ||
           typeof(ConversionResponse).IsAssignableFrom(type) ||
           typeof(IEnumerable<Account>).IsAssignableFrom(type) ||
           typeof(TokenResponse).IsAssignableFrom(type);

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var httpContext = context.HttpContext;
        var buffer = new StringBuilder();
        
        if (context.Object is Account or TokenResponse)
        {
            CreateSingleObjectCsvHeader(context.Object, buffer);
            buffer.Append('\n');
            CreateSingleObjectCsvRow(context.Object, buffer);
        }
        
        else if (context.Object is ConversionResponse conversionResponse)
        {
            DictionaryToCsv(conversionResponse.ConvertedCurrencies, buffer);
        }
        
        else if(context.Object is List<Account> accounts)
        {
            CreateSingleObjectCsvHeader(accounts[0], buffer);
            buffer.Append('\n');

            for (var i = 0; i < accounts.Count; i++)
            {
                CreateSingleObjectCsvRow(accounts[i], buffer);
                if (i < accounts.Count - 1)
                {
                    buffer.Append('\n');
                }
            }
        }
        
        await httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
    }

    private void CreateSingleObjectCsvHeader(object toConvert, StringBuilder buffer)
    {
        var propertyInfos = toConvert.GetType().GetProperties();
        for (var i = 0; i < propertyInfos.Length; i++)
        {
            buffer.Append(propertyInfos[i].Name);
            if (i < propertyInfos.Length - 1)
            {
                buffer.Append(',');
            }
        }
    }

    private void CreateSingleObjectCsvRow(object toConvert, StringBuilder buffer)
    {
        var propertyInfos = toConvert.GetType().GetProperties();
        for (var i = 0; i < propertyInfos.Length; i++)
        {
            if (propertyInfos[i].PropertyType == typeof(string))
            {
                buffer.Append($"\"{propertyInfos[i].GetValue(toConvert)}\"");
            }
            else
            {
                buffer.Append(propertyInfos[i].GetValue(toConvert));
            }
            
            if (i < propertyInfos.Length - 1)
            {
                buffer.Append(',');
            }
        }
    }

    private void DictionaryToCsv(IDictionary<string, decimal> toConvert, StringBuilder buffer)
    {
        buffer.Append("CurrencyType,Value");
        foreach (var keyValuePair in toConvert)
        {
            buffer.Append($"\n{keyValuePair.Key},{keyValuePair.Value}");
        }
    }
}