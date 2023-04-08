using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Contexts;
using PropertyRenting.Api.Models.Entities;
using PropertyRenting.Api.ViewModels;

namespace PropertyRenting.Api.Controllers;

public class ExpenseController : BaseController
{
    public ExpenseController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Expenses.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<ExpenseDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.Expenses.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<ExpenseDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();

        int count = (await Context.Expenses.AsNoTracking().CountAsync());

        var result = new Pagination<ExpenseDTO>
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
        var entity = await Context.Expenses.AsNoTracking()
            .ProjectTo<ExpenseDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(ExpenseDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        entityDTO.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<ExpenseEntity>(entityDTO);
        await Context.Expenses.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{entityDTO.Id}", entityDTO);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, ExpenseDTO entityDTO)
    {
        if (entityDTO == null) return BadRequest();
        var currentEntity = (await Context.Expenses.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();
        currentEntity = Mapper.Map(entityDTO, currentEntity);

        Context.Expenses.Update(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentEntity = (await Context.Expenses.FirstOrDefaultAsync(x => x.Id == id));
        if (currentEntity == null) return NotFound();

        Context.Expenses.Remove(currentEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
}