using System.Text;

namespace Level4BankingAPI.Interfaces;

public interface ICsvFormatter
{
    string Format();
    string CreateHeader();
}