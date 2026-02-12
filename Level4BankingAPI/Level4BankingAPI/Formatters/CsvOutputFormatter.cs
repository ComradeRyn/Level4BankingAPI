using System.Reflection;
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
        var buffer = new StringBuilder();
        if (context.Object is Account or TokenResponse)
        {
            CreateHeader(context.Object, buffer);
            buffer.Append('\n');
            CreateRow(context.Object, buffer);
        }
        
        else if (context.Object is ConversionResponse conversionResponse)
        {
            ConvertDictionary(conversionResponse.ConvertedCurrencies, buffer);
        }
        
        else if(context.Object is List<Account> accounts)
        {
            CreateHeader(accounts[0], buffer);
            foreach (var account in accounts)
            {
                buffer.Append('\n');
                CreateRow(account, buffer);
            }
        }
        
        await context.HttpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
    }

    private void CreateHeader(object toConvert, StringBuilder buffer)
    {
        var propertyInfos = toConvert.GetType().GetProperties();
        buffer.Append(propertyInfos[0].Name);
        for (var i = 1; i < propertyInfos.Length; i++)
        {
            buffer.Append(',');
            buffer.Append(propertyInfos[i].Name);
        }
    }

    private void CreateRow(object toConvert, StringBuilder buffer)
    {
        var propertyInfos = toConvert.GetType().GetProperties();
        if (propertyInfos.Length != 0)
        {
            AppendValue(toConvert, propertyInfos[0], buffer);
        }
        
        for (var i = 1; i < propertyInfos.Length; i++)
        {
            buffer.Append(',');
            AppendValue(toConvert, propertyInfos[i], buffer);
        }
    }

    private void ConvertDictionary(IDictionary<string, decimal> toConvert, StringBuilder buffer)
    {
        buffer.Append("CurrencyType,Value");
        foreach (var keyValuePair in toConvert)
        {
            buffer.Append($"\n{keyValuePair.Key},{keyValuePair.Value}");
        }
    }

    private void AppendValue(object toConvert, 
        PropertyInfo propertyInfo, 
        StringBuilder buffer)
    {
        if (propertyInfo.PropertyType == typeof(string))
        {
            buffer.Append($"\"{propertyInfo.GetValue(toConvert)}\"");
        }
            
        else
        {
            buffer.Append(propertyInfo.GetValue(toConvert));
        }
    }
}