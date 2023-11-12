namespace PropertyRenting.Api.Contracts.Requests;

public record ContributorBalanceRequest(Guid? ContributorId, DateTime? FromDate, DateTime? ToDate);
