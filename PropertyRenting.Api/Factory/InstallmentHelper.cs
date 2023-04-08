using PropertyRenting.Api.DTOs;

namespace PropertyRenting.Api.Factory;

public abstract class InstallmentHelper
{

    public void TimeSpanToDate(DateTime d1, DateTime d2, out int years, out int months, out int days)
    {
        if (d1 < d2)
        {
            DateTime d3 = d2;
            d2 = d1;
            d1 = d3;
        }

        months = 12 * (d1.Year - d2.Year) + (d1.Month - d2.Month);


        if (d1.Day < d2.Day)
        {
            months--;
            days = DateTime.DaysInMonth(d2.Year, d2.Month) - d2.Day + d1.Day;
        }
        else
        {
            days = d1.Day - d2.Day;
        }
        years = months / 12;
        months -= years * 12;
    }
    public virtual int GetMonthCount(DateTime fromDate, DateTime toDate)
    {

        int fromDay = fromDate.Day;
        int fromMonth = fromDate.Month;
        int fromYear = fromDate.Year;

        int toDay = toDate.Day;
        int toMonth = toDate.Month;
        int toYear = toDate.Year;


        int monthCount = 0;
        monthCount += (toYear - fromYear) * 12;
        monthCount += (toMonth - fromMonth);
        monthCount += (toDay - fromDay) >= 27 ? 1 : 0;
        return monthCount;
    }
    public virtual decimal GetInstallmentAmountPerMonth(decimal contractAmount, int totalMonthCount)
    {
        return contractAmount / totalMonthCount;
    }
    public virtual ContractFinancialTransactionDTO CreateInstallmentObj(Guid contractId, DateTime dueDate, decimal amount)
    {
        return new ContractFinancialTransactionDTO
        {
            Id = Guid.NewGuid(),
            ContractId = contractId,
            DueDate = dueDate,
            ContractAdditionId = null,
            IsPaid = false,
            IsCancelled = false,
            Amount = amount,
            Balance = amount
        };
    }
    public virtual decimal GetInstallmentAmount(DateTime dueDate, DateTime toDate, int monthAdded, decimal amountPerMonth)
    {
        int monthCount = GetMonthCount(dueDate, toDate);
        decimal installmentAmount = monthCount < monthAdded ? amountPerMonth * monthCount : amountPerMonth * monthAdded;
        return installmentAmount;
    }
}
