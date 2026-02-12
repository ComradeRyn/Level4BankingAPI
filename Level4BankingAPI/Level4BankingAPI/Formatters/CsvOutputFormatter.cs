using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using IFormattable = Level4BankingAPI.Interfaces.IFormattable;

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
        => typeof(IFormattable).IsAssignableFrom(type);

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var toReturn = context.Object as IFormattable;
        await context.HttpContext.Response.WriteAsync(toReturn!.Format(), selectedEncoding);
    }
}