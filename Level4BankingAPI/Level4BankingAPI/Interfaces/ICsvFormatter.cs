namespace Level4BankingAPI.Interfaces;

public interface ICsvFormatter
{
    string FormatCsv();
    string CreateHeader();
}