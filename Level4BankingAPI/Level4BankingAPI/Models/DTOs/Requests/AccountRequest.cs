namespace Level4BankingAPI.Models.DTOs.Requests;

public record AccountRequest<T>(string Id, T Content);