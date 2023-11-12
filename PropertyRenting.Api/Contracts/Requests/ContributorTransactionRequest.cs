namespace PropertyRenting.Api.Contracts.Requests;

public record ContributorTransactionRequest(Guid ContributorId, DateTime? FromDate, DateTime? ToDate, string Contributor);