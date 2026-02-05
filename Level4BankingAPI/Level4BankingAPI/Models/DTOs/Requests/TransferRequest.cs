namespace Level4BankingAPI.Models.DTOs.Requests;

public record TransferRequest(decimal Amount, 
    string SenderId, 
    string ReceiverId);