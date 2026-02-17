namespace Level4BankingAPI.Models.DTOs.Requests;

public record GetAccountsRequest(
    string? Name,
    string? SortBy,
    bool IsDescending,
    int PageNumber,
    int PageSize);