namespace Level4BankingAPI.Models.DTOs.Requests;

public record GetAccountsRequest(
    string? Name,
    string? SortBy,
    bool Reverse,
    int PageNumber,
    int PageSize)
{
    public int PageSize { get; set; } = PageSize;
}