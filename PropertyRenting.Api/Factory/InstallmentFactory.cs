using PropertyRenting.Api.Enums;

namespace PropertyRenting.Api.Factory;

public static class InstallmentFactory
{
    public static IInstallment Create(PaymentMethod paymentMethod)
    {
        return paymentMethod switch
        {
            PaymentMethod.Monthly => new MonthlyInstallment(),
            PaymentMethod.Quarter => new QuarterInstallment(),
            PaymentMethod.TwoMonths => new TwoMonthsInstallment(),
            PaymentMethod.HalfYear => new HalfYearInstallment(),
            PaymentMethod.Yearly => new YearlyInstallment(),
            _ => throw new NotImplementedException(),
        };
    }
}
