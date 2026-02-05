namespace Level4BankingAPI.Models.DTOs;

public record Account(string Id, 
    string HolderName, 
    decimal Amount);