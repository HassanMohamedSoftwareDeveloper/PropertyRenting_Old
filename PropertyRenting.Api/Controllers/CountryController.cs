using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Contexts;
using PropertyRenting.Api.Models.Entities;
using PropertyRenting.Api.ViewModels;

namespace PropertyRenting.Api.Controllers;

public class CountryController : BaseController
{
    public CountryController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Countries.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<CountryDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();

        return Ok(data);
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.Countries.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
          .ProjectTo<CountryDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();

        int count = (await Context.Countries.AsNoTracking().CountAsync());

        var result = new Pagination<CountryDTO>
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
        var country = await Context.Countries.AsNoTracking()
            .ProjectTo<CountryDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (country == null) return NotFound();
        return Ok(country);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CountryDTO country)
    {
        if (country == null) return BadRequest();
        country.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<CountryEntity>(country);
        await Context.Countries.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{country.Id}", country);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, CountryDTO country)
    {
        if (country == null) return BadRequest();
        var currentCountry = (await Context.Countries.FirstOrDefaultAsync(x => x.Id == id));
        if (currentCountry == null) return NotFound();

        currentCountry = Mapper.Map(country, currentCountry);

        Context.Countries.Update(currentCountry);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentCountry = (await Context.Countries.FirstOrDefaultAsync(x => x.Id == id));
        if (currentCountry == null) return NotFound();

        Context.Countries.Remove(currentCountry);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
}
