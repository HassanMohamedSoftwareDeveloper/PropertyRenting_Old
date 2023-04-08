using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyRenting.Api.DTOs;
using PropertyRenting.Api.Models.Contexts;
using PropertyRenting.Api.Models.Entities;
using PropertyRenting.Api.ViewModels;

namespace PropertyRenting.Api.Controllers
{
    public class ContributerController : BaseController
    {
        public ContributerController(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await Context.Contributers.AsNoTracking().OrderBy(x => x.CreatedOnUtc)
                .ProjectTo<ContributerDTO>(Mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(data);
        }
        [HttpGet("list/byPage/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            int skipped = (pageNumber - 1) * pageSize;

            var data = await Context.Contributers.AsNoTracking().OrderBy(x => x.CreatedOnUtc).ProjectTo<ContributerDTO>(Mapper.ConfigurationProvider)
              .Skip(skipped).Take(pageSize)
              .ToListAsync();
            int count = (await Context.Contributers.AsNoTracking().CountAsync());
            var result = new Pagination<ContributerDTO>
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
            var owner = await Context.Contributers.AsNoTracking()
                .ProjectTo<ContributerDTO>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (owner == null) return NotFound();
            return Ok(owner);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(ContributerDTO contributer)
        {
            if (contributer == null) return BadRequest();
            contributer.Id = Guid.NewGuid();
            var mappedEntity = Mapper.Map<ContributerEntity>(contributer);
            await Context.Contributers.AddAsync(mappedEntity);

            bool saved = (await Context.SaveChangesAsync()) > 0;
            if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

            return Created($"~/byId/{contributer.Id}", contributer);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, ContributerDTO contributer)
        {
            if (contributer == null) return BadRequest();
            var currentContributer = (await Context.Contributers.FirstOrDefaultAsync(x => x.Id == id));
            if (currentContributer == null) return NotFound();
            currentContributer = Mapper.Map(contributer, currentContributer);

            Context.Contributers.Update(currentContributer);

            bool saved = (await Context.SaveChangesAsync()) > 0;
            if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var currentContributer = (await Context.Contributers.FirstOrDefaultAsync(x => x.Id == id));
            if (currentContributer == null) return NotFound();

            Context.Contributers.Remove(currentContributer);

            bool saved = (await Context.SaveChangesAsync()) > 0;
            if (saved is false) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok();
        }
    }
}
