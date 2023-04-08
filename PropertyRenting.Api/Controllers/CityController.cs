using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Contexts;
using PropertyRenting.Api.Models.Entities;
using PropertyRenting.Api.ViewModels;

namespace PropertyRenting.Api.Controllers;

public class CityController : BaseController
{
    public CityController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Cities.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<CityDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.Cities.AsNoTracking()
            .OrderBy(x => x.CreatedOnUtc)
          .ProjectTo<CityDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();
        int count = (await Context.Cities.AsNoTracking().CountAsync());
        var result = new Pagination<CityDTO>
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
        var city = await Context.Cities.AsNoTracking()
            .ProjectTo<CityDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (city == null) return NotFound();
        return Ok(city);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CityDTO city)
    {
        if (city == null) return BadRequest();
        city.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<CityEntity>(city);
        await Context.Cities.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{city.Id}", city);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, CityDTO city)
    {
        if (city == null) return BadRequest();
        var currentCity = (await Context.Cities.FirstOrDefaultAsync(x => x.Id == id));
        if (currentCity == null) return NotFound();
        currentCity = Mapper.Map(city, currentCity);

        Context.Cities.Update(currentCity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentCity = (await Context.Cities.FirstOrDefaultAsync(x => x.Id == id));
        if (currentCity == null) return NotFound();

        Context.Cities.Remove(currentCity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
}
