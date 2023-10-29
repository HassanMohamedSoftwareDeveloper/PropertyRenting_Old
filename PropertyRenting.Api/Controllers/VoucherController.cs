using Microsoft.Reporting.NETCore;
using PropertyRenting.Api.DTOs.Reports;

namespace PropertyRenting.Api.Controllers;

public class VoucherController : BaseController
{
    private readonly IWebHostEnvironment _env;

    public VoucherController(AppDbContext context, IMapper mapper, IWebHostEnvironment env) : base(context, mapper)
    {
        _env = env;
    }

    #region Receipt :
    [HttpGet("receipts/list")]
    public async Task<IActionResult> GetAlllReceiptVouchersAsync()
    {
        var data = await Context.ReceiptVouchers.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<ReceiptVoucherDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("receipts/list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAlllReceiptVouchersAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;
        var data = await Context.ReceiptVouchers.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<ReceiptVoucherDTO>(Mapper.ConfigurationProvider)
         .Skip(skipped).Take(pageSize)
         .ToListAsync();
        int count = (await Context.ReceiptVouchers.AsNoTracking().CountAsync());
        var result = new Pagination<ReceiptVoucherDTO>
        {
            Data = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = count
        };

        return Ok(result);
    }
    [HttpGet("receipts/byId/{id}")]
    public async Task<IActionResult> GetReceiptVoucherByIdAsync(Guid id)
    {
        var entity = await Context.ReceiptVouchers.AsNoTracking()
            .ProjectTo<ReceiptVoucherDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost("receipts/create")]
    public async Task<IActionResult> CreateReceiptVoucherAsync(ReceiptVoucherDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        entityDTO.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<ReceiptVoucherEntity>(entityDTO);
        mappedEntity.StateId = (int)VoucherState.Open;

        await Context.ReceiptVouchers.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/receipts/byId/{entityDTO.Id}", Mapper.Map<ReceiptVoucherDTO>(mappedEntity));
    }
    [HttpPut("receipts/update/{id}")]
    public async Task<IActionResult> UpdateReceiptVoucherAsync(Guid id, ReceiptVoucherDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        var currentEntity = (await Context.ReceiptVouchers.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        currentEntity = Mapper.Map(entityDTO, currentEntity);
        currentEntity.SanadDetails.Clear();
        Context.ReceiptVouchers.Update(currentEntity);

        var receiptVoucherDetails = Mapper.Map<List<ReceiptVoucherDetailsEntity>>(entityDTO.SanadDetails);
        receiptVoucherDetails.ForEach(contributer =>
        {
            contributer.Id = Guid.NewGuid();
            contributer.ReceiptVoucherId = currentEntity.Id;
        });

        Context.ReceiptVoucherDetails.AddRange(receiptVoucherDetails);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }

    [HttpPost("receipts/post/{id}")]
    public async Task<IActionResult> PostReceiptVoucherAsync(Guid id)
    {
        var currentEntity = (await Context.ReceiptVouchers.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        var accountSetup = await Context.AccountSetups.FirstOrDefaultAsync();
        if (accountSetup is null) return BadRequest("NoSetupExist");

        currentEntity.StateId = (int)VoucherState.Posted;

        var voucher = new VoucherEntity
        {
            Id = Guid.NewGuid(),
            StateId = (int)VoucherState.Posted,
            Description = currentEntity.Description,
            ReferenceId = currentEntity.Id,
            ReferenceType = RefType.Receipt.ToString(),
            VoucherDate = currentEntity.SanadDate,
            ReferenceAutoNumber = currentEntity.AutoNumber,
            ReferenceManualNumber = currentEntity.SanadNumber
        };

        var voucherDetails = new List<VoucherDetailsEntity>
        {
            new VoucherDetailsEntity
            {
                Id = Guid.NewGuid(),
                VoucherId = voucher.Id,
                AccountId = currentEntity.CashBank.AccountId,
                DebitAmount = currentEntity.Amount,
                CashBankId=currentEntity.CashBankId
            }
        };
        if (currentEntity.SanadTypeId == (int)SanadType.Contributer)
        {
            voucherDetails.Add(new VoucherDetailsEntity
            {
                Id = Guid.NewGuid(),
                VoucherId = voucher.Id,
                AccountId = accountSetup.ContributerAccountId,
                CreditAmount = currentEntity.Amount,
                BuildingId = null,
                UnitId = null,
                RenterId = null,
                OwnerId = null,
                ContributerId = currentEntity.ContributerId,
                ExpenseId = null
            });
        }
        else
        {
            foreach (var item in currentEntity.SanadDetails)
            {
                voucherDetails.Add(new VoucherDetailsEntity
                {
                    Id = Guid.NewGuid(),
                    VoucherId = voucher.Id,
                    AccountId =
                        currentEntity.SanadTypeId == (int)SanadType.General ? item.Expense.AccountId :
                        currentEntity.SanadTypeId == (int)SanadType.Owner ? accountSetup.OwnerAccountId :
                        accountSetup.RenterAccountId,
                    CreditAmount = item.Amount,
                    BuildingId = item.BuildingId,
                    UnitId = item.UnitId,
                    RenterId = currentEntity.RenterId,
                    OwnerId = currentEntity.OwnerId,
                    ContributerId = currentEntity.ContributerId,
                    ExpenseId = item.ExpenseId,
                    InstallmentId = item.Id
                });

                if (currentEntity.SanadTypeId == (int)SanadType.Owner)
                {
                    voucherDetails.Add(new VoucherDetailsEntity
                    {
                        Id = Guid.NewGuid(),
                        VoucherId = voucher.Id,
                        AccountId = accountSetup.OwnerAccountId,
                        DebitAmount = item.Amount,
                        OwnerId = currentEntity.OwnerId,
                        BuildingId = item.BuildingId,
                        InstallmentId = item.Id
                    });
                    voucherDetails.Add(new VoucherDetailsEntity
                    {
                        Id = Guid.NewGuid(),
                        VoucherId = voucher.Id,
                        AccountId = accountSetup.ExpenseAccountId,
                        CreditAmount = item.Amount,
                        BuildingId = item.BuildingId,
                        InstallmentId = item.Id
                    });
                }

                if (currentEntity.SanadTypeId == (int)SanadType.Renter && item.InstallmentId != null)
                {
                    var installmentRecord = await Context.RenterFinancialTransactions.FirstAsync(x => x.Id == item.InstallmentId);
                    voucherDetails.Add(new VoucherDetailsEntity()
                    {
                        AccountId = accountSetup.AccruedRevenueAccountId,
                        DebitAmount = item.Amount,
                        UnitId = item.UnitId,
                        BuildingId = item.BuildingId,
                        InstallmentId = item.Id
                    });
                    voucherDetails.Add(new VoucherDetailsEntity()
                    {
                        AccountId = installmentRecord.ContractAdditionId == null ? accountSetup.RevenueAccountId : installmentRecord.ContractAddition.AccountId.Value,
                        CreditAmount = item.Amount,
                        UnitId = item.UnitId,
                        BuildingId = item.BuildingId,
                        InstallmentId = item.Id
                    });
                }

                if (item.InstallmentId == null) continue;
                var trans = await Context.RenterFinancialTransactions.FirstOrDefaultAsync(x => x.Id == item.InstallmentId);
                trans.PaidAmount += item.Amount;
                trans.Balance -= item.Amount;
                trans.IsPaid = trans.Balance <= 0;

                Context.RenterFinancialTransactions.Update(trans);
                var contract = trans.RenterContract;
                if (contract.RenterFinancialTransactions.All(x => x.IsPaid))
                {
                    contract.ContractState = (int)ContractState.Closed;
                    Context.RenterContracts.Update(contract);

                    contract.Unit.RentStatus = false;
                    Context.Units.Update(contract.Unit);
                }
            }
        }

        voucher.VoucherDetails = voucherDetails;

        Context.ReceiptVouchers.Update(currentEntity);
        await Context.Vouchers.AddAsync(voucher);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("receipts/delete/{id}")]
    public async Task<IActionResult> DeleteReceiptVoucherAsync(Guid id)
    {
        var currentEntity = (await Context.ReceiptVouchers.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        if (currentEntity.StateId == (int)VoucherState.Posted) return BadRequest();

        Context.ReceiptVouchers.Remove(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpGet("receipts/print/{id}")]
    public async Task<IActionResult> PrintReceiptAsync(Guid id)
    {
        try
        {

            var data = await Context.ReceiptVouchers.Where(x => x.Id == id)
                .Select(x => new SanadDTO
                {
                    SanadNumber = $"{x.AutoNumber}-{x.SanadNumber}",
                    Amount = x.Amount,
                    Description = x.Description,
                    SanadDate = x.SanadDate.ToString("dd/MM/yyyy"),
                    Name = x.SanadTypeId == (int)SanadType.Renter ? x.Renter.NameAR :
                    x.SanadTypeId == (int)SanadType.Owner ? x.Owner.NameAR :
                     x.SanadTypeId == (int)SanadType.Contributer ? x.Contributer.NameAR :
                     "",
                    Bank = x.CashBank.Name,

                }).ToListAsync().ConfigureAwait(false);
            Stream reportDefinition;
            var path = Path.Combine(_env.WebRootPath, "Reports", "Receipt.rdlc");
            using var fs = new FileStream(path, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("DataSet", data));
            report.SetParameters(new[] { new ReportParameter("ID", data?[0]?.SanadNumber ?? "") });
            byte[] pdf = report.Render("PDF");
            fs.Dispose();

            return File(pdf, "application/pdf", $"Receipt_{DateTime.Now:yyyy-MM-dd_hh_mm_ss}.pdf");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }
    #endregion

    #region Exchange :
    [HttpGet("exchanges/list")]
    public async Task<IActionResult> GetAlllExchangeVouchersAsync()
    {
        var data = await Context.ExchangeVouchers.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<ExchangeVoucherDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("exchanges/list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAlllExchangeVouchersAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;
        var data = await Context.ExchangeVouchers.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<ExchangeVoucherDTO>(Mapper.ConfigurationProvider)
         .Skip(skipped).Take(pageSize)
         .ToListAsync();
        int count = (await Context.ExchangeVouchers.AsNoTracking().CountAsync());
        var result = new Pagination<ExchangeVoucherDTO>
        {
            Data = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = count
        };

        return Ok(result);
    }
    [HttpGet("exchanges/byId/{id}")]
    public async Task<IActionResult> GetExchangeVoucherByIdAsync(Guid id)
    {
        var entity = await Context.ExchangeVouchers.AsNoTracking()
            .ProjectTo<ExchangeVoucherDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost("exchanges/create")]
    public async Task<IActionResult> CreateExchangeVoucherAsync(ExchangeVoucherDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        entityDTO.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<ExchangeVoucherEntity>(entityDTO);
        mappedEntity.StateId = (int)VoucherState.Open;

        await Context.ExchangeVouchers.AddAsync(mappedEntity);


        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/exchanges/byId/{entityDTO.Id}", Mapper.Map<ExchangeVoucherDTO>(mappedEntity));
    }
    [HttpPut("exchanges/update/{id}")]
    public async Task<IActionResult> UpdateExchangeVoucherAsync(Guid id, ExchangeVoucherDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        var currentEntity = (await Context.ExchangeVouchers.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        currentEntity = Mapper.Map(entityDTO, currentEntity);
        currentEntity.SanadDetails.Clear();
        Context.ExchangeVouchers.Update(currentEntity);

        var exchangeVoucherDetails = Mapper.Map<List<ExchangeVoucherDetailsEntity>>(entityDTO.SanadDetails);
        exchangeVoucherDetails.ForEach(contributer =>
        {
            contributer.Id = Guid.NewGuid();
            contributer.ExchangeVoucherId = currentEntity.Id;
        });

        Context.ExchangeVoucherDetails.AddRange(exchangeVoucherDetails);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }

    [HttpPost("exchanges/post/{id}")]
    public async Task<IActionResult> PostExchangeVoucherAsync(Guid id)
    {
        var currentEntity = (await Context.ExchangeVouchers.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        var accountSetup = await Context.AccountSetups.FirstOrDefaultAsync();
        if (accountSetup is null) return BadRequest("NoSetupExist");

        currentEntity.StateId = (int)VoucherState.Posted;

        var voucher = new VoucherEntity
        {
            Id = Guid.NewGuid(),
            StateId = (int)VoucherState.Posted,
            Description = currentEntity.Description,
            ReferenceId = currentEntity.Id,
            ReferenceType = RefType.Exchange.ToString(),
            VoucherDate = currentEntity.SanadDate,
            ReferenceAutoNumber = currentEntity.AutoNumber,
            ReferenceManualNumber = currentEntity.SanadNumber
        };

        var voucherDetails = new List<VoucherDetailsEntity>
        {
            new VoucherDetailsEntity
            {
                Id = Guid.NewGuid(),
                VoucherId = voucher.Id,
                AccountId = currentEntity.CashBank.AccountId,
                CreditAmount = currentEntity.Amount,
                CashBankId=currentEntity.CashBankId
            }
        };
        if (currentEntity.SanadTypeId == (int)SanadType.Contributer)
        {
            voucherDetails.Add(new VoucherDetailsEntity
            {
                Id = Guid.NewGuid(),
                VoucherId = voucher.Id,
                AccountId = accountSetup.ContributerAccountId,
                DebitAmount = currentEntity.Amount,
                BuildingId = null,
                UnitId = null,
                OwnerId = null,
                ContributerId = currentEntity.ContributerId,
                ExpenseId = null
            });
        }
        else
        {
            foreach (var item in currentEntity.SanadDetails)
            {
                voucherDetails.Add(new VoucherDetailsEntity
                {
                    Id = Guid.NewGuid(),
                    VoucherId = voucher.Id,
                    AccountId =
                        currentEntity.SanadTypeId == (int)SanadType.General ? item.Expense.AccountId :
                        currentEntity.SanadTypeId == (int)SanadType.Owner ? accountSetup.OwnerAccountId :
                        currentEntity.SanadTypeId == (int)SanadType.Contributer ? accountSetup.ContributerAccountId :
                        accountSetup.RenterAccountId,
                    DebitAmount = item.Amount,
                    BuildingId = item.BuildingId,
                    UnitId = item.UnitId,
                    OwnerId = currentEntity.OwnerId,
                    ContributerId = currentEntity.ContributerId,
                    ExpenseId = item.ExpenseId,
                    InstallmentId = item.Id
                });

                if (currentEntity.SanadTypeId == (int)SanadType.Renter)
                {
                    voucherDetails.Add(new VoucherDetailsEntity
                    {
                        Id = Guid.NewGuid(),
                        VoucherId = voucher.Id,
                        AccountId = item.AdditionId.Value == Guid.Parse(SharedId.Rent_Type_Id) ? accountSetup.RevenueAccountId : item.Addition.AccountId.Value,
                        DebitAmount = item.Amount,
                        BuildingId = item.BuildingId,
                        UnitId = item.UnitId,
                        InstallmentId = item.Id

                    });
                    voucherDetails.Add(new VoucherDetailsEntity
                    {
                        Id = Guid.NewGuid(),
                        VoucherId = voucher.Id,
                        AccountId = accountSetup.RenterAccountId,
                        CreditAmount = item.Amount,
                        RenterId = currentEntity.RenterId,
                        BuildingId = item.BuildingId,
                        UnitId = item.UnitId,
                        InstallmentId = item.Id
                    });
                }

                if (currentEntity.SanadTypeId == (int)SanadType.Owner && item.InstallmentId != null)
                {
                    voucherDetails.Add(new VoucherDetailsEntity()
                    {
                        AccountId = accountSetup.ExpenseAccountId,
                        DebitAmount = item.Amount,
                        BuildingId = item.BuildingId,
                        InstallmentId = item.Id
                    });
                    voucherDetails.Add(new VoucherDetailsEntity()
                    {
                        AccountId = accountSetup.AccruedExpenseAccountId,
                        CreditAmount = item.Amount,
                        BuildingId = item.BuildingId,
                        InstallmentId = item.Id
                    });
                }

                if (item.InstallmentId == null) continue;
                var trans = await Context.OwnerFinancialTransactions.FirstOrDefaultAsync(x => x.Id == item.InstallmentId);
                trans.PaidAmount += item.Amount;
                trans.Balance -= item.Amount;
                trans.IsPaid = trans.Balance <= 0;

                Context.OwnerFinancialTransactions.Update(trans);
                var contract = trans.OwnerContract;
                if (contract.OwnerFinancialTransactions.All(x => x.IsPaid))
                {
                    contract.ContractState = (int)ContractState.Closed;
                    Context.OwnerContracts.Update(contract);
                }
            }
        }

        voucher.VoucherDetails = voucherDetails;

        Context.ExchangeVouchers.Update(currentEntity);

        await Context.Vouchers.AddAsync(voucher);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("exchanges/delete/{id}")]
    public async Task<IActionResult> DeleteExchangeVoucherAsync(Guid id)
    {
        var currentEntity = (await Context.ExchangeVouchers.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        if (currentEntity.StateId == (int)VoucherState.Posted) return BadRequest();

        Context.ExchangeVouchers.Remove(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpGet("exchanges/print/{id}")]
    public async Task<IActionResult> PrintExchangeAsync(Guid id)
    {
        try
        {

            var data = await Context.ExchangeVouchers.Where(x => x.Id == id)
                .Select(x => new SanadDTO
                {
                    SanadNumber = $"{x.AutoNumber}-{x.SanadNumber}",
                    Amount = x.Amount,
                    Description = x.Description,
                    SanadDate = x.SanadDate.ToString("dd/MM/yyyy"),
                    Name = x.SanadTypeId == (int)SanadType.Renter ? x.Renter.NameAR :
                    x.SanadTypeId == (int)SanadType.Owner ? x.Owner.NameAR :
                     x.SanadTypeId == (int)SanadType.Contributer ? x.Contributer.NameAR :
                     "",
                    Bank = x.CashBank.Name
                }).ToListAsync().ConfigureAwait(false);
            Stream reportDefinition;
            var path = Path.Combine(_env.WebRootPath, "Reports", "Exchange.rdlc");
            using var fs = new FileStream(path, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("DataSet", data));
            report.SetParameters(new[] { new ReportParameter("ID", data?[0]?.SanadNumber ?? "") });
            byte[] pdf = report.Render("PDF");
            fs.Dispose();

            return File(pdf, "application/pdf", $"Exchange_{DateTime.Now:yyyy-MM-dd_hh_mm_ss}.pdf");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }
    #endregion


    #region Voucher :
    [HttpGet("vouchers/list")]
    public async Task<IActionResult> GetAlllVouchersAsync()
    {
        var data = await Context.Vouchers.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<VoucherDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("vouchers/list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAlllVouchersAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;
        var data = await Context.Vouchers.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<VoucherDTO>(Mapper.ConfigurationProvider)
         .Skip(skipped).Take(pageSize)
         .ToListAsync();
        int count = (await Context.Vouchers.AsNoTracking().CountAsync());
        var result = new Pagination<VoucherDTO>
        {
            Data = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = count
        };

        return Ok(result);
    }
    [HttpGet("vouchers/byId/{id}")]
    public async Task<IActionResult> GetVoucherByIdAsync(Guid id)
    {
        var entity = await Context.Vouchers.AsNoTracking()
            .ProjectTo<VoucherDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost("vouchers/create")]
    public async Task<IActionResult> CreateVoucherAsync(VoucherDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        entityDTO.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<VoucherEntity>(entityDTO);
        mappedEntity.StateId = (int)VoucherState.Open;


        await Context.Vouchers.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/vouchers/byId/{entityDTO.Id}", Mapper.Map<VoucherDTO>(mappedEntity));
    }
    [HttpPut("vouchers/update/{id}")]
    public async Task<IActionResult> UpdateVoucherAsync(Guid id, VoucherDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        var currentEntity = (await Context.Vouchers.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        currentEntity = Mapper.Map(entityDTO, currentEntity);
        currentEntity.VoucherDetails.Clear();
        Context.Vouchers.Update(currentEntity);

        var voucherDetails = Mapper.Map<List<VoucherDetailsEntity>>(entityDTO.VoucherDetails);
        voucherDetails.ForEach(detail =>
        {
            detail.Id = Guid.NewGuid();
            detail.VoucherId = currentEntity.Id;
        });

        Context.VoucherDetails.AddRange(voucherDetails);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }

    [HttpPost("vouchers/post/{id}")]
    public async Task<IActionResult> PostVoucherAsync(Guid id)
    {
        var currentEntity = (await Context.Vouchers.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        currentEntity.StateId = (int)VoucherState.Posted;

        Context.Vouchers.Update(currentEntity);
        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("vouchers/delete/{id}")]
    public async Task<IActionResult> DeleteVoucherAsync(Guid id)
    {
        var currentEntity = (await Context.Vouchers.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        if (currentEntity.StateId == (int)VoucherState.Posted) return BadRequest();

        Context.Vouchers.Remove(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    #endregion
}
