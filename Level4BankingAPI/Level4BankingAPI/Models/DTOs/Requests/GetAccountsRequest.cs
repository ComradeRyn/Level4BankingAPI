namespace Level4BankingAPI.Models.DTOs.Requests;

public record GetAccountsRequest(
    string? Name,
    string? SortType,
    string? SortOrder,
    int PageNumber,
    int PageSize);