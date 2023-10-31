using Dapper;
using PropertyRenting.Api.DTOs.Reports;
using PropertyRenting.Api.Enums;
using PropertyRenting.Api.Helpers;
using PropertyRenting.Api.Models.Contexts;

namespace PropertyRenting.Api.Repositories;

public class ReportRepository : IReportRepository
{
    #region Fields :
    private readonly DapperContext _context;
    private readonly QueryHepler _queryHepler;
    #endregion

    #region CTORS :
    public ReportRepository(DapperContext context, QueryHepler queryHelper)
    {
        _context = context;
        _queryHepler = queryHelper;
    }
    #endregion

    #region Methods :
    public async Task<List<ActiveRenterDTO>> GetActiveRenterAsync()
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.ActiveRenters);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<ActiveRenterDTO>(query);
        return result.ToList();
    }

    public async Task<List<RenterDueInstallmentDTO>> GetRenterDueInstallmentsAsync(Guid? RenterId,
                                                                                   Guid? UnitId,
                                                                                   DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.RenterDueInstallments);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<RenterDueInstallmentDTO>(query, new { RenterId, UnitId, ToDate });
        return result.ToList();
    }
    public async Task<List<OwnerDueInstallmentDTO>> GetOwnerDueInstallmentsAsync(Guid? OwnerId,
                                                                                 Guid? BuildingId,
                                                                                 DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.OwnerDueInstallments);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<OwnerDueInstallmentDTO>(query, new { OwnerId, BuildingId, ToDate });
        return result.ToList();
    }
    public async Task<List<AccountBalanceDTO>> GetAccountBalanceAsync(Guid? AccountId,
                                                                      DateTime? FromDate,
                                                                      DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.AccountBalance);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<AccountBalanceDTO>(query, new { AccountId, FromDate, ToDate });
        return result.ToList();
    }
    public async Task<List<AccountTransactionDTO>> GetAccountTransactionAsync(Guid AccountId,
                                                                              DateTime? FromDate,
                                                                              DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.AccountTransaction);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<AccountTransactionDTO>(query, new { AccountId, FromDate, ToDate });
        return result.ToList();
    }
    public async Task<List<RenterBalanceDTO>> GetRenterBalanceAsync(Guid? RenterId,
                                                                    DateTime? FromDate,
                                                                    DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.RenterBalance);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<RenterBalanceDTO>(query, new { RenterId, FromDate, ToDate });
        return result.ToList();
    }
    public async Task<List<RenterTransactionDTO>> GetRenterTransactionAsync(Guid RenterId,
                                                                            DateTime? FromDate,
                                                                            DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.RenterTransaction);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<RenterTransactionDTO>(query, new { RenterId, FromDate, ToDate });
        return result.ToList();
    }
    public async Task<List<OwnerBalanceDTO>> GetOwnerBalanceAsync(Guid? OwnerId,
                                                                  DateTime? FromDate,
                                                                  DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.OwnerBalance);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<OwnerBalanceDTO>(query, new { OwnerId, FromDate, ToDate });
        return result.ToList();
    }
    public async Task<List<OwnerTransactionDTO>> GetOwnerTransactionAsync(Guid OwnerId,
                                                                          DateTime? FromDate,
                                                                          DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.OwnerTransaction);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<OwnerTransactionDTO>(query, new { OwnerId, FromDate, ToDate });
        return result.ToList();
    }
    public async Task<List<BuildingRevenueExpenseDTO>> GetBuildingRevenueExpenseAsync(Guid? BuildingId,
                                                                                      DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.BuildingRevenueExpense);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<BuildingRevenueExpenseDTO>(query, new { BuildingId, ToDate });
        return result.ToList();
    }
    public async Task<List<BuildingBalanceDTO>> GetBuildingBalanceAsync(Guid? BuildingId,
                                                                        DateTime? FromDate,
                                                                        DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.BuildingBalance);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<BuildingBalanceDTO>(query, new { BuildingId, FromDate, ToDate });
        return result.ToList();
    }
    public async Task<List<BuildingTransactionDTO>> GetBuildingTransactionAsync(Guid BuildingId,
                                                                                DateTime? FromDate,
                                                                                DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.BuildingTransaction);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<BuildingTransactionDTO>(query, new { BuildingId, FromDate, ToDate });
        return result.ToList();
    }
    public async Task<List<UnitBalanceDTO>> GetUnitBalanceAsync(Guid? UnitId,
                                                                DateTime? FromDate,
                                                                DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.UnitBalance);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<UnitBalanceDTO>(query, new { UnitId, FromDate, ToDate });
        return result.ToList();
    }
    public async Task<List<UnitTransactionDTO>> GetUnitTransactionAsync(Guid UnitId,
                                                                        DateTime? FromDate,
                                                                        DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.UnitTransaction);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<UnitTransactionDTO>(query, new { UnitId, FromDate, ToDate });
        return result.ToList();
    }

    public async Task<List<CashBankBalanceDTO>> GetCashBankBalanceAsync(Guid? CashBankId,
                                                                        DateTime? FromDate,
                                                                        DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.CashBankBalance);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<CashBankBalanceDTO>(query, new { CashBankId, FromDate, ToDate });
        return result.ToList();
    }
    public async Task<List<CashBankTransactionDTO>> GetCashBankTransactionAsync(Guid CashBankId,
                                                                                DateTime? FromDate,
                                                                                DateTime? ToDate)
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.CashBankTransaction);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<CashBankTransactionDTO>(query, new { CashBankId, FromDate, ToDate });
        return result.ToList();
    }
    public async Task<List<CurrentUnitDTO>> GetCurrentUnitsAsync()
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.CurrentUnits);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<CurrentUnitDTO>(query);
        return result.ToList();
    }
    public async Task<List<AvailableUnitDTO>> GetAvailableUnitsAsync()
    {
        var query = _queryHepler.GetQuery(FolderName.Reports, ReportName.AvailableUnits);
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<AvailableUnitDTO>(query);
        return result.ToList();
    }
    #endregion
}
