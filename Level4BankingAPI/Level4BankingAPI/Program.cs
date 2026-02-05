using Microsoft.EntityFrameworkCore;
using Level4BankingAPI.Clients;
using Level4BankingAPI.Interfaces;
using Level4BankingAPI.Models;
using Level4BankingAPI.Repositories;
using Level4BankingAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AccountContext>(opt => opt.UseInMemoryDatabase("AccountList"));

builder.Services.AddHttpClient<ICurrencyClient, CurrencyClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiWebAddress"]!);
});

builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
builder.Services.AddScoped<AccountsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();