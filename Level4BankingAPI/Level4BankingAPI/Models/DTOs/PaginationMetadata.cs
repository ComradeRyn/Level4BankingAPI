namespace Level4BankingAPI.Models.DTOs;

public class PaginationMetadata(int totalItemCount, int pageSize, int currentPage)
{
    private int TotalItemCount { get; } = totalItemCount;
    public int TotalPageCount => (int)Math.Ceiling(TotalItemCount / (double)PageSize);
    private int PageSize { get; } = pageSize;
    public int CurrentPage { get; } = currentPage;
}