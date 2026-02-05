namespace Level4BankingAPI.Models.DTOs;

public record PaginationMetadata(int TotalItemCount,
    int TotalPageCount,
    int PageSize,
    int CurrentPage);