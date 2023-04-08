using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Enums;
using PropertyRenting.Api.Models.Contexts;
using PropertyRenting.Api.Models.Entities;
using PropertyRenting.Api.ViewModels;

namespace PropertyRenting.Api.Controllers;

public class UnitController : BaseController
{
    public UnitController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Units.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<UnitDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("list/available/{buidlingId}")]
    public async Task<IActionResult> GetAvailableAsync(Guid buidlingId)
    {
        var data = await Context.Units.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .Where(x => x.BuildingId == buidlingId
            && (x.RenterContracts.Any() == false
            || x.RenterContracts.All(c => c.ContractState != (int)ContractState.Activated
            || (c.ContractState == (int)ContractState.Activated && c.ContractEndDate.Date < DateTime.UtcNow.Date))))
            .ProjectTo<UnitDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.Units.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
          .ProjectTo<UnitDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();
        int count = (await Context.Units.AsNoTracking().CountAsync());
        var result = new Pagination<UnitDTO>
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
        var unit = await Context.Units.AsNoTracking()
            .ProjectTo<UnitDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (unit == null) return NotFound();
        return Ok(unit);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(UnitDTO unit)
    {
        if (unit == null) return BadRequest();
        unit.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<UnitEntity>(unit);
        await Context.Units.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{unit.Id}", unit);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UnitDTO unit)
    {
        if (unit == null) return BadRequest();
        var currentUnit = (await Context.Units.FirstOrDefaultAsync(x => x.Id == id));
        if (currentUnit == null) return NotFound();
        currentUnit = Mapper.Map(unit, currentUnit);

        Context.Units.Update(currentUnit);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentUnit = (await Context.Units.FirstOrDefaultAsync(x => x.Id == id));
        if (currentUnit == null) return NotFound();

        Context.Units.Remove(currentUnit);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
}

