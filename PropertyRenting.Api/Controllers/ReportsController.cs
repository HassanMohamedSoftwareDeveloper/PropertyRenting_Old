using Microsoft.Reporting.NETCore;
using PropertyRenting.Api.Contracts.Requests;
using PropertyRenting.Api.Repositories;

namespace PropertyRenting.Api.Controllers;

public class ReportsController : BaseController
{
    #region Fields :
    private readonly IReportRepository _reportRepository;
    private readonly IWebHostEnvironment _env;
    #endregion

    #region CTORS :
    public ReportsController(AppDbContext context, IMapper mapper, IReportRepository reportRepository, IWebHostEnvironment env) : base(context, mapper)
    {
        _reportRepository = reportRepository;
        _env = env;
    }
    #endregion

    #region Displaying :
    [HttpGet("renters/active")]
    public async Task<IActionResult> GetActiveRentersAsync()
    {
        var data = await _reportRepository.GetActiveRenterAsync();
        return Ok(data);
    }
    [HttpPost("building/{id}/revenues-expenses")]
    public async Task<IActionResult> GetBuildingRevenueExpenses(GetBuildingRevenueExpenseRequest request)
    {
        var data = await _reportRepository.GetBuildingRevenueExpenseAsync(request.BuildingId, request.ToDate);
        return Ok(data);
    }

    [HttpPost("renter/due-installments")]
    public async Task<IActionResult> GetRenterDueInstallmentsAsync(RenterDueInstallmentRequest request)
    {
        var data = await _reportRepository.GetRenterDueInstallmentsAsync(request.RenterId, request.UnitId, request.ToDate);
        return Ok(data);
    }
    [HttpPost("owner/due-installments")]
    public async Task<IActionResult> GetOwnerDueInstallmentsAsync(OwnerDueInstallmentRequest request)
    {
        var data = await _reportRepository.GetOwnerDueInstallmentsAsync(request.OwnerId, request.BuildingId, request.ToDate);
        return Ok(data);
    }

    [HttpPost("account/balance")]
    public async Task<IActionResult> GetAccountBalanceAsync(AccountBalanceRequest request)
    {
        var data = await _reportRepository.GetAccountBalanceAsync(request.AccountId, request.FromDate, request.ToDate);
        return Ok(data);
    }
    [HttpPost("account/transaction")]
    public async Task<IActionResult> GetAccountTransactionAsync(AccountTransactionRequest request)
    {
        var data = await _reportRepository.GetAccountTransactionAsync(request.AccountId, request.FromDate, request.ToDate);
        return Ok(data);
    }

    [HttpPost("renter/balance")]
    public async Task<IActionResult> GetRenterBalanceAsync(RenterBalanceRequest request)
    {
        var data = await _reportRepository.GetRenterBalanceAsync(request.RenterId, request.FromDate, request.ToDate);
        return Ok(data);
    }
    [HttpPost("renter/transaction")]
    public async Task<IActionResult> GetRenterTransactionAsync(RenterTransactionRequest request)
    {
        var data = await _reportRepository.GetRenterTransactionAsync(request.RenterId, request.FromDate, request.ToDate);
        return Ok(data);
    }

    [HttpPost("owner/balance")]
    public async Task<IActionResult> GetOwnerBalanceAsync(OwnerBalanceRequest request)
    {
        var data = await _reportRepository.GetOwnerBalanceAsync(request.OwnerId, request.FromDate, request.ToDate);
        return Ok(data);
    }
    [HttpPost("owner/transaction")]
    public async Task<IActionResult> GetOwnerTransactionAsync(OwnerTransactionRequest request)
    {
        var data = await _reportRepository.GetOwnerTransactionAsync(request.OwnerId, request.FromDate, request.ToDate);
        return Ok(data);
    }

    [HttpPost("building/balance")]
    public async Task<IActionResult> GetBuildingBalanceAsync(BuildingBalanceRequest request)
    {
        var data = await _reportRepository.GetBuildingBalanceAsync(request.BuildingId, request.FromDate, request.ToDate);
        return Ok(data);
    }
    [HttpPost("building/transaction")]
    public async Task<IActionResult> GetBuildingTransactionAsync(BuildingTransactionRequest request)
    {
        var data = await _reportRepository.GetBuildingTransactionAsync(request.BuildingId, request.FromDate, request.ToDate);
        return Ok(data);
    }

    [HttpPost("unit/balance")]
    public async Task<IActionResult> GetUnitBalanceAsync(UnitBalanceRequest request)
    {
        var data = await _reportRepository.GetUnitBalanceAsync(request.UnitId, request.FromDate, request.ToDate);
        return Ok(data);
    }
    [HttpPost("unit/transaction")]
    public async Task<IActionResult> GetUnitTransactionAsync(UnitTransactionRequest request)
    {
        var data = await _reportRepository.GetUnitTransactionAsync(request.UnitId, request.FromDate, request.ToDate);
        return Ok(data);
    }

