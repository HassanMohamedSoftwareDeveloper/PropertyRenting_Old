using PropertyRenting.Api.Factory;
using PropertyRenting.Api.Helpers;

namespace PropertyRenting.Api.Controllers;

public class RenterContractController : BaseController
{
    #region CTORS :
    public RenterContractController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    #endregion

    #region Actions :
    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.RenterContracts.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<RenterContractDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.RenterContracts.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<RenterContractDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();

        int count = (await Context.RenterContracts.AsNoTracking().CountAsync());

        var result = new Pagination<RenterContractDTO>
        {
            Data = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = count
        };

        return Ok(result);
    }
    [HttpGet("byId/{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var entity = await Context.RenterContracts.AsNoTracking()
            .ProjectTo<RenterContractDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        entity.RenterFinancialTransactions = entity.RenterFinancialTransactions.OrderBy(x => x.DueDate).ToList();
        return Ok(entity);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(RenterContractDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();

        entityDTO.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<RenterContractEntity>(entityDTO);
        foreach (var trans in mappedEntity.RenterFinancialTransactions)
        {
            trans.Balance = trans.Amount;
        }
        await Context.RenterContracts.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;

        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{entityDTO.Id}", entityDTO);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, RenterContractDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        var currentEntity = (await Context.RenterContracts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        currentEntity = Mapper.Map(entityDTO, currentEntity);


        currentEntity.RenterFinancialTransactions.Clear();
        Context.RenterContracts.Update(currentEntity);

        var renterFinancialTransactions = Mapper.Map<List<RenterFinancialTransactionEntity>>(entityDTO.RenterFinancialTransactions);
        renterFinancialTransactions.ForEach(trans =>
        {
            trans.Id = Guid.NewGuid();
            trans.ContractId = entityDTO.Id;
            trans.Balance = trans.Amount;
        });

        Context.RenterFinancialTransactions.AddRange(renterFinancialTransactions);


        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentEntity = (await Context.RenterContracts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        Context.RenterContracts.Remove(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpPost("activate/{id}")]
    public async Task<IActionResult> ActivateAsync(Guid id)
    {
        var currentEntity = (await Context.RenterContracts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        if (currentEntity.ContractState != (int)ContractState.Open) return BadRequest();

        var accountSetup = await Context.AccountSetups.FirstOrDefaultAsync();
        if (accountSetup is null) return BadRequest("NoSetupExist");

        currentEntity.ContractState = (int)ContractState.Activated;
        IInstallment installment = InstallmentFactory.Create((PaymentMethod)currentEntity.PaymentMethod);
        var installments = installment.CreateInstallments(currentEntity.Id, currentEntity.ContractAmount, currentEntity.ContractStartDate, currentEntity.ContractEndDate);

        Context.RenterContracts.Update(currentEntity);

        var mappedInstallments = Mapper.Map<List<RenterFinancialTransactionEntity>>(installments);
        Context.RenterFinancialTransactions.AddRange(mappedInstallments);



        foreach (var item in currentEntity.RenterFinancialTransactions.OrderBy(x => x.DueDate).ToList())
        {
            VoucherEntity voucher = new()
            {
                VoucherDate = item.DueDate,
                ReferenceId = currentEntity.Id,
                ReferenceAutoNumber = currentEntity.AutoNumber,
                ReferenceManualNumber = currentEntity.ContractNumber,
                StateId = (int)VoucherState.Posted,
                ReferenceType = RefType.RenterContract.ToString(),
                Description = $"إثبات قيمة إيجار مستحق فى تاريخ ({item.DueDate:yyyy-MM-dd}) بقيمة ({item.Balance})",
                VoucherDetails = new List<VoucherDetailsEntity>()
                {
                    new VoucherDetailsEntity()
                    {
                        AccountId=accountSetup.RenterAccountId,
                        RenterId=currentEntity.RenterId,
                        DebitAmount=item.Balance,
                        UnitId=currentEntity.UnitId,
                        BuildingId=currentEntity.Unit.BuildingId,
                        InstallmentId=item.Id
                    },
                    new VoucherDetailsEntity()
                    {
                        AccountId=accountSetup.AccruedRevenueAccountId,
                        CreditAmount=item.Balance,
                        UnitId=currentEntity.UnitId,
                        BuildingId=currentEntity.Unit.BuildingId,
                        InstallmentId=item.Id
                    }
                },
            };

            Context.Vouchers.Add(voucher);
        }
        var unit = currentEntity.Unit;
        unit.RentStatus = true;
        Context.Units.Update(unit);
        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        return Ok();
    }
    [HttpPost("cancel/{id}")]
    public async Task<IActionResult> CancelAsync(Guid id)
    {
        var currentEntity = (await Context.RenterContracts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        if (currentEntity.ContractState != (int)ContractState.Activated) return BadRequest();

        if (currentEntity.RenterFinancialTransactions.Any() == false) return BadRequest();

        var accountSetup = await Context.AccountSetups.FirstOrDefaultAsync();
        if (accountSetup is null) return BadRequest("NoSetupExist");

        currentEntity.ContractState = (int)ContractState.Canceled;

        foreach (var item in currentEntity.RenterFinancialTransactions)
        {
            if (item.IsPaid) continue;
            item.IsCancelled = true;

            VoucherEntity voucher = new()
            {
                VoucherDate = item.DueDate,
                ReferenceId = currentEntity.Id,
                ReferenceAutoNumber = currentEntity.AutoNumber,
                ReferenceManualNumber = currentEntity.ContractNumber,
                StateId = (int)VoucherState.Posted,
                ReferenceType = RefType.RenterContract.ToString(),
                Description = $"إلغاء قيمة الباقى من إيجار مستحق فى تاريخ ({item.DueDate:yyyy-MM-dd}) بقيمة ({item.Balance})",
                VoucherDetails = new List<VoucherDetailsEntity>()
                {
                    new VoucherDetailsEntity()
                    {
                        AccountId= accountSetup.RenterAccountId,
                        RenterId=currentEntity.RenterId,
                        CreditAmount=item.Balance,
                        UnitId=currentEntity.UnitId,
                        BuildingId=currentEntity.Unit.BuildingId,
                        InstallmentId=item.Id

                    },
                    new VoucherDetailsEntity()
                    {
                        AccountId=  accountSetup.AccruedRevenueAccountId,
                        DebitAmount=item.Balance,
                        UnitId=currentEntity.UnitId,
                        BuildingId=currentEntity.Unit.BuildingId,
                        InstallmentId=item.Id
                    }
                },
            };

            Context.Vouchers.Add(voucher);
        }

        Context.RenterContracts.Update(currentEntity);
        var unit = currentEntity.Unit;
        unit.RentStatus = false;
        Context.Units.Update(unit);
        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (!saved) return StatusCode(StatusCodes.Status500InternalServerError);
        return Ok();
    }
    [HttpGet("byId/{renterId}/installments")]
    public async Task<IActionResult> GetDuedInstallments(Guid renterId)
    {
        var installments = await Context.RenterFinancialTransactions.AsNoTracking()
             .Where(x => x.RenterContract.RenterId == renterId && x.IsPaid == false && x.IsCancelled == false)
             .OrderBy(x => x.RenterContract.CreatedOnUtc)
             .ThenBy(x => x.RenterContract.Id)
             .ThenBy(x => x.DueDate)
             .ProjectTo<ContractFinancialTransactionDTO>(Mapper.ConfigurationProvider).ToListAsync();

        return Ok(installments);
    }
    [HttpGet("count-by-state")]
    public async Task<IActionResult> GetCountByState()
    {
        var groupedData = await Context.RenterContracts
                     .AsNoTracking()
                     .GroupBy(x => x.ContractState)
                     .Select(x => new { ContractState = x.Key, Count = x.Count() })
                     .ToListAsync();
        var data = groupedData.Select(x => new ContractCountDTO
        {
            Description = Resources.ContractState.ResourceManager.GetResourceValue(((ContractState)x.ContractState).ToEnumString()),
            Count = x.Count
        });

        return Ok(data);
    }
    #endregion
}
