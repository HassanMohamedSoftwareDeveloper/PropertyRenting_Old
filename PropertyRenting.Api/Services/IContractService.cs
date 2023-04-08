using PropertyRenting.Api.Contracts.Requests;

namespace PropertyRenting.Api.Services;

public interface IContractService
{
    Task<bool> AddContractAsync(AddRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateContractAsync(UpdateRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteContractAsync(DeleteRequest request, CancellationToken cancellationToken = default);
}
