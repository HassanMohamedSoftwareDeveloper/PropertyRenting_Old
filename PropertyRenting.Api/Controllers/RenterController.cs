using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Contexts;
using PropertyRenting.Api.Models.Entities;
using PropertyRenting.Api.ViewModels;

namespace PropertyRenting.Api.Controllers;

public class RenterController : BaseController
{

    public RenterController(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await Context.Renters.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
            .ProjectTo<RenterDTO>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(data);
    }
    [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        int skipped = (pageNumber - 1) * pageSize;

        var data = await Context.Renters.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
          .ProjectTo<RenterDTO>(Mapper.ConfigurationProvider)
          .Skip(skipped).Take(pageSize)
          .ToListAsync();
        int count = (await Context.Renters.AsNoTracking().CountAsync());
        var result = new Pagination<RenterDTO>
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
        var renter = await Context.Renters.AsNoTracking()
            .ProjectTo<RenterDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (renter == null) return NotFound();
        return Ok(renter);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(RenterDTO renter)
    {
        if (renter == null) return BadRequest();
        renter.Id = Guid.NewGuid();
        var mappedEntity = Mapper.Map<RenterEntity>(renter);
        await Context.Renters.AddAsync(mappedEntity);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Created($"~/byId/{renter.Id}", renter);
    }
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, RenterDTO renter)
    {
        if (renter == null) return BadRequest();
        var currentRenter = (await Context.Renters.FirstOrDefaultAsync(x => x.Id == id));
        if (currentRenter == null) return NotFound();

        currentRenter = Mapper.Map(renter, currentRenter);
        currentRenter.ContactPersons.Clear();
        Context.Renters.Update(currentRenter);

        var contactPersons = Mapper.Map<List<ContactPersonEntity>>(renter.ContactPersons);
        contactPersons.ForEach(contact =>
        {
            contact.Id = Guid.NewGuid();
            contact.RenterId = currentRenter.Id;
        });

        Context.ContactPersons.AddRange(contactPersons);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var currentRenter = (await Context.Renters.FirstOrDefaultAsync(x => x.Id == id));
        if (currentRenter == null) return NotFound();

        Context.Renters.Remove(currentRenter);

        bool saved = (await Context.SaveChangesAsync()) > 0;
        if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
}
