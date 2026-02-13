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
        => typeof(IFormattable).IsAssignableFrom(type) || typeof(IEnumerable<IFormattable>).IsAssignableFrom(type);

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        string formattedData;
        if (context.Object is IEnumerable<IFormattable> formattableEnumerable)
        {
            var buffer = new StringBuilder();
            var formattableType = formattableEnumerable.GetType().GetGenericArguments()[0];
            IFormattable.CreateHeader(formattableType, buffer);
            foreach (var formattableItem in formattableEnumerable)
            {
                buffer.Append('\n');
                formattableItem.CreateRow(buffer);
            }

            formattedData = buffer.ToString();
        }

        else
        {
            formattedData = (context.Object as IFormattable)!.Format();
        }
        
        await context.HttpContext.Response.WriteAsync(formattedData, selectedEncoding);
    }
}