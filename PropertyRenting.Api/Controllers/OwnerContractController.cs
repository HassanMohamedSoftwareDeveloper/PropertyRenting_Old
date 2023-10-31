using PropertyRenting.Api.Factory;

namespace PropertyRenting.Api.Controllers;

public class OwnerContractController : BaseController
{
    #region CTORS :
    public OwnerContractController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    #endregion


    #region Actions :
    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.OwnerContracts.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<OwnerContractDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.OwnerContracts.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<OwnerContractDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();

        int count = (await Context.OwnerContracts.AsNoTracking().CountAsync());

        var result = new Pagination<OwnerContractDTO>
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
        var entity = await Context.OwnerContracts.AsNoTracking()
            .ProjectTo<OwnerContractDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        entity.OwnerFinancialTransactions = entity.OwnerFinancialTransactions.OrderBy(x => x.DueDate).ToList();
        return Ok(entity);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(OwnerContractDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        entityDTO.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<OwnerContractEntity>(entityDTO);
        await Context.OwnerContracts.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{entityDTO.Id}", entityDTO);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, OwnerContractDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        var currentEntity = (await Context.OwnerContracts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        if (currentEntity.ContractState != (int)ContractState.Open)
            return BadRequest();
        currentEntity = Mapper.Map(entityDTO, currentEntity);

        Context.OwnerContracts.Update(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentEntity = (await Context.OwnerContracts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        if (currentEntity.ContractState != (int)ContractState.Open)
            return BadRequest();
        Context.OwnerContracts.Remove(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }

    [HttpPost("activate/{id}")]
    public async Task<IActionResult> ActivateAsync(Guid id)
    {
        var currentEntity = (await Context.OwnerContracts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        if (currentEntity.ContractState != (int)ContractState.Open) return BadRequest();

        var accountSetup = await Context.AccountSetups.FirstOrDefaultAsync();
        if (accountSetup is null) return BadRequest("NoSetupExist");

        currentEntity.ContractState = (int)ContractState.Activated;
        IInstallment installment = InstallmentFactory.Create((PaymentMethod)currentEntity.PaymentMethod);
        var installments = installment.CreateInstallments(currentEntity.Id, currentEntity.ContractAmount, currentEntity.ContractStartDate, currentEntity.ContractEndDate);

        Context.OwnerContracts.Update(currentEntity);

        var mappedInstallments = Mapper.Map<List<OwnerFinancialTransactionEntity>>(installments);
        Context.OwnerFinancialTransactions.AddRange(mappedInstallments);

        foreach (var item in currentEntity.OwnerFinancialTransactions.OrderBy(x => x.DueDate).ToList())
        {
            VoucherEntity voucher = new()
            {
                VoucherDate = item.DueDate,
                ReferenceId = currentEntity.Id,
                ReferenceAutoNumber = currentEntity.AutoNumber,
                ReferenceManualNumber = currentEntity.ContractNumber,
                StateId = (int)VoucherState.Posted,
                ReferenceType = RefType.OwnerContract.ToString(),
                Description = $"إثبات قيمة إيجار مستحق فى تاريخ ({item.DueDate:yyyy-MM-dd}) بقيمة ({item.Balance})",
                VoucherDetails = new List<VoucherDetailsEntity>()
                {
                    new VoucherDetailsEntity()
                    {
                        AccountId=accountSetup.AccruedExpenseAccountId,
                        DebitAmount=item.Balance,
                        BuildingId=currentEntity.BuildingId,
                        InstallmentId=item.Id
                    },
                    new VoucherDetailsEntity()
                    {
                        AccountId=accountSetup.OwnerAccountId,
                        OwnerId=currentEntity.OwnerId,
                        CreditAmount=item.Balance,
                        BuildingId=currentEntity.BuildingId,
                        InstallmentId=item.Id
                    }
                },
            };

            Context.Vouchers.Add(voucher);
        }

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);
        return Ok();
    }
    [HttpPost("cancel/{id}")]
    public async Task<IActionResult> CancelAsync(Guid id)
    {
        var currentEntity = (await Context.OwnerContracts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        if (currentEntity.ContractState != (int)ContractState.Activated) return BadRequest();

        if (currentEntity.OwnerFinancialTransactions.Any() == false) return BadRequest();

        var accountSetup = await Context.AccountSetups.FirstOrDefaultAsync();
        if (accountSetup is null) return BadRequest("NoSetupExist");

        currentEntity.ContractState = (int)ContractState.Canceled;
        foreach (var trans in currentEntity.OwnerFinancialTransactions)
        {
            if (trans.IsPaid) continue;
            trans.IsCancelled = true;

            VoucherEntity voucher = new()
            {
                VoucherDate = trans.DueDate,
                ReferenceId = currentEntity.Id,
                StateId = (int)VoucherState.Posted,
                ReferenceAutoNumber = currentEntity.AutoNumber,
                ReferenceManualNumber = currentEntity.ContractNumber,
                ReferenceType = RefType.OwnerContract.ToString(),
                Description = $"إلغاء قيمة الباقى من إيجار مستحق فى تاريخ ({trans.DueDate:yyyy-MM-dd}) بقيمة ({trans.Balance})",
                VoucherDetails = new List<VoucherDetailsEntity>()
                {
                    new VoucherDetailsEntity()
                    {
                        AccountId=accountSetup.OwnerAccountId,
                        OwnerId=currentEntity.OwnerId,
                        DebitAmount=trans.Balance,
                        BuildingId=currentEntity.BuildingId,
                        InstallmentId=trans.Id
                    },
                    new VoucherDetailsEntity()
                    {
                        AccountId=accountSetup.AccruedExpenseAccountId,
                        CreditAmount=trans.Balance,
                        BuildingId=currentEntity.BuildingId,
                        InstallmentId=trans.Id
                    }
                },
            };

            Context.Vouchers.Add(voucher);
        }

        Context.OwnerContracts.Update(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);
        return Ok();
    }

    [HttpGet("byId/{ownerId}/installments")]
    public async Task<IActionResult> GetDuedInstallments(Guid ownerId)
    {
        var installments = await Context.OwnerFinancialTransactions.AsNoTracking()
             .Where(x => x.OwnerContract.OwnerId == ownerId && x.IsPaid == false && x.IsCancelled == false)
             .OrderBy(x => x.OwnerContract.CreatedOnUtc)
             .ThenBy(x => x.OwnerContract.Id)
             .ThenBy(x => x.DueDate)
             .ProjectTo<ContractFinancialTransactionDTO>(Mapper.ConfigurationProvider).ToListAsync();

        return Ok(installments);
    }
    [HttpGet("installments-per-date")]
    public async Task<IActionResult> GetInstallmentsPerDate()
    {
        var data = await Context.OwnerFinancialTransactions
                     .AsNoTracking()
                     .Where(x => !x.IsPaid && !x.IsCancelled)
                     .OrderBy(x => x.DueDate.Date)
                     .GroupBy(x => x.DueDate.Date)
                     .Select(x => new InstallmentsPerDateDTO { DueDate = x.Key.ToString("yyyy-MM-dd"), Count = x.Count(), Total = x.Sum(c => c.Balance) })
                     .ToListAsync();

        return Ok(data);
    }
    #endregion
}
