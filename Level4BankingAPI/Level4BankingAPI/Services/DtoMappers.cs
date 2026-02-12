using Level4BankingAPI.Models.DTOs.Responses;
using Account = Level4BankingAPI.Models.Account;

namespace Level4BankingAPI.Services;

public static class DtoMappers
{
    public static Models.DTOs.Account AsDto(this Account account) 
        => new(account.Id, account.HolderName, account.Balance);

    public static GetAccountsResponse AsDto(this IEnumerable<Account> accounts)
        => new(accounts.Select(account => account.AsDto()).ToList());
}