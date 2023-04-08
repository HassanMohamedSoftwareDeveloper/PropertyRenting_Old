using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Contexts;
using PropertyRenting.Api.Models.Entities;
using PropertyRenting.Api.ViewModels;

namespace PropertyRenting.Api.Controllers;

public class NationalityController : BaseController
{

    public NationalityController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Nationalities.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<NationalityDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.Nationalities.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<NationalityDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();
        int count = (await Context.Nationalities.AsNoTracking().CountAsync());
        var result = new Pagination<NationalityDTO>
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
        var nationality = await Context.Nationalities.AsNoTracking()
            .ProjectTo<NationalityDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (nationality == null) return NotFound();
        return Ok(nationality);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(NationalityDTO nationality)
    {
        if (nationality == null) return BadRequest();
        nationality.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<NationalityEntity>(nationality);
        await Context.Nationalities.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{nationality.Id}", nationality);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, NationalityDTO nationality)
    {
        if (nationality == null) return BadRequest();
        var currentNationality = (await Context.Nationalities.FirstOrDefaultAsync(x => x.Id == id));
        if (currentNationality == null) return NotFound();
        currentNationality = Mapper.Map(nationality, currentNationality);

        Context.Nationalities.Update(currentNationality);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentNationality = (await Context.Nationalities.FirstOrDefaultAsync(x => x.Id == id));
        if (currentNationality == null) return NotFound();

        Context.Nationalities.Remove(currentNationality);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }

}
