namespace Level4BankingAPI.Interfaces;

public interface ICsvFormatter
{
    string CreateBody();
    string CreateHeader();
}