    [HttpPost("cash-bank/balance")]
    public async Task<IActionResult> GetCashBankBalanceAsync(CashBankBalanceRequest request)
    {
        var data = await _reportRepository.GetCashBankBalanceAsync(request.CashBankId, request.FromDate, request.ToDate);
        return Ok(data);
    }
    [HttpPost("cash-bank/transaction")]
    public async Task<IActionResult> GetCashBankTransactionAsync(CashBankTransactionRequest request)
    {
        var data = await _reportRepository.GetCashBankTransactionAsync(request.CashBankId, request.FromDate, request.ToDate);
        return Ok(data);
    }

    [HttpGet("unit/current")]
    public async Task<IActionResult> GetCurrentUnitsAsync()
    {
        var data = await _reportRepository.GetCurrentUnitsAsync();
        return Ok(data);
    }
    [HttpGet("unit/available")]
    public async Task<IActionResult> GetAvailableUnitsAsync()
    {
        var data = await _reportRepository.GetAvailableUnitsAsync();
        return Ok(data);
    }
    #endregion

    #region Exports :
    [HttpGet("renters/active/export/{type}")]
    public async Task<IActionResult> ExportActiveRentersAsync(string type)
    {
        var data = await _reportRepository.GetActiveRenterAsync();
        return ExportReport(type, ReportName.ActiveRenters.ToString(), data);
    }
    [HttpPost("building/{id}/revenues-expenses/export/{type}")]
    public async Task<IActionResult> ExportBuildingRevenueExpenses(string type, GetBuildingRevenueExpenseRequest request)
    {
        var data = await _reportRepository.GetBuildingRevenueExpenseAsync(request.BuildingId, request.ToDate);

        return ExportReport(type, ReportName.BuildingRevenueExpense.ToString(),
            data, new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd")),
            new ReportParameter("BuildingId", request.BuildingId?.ToString()));
    }

    [HttpPost("renter/due-installments/export/{type}")]
    public async Task<IActionResult> ExportRenterDueInstallmentsAsync(string type, RenterDueInstallmentRequest request)
    {
        var data = await _reportRepository.GetRenterDueInstallmentsAsync(request.RenterId, request.UnitId, request.ToDate);
        return ExportReport(type, ReportName.RenterDueInstallments.ToString(),
        data,
        new ReportParameter("RenterId", request.RenterId?.ToString()),
        new ReportParameter("UnitId", request.UnitId?.ToString()),
        new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd"))
        );
    }
    [HttpPost("owner/due-installments/export/{type}")]
    public async Task<IActionResult> ExportOwnerDueInstallmentsAsync(string type, OwnerDueInstallmentRequest request)
    {
        var data = await _reportRepository.GetOwnerDueInstallmentsAsync(request.OwnerId, request.BuildingId, request.ToDate);
        return ExportReport(type, ReportName.OwnerDueInstallments.ToString(),
       data,
        new ReportParameter("OwnerId", request.OwnerId?.ToString()),
        new ReportParameter("BuildingId", request.BuildingId?.ToString()),
       new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd"))
       );
    }

    [HttpPost("account/balance/export/{type}")]
    public async Task<IActionResult> ExportAccountBalanceAsync(string type, AccountBalanceRequest request)
    {
        var data = await _reportRepository.GetAccountBalanceAsync(request.AccountId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.AccountBalance.ToString(), data,
        new ReportParameter("AccountId", request.AccountId?.ToString()),
        new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
        new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd"))
       );
    }
    [HttpPost("account/transaction/export/{type}")]
    public async Task<IActionResult> ExportAccountTransactionAsync(string type, AccountTransactionRequest request)
    {
        var data = await _reportRepository.GetAccountTransactionAsync(request.AccountId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.AccountTransaction.ToString(), data,
             new ReportParameter("AccountId", request.AccountId.ToString()),
             new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
             new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd")),
             new ReportParameter("AccountName", request.AccountName));
    }

    [HttpPost("renter/balance/export/{type}")]
    public async Task<IActionResult> ExportRenterBalanceAsync(string type, RenterBalanceRequest request)
    {
        var data = await _reportRepository.GetRenterBalanceAsync(request.RenterId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.RenterBalance.ToString(), data,
             new ReportParameter("RenterId", request.RenterId?.ToString()),
new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd"))
);
    }
    [HttpPost("renter/transaction/export/{type}")]
    public async Task<IActionResult> ExportRenterTransactionAsync(string type, RenterTransactionRequest request)
    {
        var data = await _reportRepository.GetRenterTransactionAsync(request.RenterId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.RenterTransaction.ToString(), data,
            new ReportParameter("RenterId", request.RenterId.ToString()),
new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd")),
new ReportParameter("RenterName", request.RenterName)
);
    }

