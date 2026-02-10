using Level4BankingAPI.Models.DTOs;
using Account = Level4BankingAPI.Models.Account;

namespace Level4BankingAPI.Interfaces;

public interface IAccountsRepository
{
    Task<(IEnumerable<Account>, PaginationMetadata)> GetAccounts(string? name, 
        string? sortType, 
        bool IsDescending,
        int pageNumber, 
        int pageSize);
    Task<Account?> GetAccount(string id);
    Task<Account> AddAccount(string name);
    Task<Account> UpdateAccount(Account account, decimal amount);
}