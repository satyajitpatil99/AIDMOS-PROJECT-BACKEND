using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AIDMOSBackend.Context;
using AIDMOSBackend.Models;

namespace AIDMOSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly ProjectDBContext _context;

        public AreasController(ProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Areas
        [HttpGet("{villageid}")]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas(decimal villageid)
        {
            if (_context.Areas == null)
            {
                return NotFound();
            }
            if (villageid == 0)
            {
                return await _context.Areas.ToListAsync();
            }
            else
            {
                return await _context.Areas.Where(a => a.Villageid == villageid).ToListAsync();
            }
        }

        // GET: api/Areas/5
        [HttpGet("{villageid}/{id}")]
        public async Task<ActionResult<Area>> GetArea(decimal villageid, decimal id)
        {
            if (_context.Areas == null)
            {
                return NotFound();
            }
            var area = await _context.Areas.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }

        // PUT: api/Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(decimal id, Area area)
        {
            if (id != area.Id)
            {
                return BadRequest();
            }

            _context.Entry(area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(area);
        }

        // POST: api/Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(Area area)
        {
            if (_context.Areas == null)
            {
                return Problem("Entity set 'ProjectDBContext.Areas'  is null.");
            }
            _context.Areas.Add(area);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AreaExists(area.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(area);
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(decimal id)
        {
            if (_context.Areas == null)
            {
                return NotFound();
            }
            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }

            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();

            return Ok(area);
        }

        private bool AreaExists(decimal id)
        {
            return (_context.Areas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
