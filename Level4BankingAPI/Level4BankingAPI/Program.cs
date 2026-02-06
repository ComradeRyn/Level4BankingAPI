using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Level4BankingAPI.Clients;
using Level4BankingAPI.Formatters;
using Level4BankingAPI.Interfaces;
using Level4BankingAPI.Middleware;
using Level4BankingAPI.Models;
using Level4BankingAPI.Repositories;
using Level4BankingAPI.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    
    options.OutputFormatters.Insert(1, new CsvOutputFormatter());
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    
    setupAction.IncludeXmlComments(xmlCommentsFullPath);
    
    setupAction.AddSecurityDefinition("BankingApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(document => new OpenApiSecurityRequirement()
    {
        [new OpenApiSecuritySchemeReference("BankingApiBearerAuth", document)] = []
    });
});

builder.Services.AddDbContext<AccountContext>(opt => opt.UseInMemoryDatabase("AccountList"));

builder.Services.AddHttpClient<ICurrencyClient, CurrencyClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiWebAddress"]!);
});

builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
builder.Services.AddScoped<AccountsService>();
builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Authentication:Issuer"],
                ValidAudience = builder.Configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    // TODO: take a look and see if there is a better way
                    Convert.FromBase64String(builder.Configuration["Authentication:SecretForKey"]!))
            };
        }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{ 
    app.UseMiddleware<ExceptionHandlingMiddleware>();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();