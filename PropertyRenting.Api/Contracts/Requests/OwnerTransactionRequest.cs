namespace PropertyRenting.Api.Contracts.Requests;

public record OwnerTransactionRequest(Guid OwnerId, DateTime? FromDate, DateTime? ToDate, string OwnerName);
