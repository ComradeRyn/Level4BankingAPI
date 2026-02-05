namespace Level4BankingAPI.Models.DTOs.Requests;

public record GetAccountsRequest(
    string? Name,
    string? SortType,
    bool Reverse,
    int PageNumber,
    int PageSize);