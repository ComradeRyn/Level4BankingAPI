using Microsoft.EntityFrameworkCore;

namespace Level4BankingAPI.Models;

public class AccountContext(DbContextOptions<AccountContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; init; }
}