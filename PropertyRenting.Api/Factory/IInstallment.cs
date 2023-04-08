using PropertyRenting.Api.DTOs;

namespace PropertyRenting.Api.Factory;

public interface IInstallment
{
    List<ContractFinancialTransactionDTO> CreateInstallments(Guid contractId,
                                                             decimal amount,
                                                             DateTime fromDate,
                                                             DateTime toDate);
}
