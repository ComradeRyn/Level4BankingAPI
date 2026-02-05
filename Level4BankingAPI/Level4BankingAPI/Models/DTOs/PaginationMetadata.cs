namespace Level4BankingAPI.Models.DTOs;

public record PaginationMetadata
{
    // TODO: revisit this class's properties for possible refactoring
    private int TotalItemCount { get; }
    public int TotalPageCount { get; }
    private int PageSize { get; }
    public int CurrentPage { get; }

    public PaginationMetadata(int totalItemCount, int pageSize, int currentPage)
    {
        TotalItemCount = totalItemCount;
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
    }
}