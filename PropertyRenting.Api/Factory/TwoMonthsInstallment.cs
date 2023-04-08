using PropertyRenting.Api.DTOs;

namespace PropertyRenting.Api.Factory;

public class TwoMonthsInstallment : InstallmentHelper, IInstallment
{
    private const int _monthAdded = 2;
    public List<ContractFinancialTransactionDTO> CreateInstallments(Guid contractId,
                                                                    decimal contractAmount,
                                                                    DateTime fromDate,
                                                                    DateTime toDate)
    {
        DateTime dueDate = fromDate;

        List<ContractFinancialTransactionDTO> installments = new();

        decimal amountPerMonth = GetInstallmentAmountPerMonth(contractAmount, this.GetMonthCount(fromDate, toDate));

        while (dueDate.Date < toDate.Date)
        {
            installments.Add(CreateInstallmentObj(contractId, dueDate, GetInstallmentAmount(dueDate, toDate, _monthAdded, amountPerMonth)));
            dueDate = dueDate.AddMonths(_monthAdded);
        }
        return installments;
    }
}
