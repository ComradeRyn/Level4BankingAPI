using System.Text;
using Level4BankingAPI.Interfaces;
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
        => typeof(ICsvFormatter).IsAssignableFrom(type) || typeof(IEnumerable<ICsvFormatter>).IsAssignableFrom(type);

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        if (context.Object is IEnumerable<ICsvFormatter> formattableEnumerable)
        {
            var formattableList = formattableEnumerable.ToList();
            if (formattableList.Count == 0)
            {
                await context.HttpContext.Response.WriteAsync("", selectedEncoding);

                return;
            }

            var buffer = new StringBuilder();
            buffer.Append(formattableList[0].CreateHeader());
            foreach (var element in formattableList)
            {
                buffer.Append('\n');
                buffer.Append(element.Format());
            }

            await context.HttpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
            
            return;
        }
        
        var formattableObject = context.Object as ICsvFormatter;
        var formattedData = $"{formattableObject!.CreateHeader()}\n{formattableObject.Format()}";
        
        await context.HttpContext.Response.WriteAsync(formattedData, selectedEncoding);
    }
}