namespace Level4BankingAPI.Models.DTOs;

public record PaginationMetadata
{
    public int TotalPageCount => (int)Math.Ceiling(TotalItemCount / (double)PageSize);
    public int CurrentPage { get; }
    private int PageSize { get; }
    private int TotalItemCount { get; }

    public PaginationMetadata(int totalItemCount, int pageSize, int currentPage)
    {
        TotalItemCount = totalItemCount;
        PageSize = pageSize;
        CurrentPage = currentPage;
    }
}