    [HttpPost("owner/balance/export/{type}")]
    public async Task<IActionResult> ExportOwnerBalanceAsync(string type, OwnerBalanceRequest request)
    {
        var data = await _reportRepository.GetOwnerBalanceAsync(request.OwnerId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.OwnerBalance.ToString(), data,
            new ReportParameter("OwnerId", request.OwnerId?.ToString()),
new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd"))
);
    }
    [HttpPost("owner/transaction/export/{type}")]
    public async Task<IActionResult> ExportOwnerTransactionAsync(string type, OwnerTransactionRequest request)
    {
        var data = await _reportRepository.GetOwnerTransactionAsync(request.OwnerId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.OwnerTransaction.ToString(), data,
            new ReportParameter("OwnerId", request.OwnerId.ToString()),
new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd")),
new ReportParameter("OwnerName", request.OwnerName)
);
    }

    [HttpPost("building/balance/export/{type}")]
    public async Task<IActionResult> ExportBuildingBalanceAsync(string type, BuildingBalanceRequest request)
    {
        var data = await _reportRepository.GetBuildingBalanceAsync(request.BuildingId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.BuildingBalance.ToString(), data,
            new ReportParameter("BuildingId", request.BuildingId?.ToString()),
new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd"))
);
    }
    [HttpPost("building/transaction/export/{type}")]
    public async Task<IActionResult> ExportBuildingTransactionAsync(string type, BuildingTransactionRequest request)
    {
        var data = await _reportRepository.GetBuildingTransactionAsync(request.BuildingId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.BuildingTransaction.ToString(), data,
            new ReportParameter("BuildingId", request.BuildingId.ToString()),
new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd")),
new ReportParameter("BuildingName", request.BuildingName)
);
    }

    [HttpPost("unit/balance/export/{type}")]
    public async Task<IActionResult> ExportUnitBalanceAsync(string type, UnitBalanceRequest request)
    {
        var data = await _reportRepository.GetUnitBalanceAsync(request.UnitId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.UnitBalance.ToString(), data,
            new ReportParameter("UnitId", request.UnitId?.ToString()),
new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd"))
);
    }
    [HttpPost("unit/transaction/export/{type}")]
    public async Task<IActionResult> ExportUnitTransactionAsync(string type, UnitTransactionRequest request)
    {
        var data = await _reportRepository.GetUnitTransactionAsync(request.UnitId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.UnitTransaction.ToString(), data,
new ReportParameter("UnitId", request.UnitId.ToString()),
new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd")),
new ReportParameter("UnitName", request.UnitName)
);
    }

    [HttpPost("cash-bank/balance/export/{type}")]
    public async Task<IActionResult> ExportCashBankBalanceAsync(string type, CashBankBalanceRequest request)
    {
        var data = await _reportRepository.GetCashBankBalanceAsync(request.CashBankId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.CashBankBalance.ToString(), data,
            new ReportParameter("CashBankId", request.CashBankId?.ToString()),
            new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
            new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd")));
    }
    [HttpPost("cash-bank/transaction/export/{type}")]
    public async Task<IActionResult> ExportCashBankTransactionAsync(string type, CashBankTransactionRequest request)
    {
        var data = await _reportRepository.GetCashBankTransactionAsync(request.CashBankId, request.FromDate, request.ToDate);
        return ExportReport(type, ReportName.CashBankTransaction.ToString(), data,
            new ReportParameter("CashBankId", request.CashBankId.ToString()),
            new ReportParameter("FromDate", request.FromDate?.ToString("yyyy-MM-dd")),
            new ReportParameter("ToDate", request.ToDate?.ToString("yyyy-MM-dd")),
            new ReportParameter("CashBankName", request.CashBankName));
    }

    [HttpGet("unit/current/export/{type}")]
    public async Task<IActionResult> ExportCurrentUnitsAsync(string type)
    {
        var data = await _reportRepository.GetCurrentUnitsAsync();
        return ExportReport(type, ReportName.CurrentUnits.ToString(), data);
    }
    [HttpGet("unit/available/export/{type}")]
    public async Task<IActionResult> ExportAvailableUnitsAsync(string type)
    {
        var data = await _reportRepository.GetAvailableUnitsAsync();
        return ExportReport(type, ReportName.AvailableUnits.ToString(), data);
    }
    #endregion

    #region Helpers :
    private IActionResult ExportReport(string type, string reportName, object data, params ReportParameter[] reportParameters)
    {
        try
        {
            type = type.ToUpper();
            string mimeType = type == "PDF" ? "pdf" : "xls";
            Stream reportDefinition;
            var path = Path.Combine(_env.WebRootPath, "Reports", $"{reportName}.rdlc");
            using var fs = new FileStream(path, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("DataSet", data));
            if (reportParameters.Any())
                report.SetParameters(reportParameters);
            byte[] content = report.Render(type);
            fs.Dispose();

            return File(content, $"application/{mimeType}", $"{reportName}{DateTime.Now:yyyy-MM-dd_hh_mm_ss}.{mimeType}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }
    #endregion
}
