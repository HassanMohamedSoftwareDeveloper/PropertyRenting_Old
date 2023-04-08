namespace PropertyRenting.Api.Contracts.Requests
{
    public record UnitTransactionRequest(Guid UnitId, DateTime? FromDate, DateTime? ToDate, string UnitName);
}
