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

   private void CreateHeader(StringBuilder buffer)
    {
        var propertyInfos = GetType().GetProperties();
        buffer.Append(propertyInfos[0].Name);
        for (var i = 1; i < propertyInfos.Length; i++)
        {
            buffer.Append(',');
            buffer.Append(propertyInfos[i].Name);
        }
    }

    static void CreateHeader(Type type, StringBuilder buffer)
    {
        var propertyInfos = type.GetProperties();
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
            buffer.Append(propertyInfos[0].GetValue(this));
        }
        
        for (var i = 1; i < propertyInfos.Length; i++)
        {
            buffer.Append(',');
            buffer.Append(propertyInfos[i].GetValue(this));
        }
    }
}