using System.IdentityModel.Tokens.Jwt;
using Level4BankingAPI.Models.DTOs.Responses;
using Microsoft.IdentityModel.Tokens;

namespace Level4BankingAPI.Services;

public class AuthenticationService
{
    private readonly IConfiguration _configuration;

    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public TokenResponse CreateToken()
    {
        var securityKey = new SymmetricSecurityKey(
            Convert.FromBase64String(_configuration["Authentication:SecretForKey"]!));
        
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            null,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        
        return new TokenResponse(tokenToReturn);
    }
}