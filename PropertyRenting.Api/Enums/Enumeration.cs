using System.ComponentModel;

namespace PropertyRenting.Api.Enums;

public enum BuildingType
{
    Building = 1,
    Mall = 2,
    Housing = 3,
    Managerial = 4
}
public enum ConstructionStatus
{
    New = 1,
    Middle = 2,
    Old = 3
}
public enum PaymentMethod
{
    Monthly = 1,
    Quarter = 2,
    TwoMonths = 3,
    HalfYear = 4,
    Yearly = 5,
}
public enum ContractState
{
    Open = 1,
    Activated = 2,
    Canceled = 3,
    Closed = 4
}
public enum TransactionType
{
    Due = 1,
    Liability = 2
}
public enum Actions
{
    Added,
    Updated,
    Deleted
}
public enum RefType
{
    Receipt,
    Exchange,
    RenterContract,
    OwnerContract
}
public enum AccountType
{
    Asset = 1,
    Liability = 2,
    Expenses = 3,
    Revenue = 4,
    Total = 5
}
public enum VoucherState
{
    Open = 1,
    Posted = 2

}

public enum SanadType
{
    General = 1,
    Renter = 2,
    Owner = 3,
    Contributer = 4,
}


public enum FolderName
{
    [Description("Reports")]
    Reports = 1,
}
public enum ReportName
{
    [Description("Get Active Renters")]
    ActiveRenters = 1,
    [Description("Get Renter Due-Installments")]
    RenterDueInstallments,
    [Description("Get Owner Due-Installments")]
    OwnerDueInstallments,
    [Description("Get Account Balance")]
    AccountBalance,
    [Description("Get Account Transaction")]
    AccountTransaction,
    [Description("Get Renter Balance")]
    RenterBalance,
    [Description("Get Renter Transaction")]
    RenterTransaction,
    [Description("Get Owner Balance")]
    OwnerBalance,
    [Description("Get Owner Transaction")]
    OwnerTransaction,
    [Description("Get Building Balance")]
    BuildingBalance,
    [Description("Get Building Transaction")]
    BuildingTransaction,
    [Description("Get Unit Balance")]
    UnitBalance,
    [Description("Get Unit Transaction")]
    UnitTransaction,
    [Description("Get CashBank Balance")]
    CashBankBalance,
    [Description("Get CashBank Transaction")]
    CashBankTransaction,
    [Description("Get Current Units")]
    CurrentUnits,
    [Description("Get Available Units")]
    AvailableUnits,
    [Description("Get Building Revenue Expense")]
    BuildingRevenueExpense,
    [Description("Get Contributor Balance")]
    ContributorBalance,
    [Description("Get Contributor Transaction")]
    ContributorTransaction
}