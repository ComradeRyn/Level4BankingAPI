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
        string? sortBy, 
        bool isDescending, 
        int pageNumber, 
        int pageSize)
    {
        var query = _context.Accounts as IQueryable<Account>;
        if (!string.IsNullOrWhiteSpace(name))
        {
            name = name.Trim();
            query = query.Where(account => account.HolderName.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            query = (sortBy, isDescending) switch
            {
                ("name", true) => query.OrderByDescending(account => account.HolderName),
                ("balance", true) => query.OrderByDescending(account => account.Balance),
                ("name", false) => query.OrderBy(account => account.HolderName),
                ("balance", false) => query.OrderBy(account => account.Balance),
                _ => null
            };
        }

        if (query is null)
        {
            return (new List<Account>(), 
                new PaginationMetadata(0, pageSize, pageNumber));
        }
        
        var itemCount = await query.CountAsync();
        var paginationMetadata = new PaginationMetadata(itemCount,
            pageSize,
            pageNumber);
        
        var accounts = await query.Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (accounts, paginationMetadata);
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