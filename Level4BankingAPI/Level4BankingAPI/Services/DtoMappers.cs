using Account = Level4BankingAPI.Models.Account;

namespace Level4BankingAPI.Services;

public static class DtoMappers
{
    public static Models.DTOs.Account AsDto(this Account account) 
        => new(account.Id, account.HolderName, account.Balance);
}