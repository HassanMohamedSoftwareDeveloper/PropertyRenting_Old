using PropertyRenting.Api.Enums;

namespace PropertyRenting.Api.Contracts.Requests
{
    public record AddRequest(int ContractNumber, string BuildingId, string OwnerId, string Description, DateTime ContractDate, DateTime ContractStartDate, DateTime ContractEndDate, decimal ContractAmount, PaymentMethod PaymentMethod);
    public record UpdateRequest(string Id, int ContractNumber, string BuildingId, string OwnerId, string Description, DateTime ContractDate, DateTime ContractStartDate, DateTime ContractEndDate, decimal ContractAmount, PaymentMethod PaymentMethod);
    public record DeleteRequest(string Id);

    public record GetRequest(string Id);
    public record GetAllRequest();
    public record SearchRequest();
    public record GetPageRequest(int PageNumber, int PageSize);
}
