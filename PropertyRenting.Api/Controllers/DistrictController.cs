using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Contexts;
using PropertyRenting.Api.Models.Entities;
using PropertyRenting.Api.ViewModels;

namespace PropertyRenting.Api.Controllers;

public class DistrictController : BaseController
{
    public DistrictController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Districts.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<DistrictDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.Districts.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<DistrictDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();

        int count = (await Context.Districts.AsNoTracking().CountAsync());

        var result = new Pagination<DistrictDTO>
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
        var city = await Context.Districts.AsNoTracking()
            .ProjectTo<DistrictDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (city == null) return NotFound();
        return Ok(city);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(DistrictDTO district)
    {
        if (district == null) return BadRequest();
        district.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<DistrictEntity>(district);
        await Context.Districts.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{district.Id}", district);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, DistrictDTO district)
    {
        if (district == null) return BadRequest();
        var currentDistrict = (await Context.Districts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentDistrict == null) return NotFound();
        currentDistrict = Mapper.Map(district, currentDistrict);

        Context.Districts.Update(currentDistrict);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentDistrict = (await Context.Districts.FirstOrDefaultAsync(x => x.Id == id));
        if (currentDistrict == null) return NotFound();

        Context.Districts.Remove(currentDistrict);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
}
