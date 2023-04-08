using PropertyRenting.Api.DTOs.Reports;

namespace PropertyRenting.Api.Repositories;

public interface IReportRepository
{
    Task<List<ActiveRenterDTO>> GetActiveRenterAsync();
    Task<List<RenterDueInstallmentDTO>> GetRenterDueInstallmentsAsync(Guid? RenterId,
                                                                      Guid? UnitId,
                                                                      DateTime? ToDate);
    Task<List<OwnerDueInstallmentDTO>> GetOwnerDueInstallmentsAsync(Guid? OwnerId,
                                                                    Guid? BuildingId,
                                                                    DateTime? ToDate);
    Task<List<AccountBalanceDTO>> GetAccountBalanceAsync(Guid? AccountId,
                                                         DateTime? FromDate,
                                                         DateTime? ToDate);
    Task<List<AccountTransactionDTO>> GetAccountTransactionAsync(Guid AccountId,
                                                                 DateTime? FromDate,
                                                                 DateTime? ToDate);
    Task<List<RenterBalanceDTO>> GetRenterBalanceAsync(Guid? RenterId,
                                                       DateTime? FromDate,
                                                       DateTime? ToDate);
    Task<List<RenterTransactionDTO>> GetRenterTransactionAsync(Guid RenterId,
                                                               DateTime? FromDate,
                                                               DateTime? ToDate);
    Task<List<OwnerBalanceDTO>> GetOwnerBalanceAsync(Guid? OwnerId,
                                                     DateTime? FromDate,
                                                     DateTime? ToDate);
    Task<List<OwnerTransactionDTO>> GetOwnerTransactionAsync(Guid OwnerId,
                                                             DateTime? FromDate,
                                                             DateTime? ToDate);
    Task<List<BuildingRevenueExpenseDTO>> GetBuildingRevenueExpenseAsync(Guid? BuildingId,
                                                                         DateTime? ToDate);
    Task<List<BuildingBalanceDTO>> GetBuildingBalanceAsync(Guid? BuildingId,
                                                           DateTime? FromDate,
                                                           DateTime? ToDate);
    Task<List<BuildingTransactionDTO>> GetBuildingTransactionAsync(Guid BuildingId,
                                                                   DateTime? FromDate,
                                                                   DateTime? ToDate);
    Task<List<UnitBalanceDTO>> GetUnitBalanceAsync(Guid? UnitId,
                                                   DateTime? FromDate,
                                                   DateTime? ToDate);
    Task<List<UnitTransactionDTO>> GetUnitTransactionAsync(Guid UnitId,
                                                           DateTime? FromDate,
                                                           DateTime? ToDate);

    Task<List<CashBankBalanceDTO>> GetCashBankBalanceAsync(Guid? CashBankId,
                                                           DateTime? FromDate,
                                                           DateTime? ToDate);
    Task<List<CashBankTransactionDTO>> GetCashBankTransactionAsync(Guid CashBankId,
                                                                   DateTime? FromDate,
                                                                   DateTime? ToDate);

    Task<List<CurrentUnitDTO>> GetCurrentUnitsAsync();
    Task<List<AvailableUnitDTO>> GetAvailableUnitsAsync();
}
