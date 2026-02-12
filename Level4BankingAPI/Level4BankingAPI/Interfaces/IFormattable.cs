using System.Reflection;
using System.Text;

namespace Level4BankingAPI.Interfaces;

public interface IFormattable
{
    string Format()
    {
        var buffer = new StringBuilder();
        CreateHeader(buffer);
        buffer.Append('\n');
        CreateRow(buffer);

        return buffer.ToString();
    }

    void CreateHeader(StringBuilder buffer)
    {
        var propertyInfos = GetType().GetProperties();
        buffer.Append(propertyInfos[0].Name);
        for (var i = 1; i < propertyInfos.Length; i++)
        {
            buffer.Append(',');
            buffer.Append(propertyInfos[i].Name);
        }
    }

    void CreateRow(StringBuilder buffer)
    {
        var propertyInfos = GetType().GetProperties();
        if (propertyInfos.Length != 0)
        {
            AppendValue(propertyInfos[0], buffer);
        }
        
        for (var i = 1; i < propertyInfos.Length; i++)
        {
            buffer.Append(',');
            AppendValue(propertyInfos[i], buffer);
        }
    }

    private void AppendValue(PropertyInfo propertyInfo, StringBuilder buffer)
    {
        if (propertyInfo.PropertyType == typeof(string))
        {
            buffer.Append($"\"{propertyInfo.GetValue(this)}\"");
        }
            
        else
        {
            buffer.Append(propertyInfo.GetValue(this));
        }
    }
}