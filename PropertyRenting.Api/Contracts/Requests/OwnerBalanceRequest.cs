namespace PropertyRenting.Api.Contracts.Requests;

public record OwnerBalanceRequest(Guid? OwnerId, DateTime? FromDate, DateTime? ToDate);
