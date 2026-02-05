namespace Level4BankingAPI.Models.Constants;

public static class Messages
{
    public const string NotFound = "Requested Account could not be found";
    public const string InsufficientBalance = "Requested amount must be less or equal to current balance";
    public const string NoNegativeAmount = "Requested amount must be positive";
    public const string InvalidName = "Requested name must follow the patter of <First> <Middle> <Last>";
    // TODO: find if there is a way to just put the max page size in this string
    public const string InvalidPageSize = "Requested page size must be less than or equal to 20";
    public const string InvalidSearchType = "Requested search type must be balance or name";
}