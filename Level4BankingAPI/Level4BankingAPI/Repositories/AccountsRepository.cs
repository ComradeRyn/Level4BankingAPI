using Level4BankingAPI.Interfaces;
using Level4BankingAPI.Models;
using Level4BankingAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Account = Level4BankingAPI.Models.Account;

namespace Level4BankingAPI.Repositories;

public class AccountsRepository : IAccountsRepository
{
    private readonly AccountContext _context;

    public AccountsRepository(AccountContext context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Account>, PaginationMetadata)> GetAccounts(string? name, 
        string? sortType, 
        bool isDescending, 
        int pageNumber, 
        int pageSize)
    {
        var collection = _context.Accounts as IQueryable<Account>;
        if (!string.IsNullOrWhiteSpace(name))
        {
            name = name.ToLower().Trim();
            collection = collection.Where(account => account.HolderName.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(sortType))
        {
            switch (sortType.ToLower().Trim())
            {
                case "name":
                    collection = collection.OrderBy(account => account.HolderName);
                    break;
                case "balance":
                    collection = collection.OrderBy(account => account.Balance);
                    break;
            }
        }

        var itemCount = await collection.CountAsync();
        var paginationMetadata = new PaginationMetadata(itemCount,
            pageSize,
            pageNumber);
        
        var collectionToReturn = await collection.Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
        
        if (isDescending)
        {
            collectionToReturn.Reverse();
        }

        return (collectionToReturn, paginationMetadata);
    }

    public async Task<Account?> GetAccount(string id)
        => await _context.Accounts.FindAsync(id);

    public async Task<Account> AddAccount(string name)
    {
        var account = new Account
        {
            Id = Guid.NewGuid().ToString(),
            HolderName = name,
        };
        
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();

        return account;
    }

    public async Task<Account> UpdateAccount(Account account, decimal amount)
    {
        account.Balance += amount;
        await _context.SaveChangesAsync();

        return account;
    }
}