using System.Text;
using IFormattable = Level4BankingAPI.Interfaces.IFormattable;

namespace Level4BankingAPI.Models.DTOs.Responses;

public record GetAccountsResponse(List<Account> Accounts) : IFormattable
{
    public string Format()
    {
        var buffer = new StringBuilder();
        ((IFormattable)Accounts[0]).CreateHeader(buffer);
        
        foreach (IFormattable account in Accounts)
        {
            buffer.Append('\n');
            account.CreateRow(buffer);
        }

        return buffer.ToString();
    }
}