namespace PropertyRenting.Api.Contracts.Requests;

public record RenterBalanceRequest(Guid? RenterId, DateTime? FromDate, DateTime? ToDate);
