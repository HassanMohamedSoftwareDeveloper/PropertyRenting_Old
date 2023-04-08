using AutoMapper;
using PropertyRenting.Api.Contracts.Requests;
using PropertyRenting.Api.Models.Entities;
using PropertyRenting.Api.Repositories;
using PropertyRenting.Api.UOW;

namespace PropertyRenting.Api.Services;

public class ContractService : BaseService, IContractService
{
    private readonly IContractRepository _contractRepository;

    public ContractService(IMapper mapper, IUnitOfWork unitOfWork, IContractRepository contractRepository) : base(mapper, unitOfWork)
    {
        _contractRepository = contractRepository;
    }

    public async Task<bool> AddContractAsync(AddRequest request, CancellationToken cancellationToken = default)
    {
        var contract = this.Mapper.Map<OwnerContractEntity>(request);
        this._contractRepository.Create(contract);
        return await this.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateContractAsync(UpdateRequest request, CancellationToken cancellationToken = default)
    {
        var contract = this.Mapper.Map<OwnerContractEntity>(request);
        this._contractRepository.Update(contract);
        return await this.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
    public async Task<bool> DeleteContractAsync(DeleteRequest request, CancellationToken cancellationToken = default)
    {
        var contract = await this._contractRepository.GetEntityByIdAsync(Guid.Parse(request.Id), cancellationToken);
        return true;
    }

}
