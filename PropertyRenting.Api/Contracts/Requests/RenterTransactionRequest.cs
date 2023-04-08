namespace PropertyRenting.Api.Contracts.Requests;

public record RenterTransactionRequest(Guid RenterId, DateTime? FromDate, DateTime? ToDate, string RenterName);
