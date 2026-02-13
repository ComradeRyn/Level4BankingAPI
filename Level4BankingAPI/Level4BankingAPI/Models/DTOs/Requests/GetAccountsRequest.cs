namespace Level4BankingAPI.Models.DTOs.Requests;

public record GetAccountsRequest(
    string? Name,
    string? SortBy,
    bool Reverse,
    int PageNumber,
    int PageSize)
{
    public string? SortBy { get; set; } = SortBy;
    public int PageSize { get; set; } = PageSize;
    public int PageNumber { get; set; } = PageNumber;
}