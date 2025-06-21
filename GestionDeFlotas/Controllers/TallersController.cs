using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionDeFlotas.Modelos;

namespace GestionDeFlotas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TallersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TallersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tallers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Taller>>> GetTalleres()
        {
            return await _context.Talleres.ToListAsync();
        }

        // GET: api/Tallers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Taller>> GetTaller(int id)
        {
            var taller = await _context.Talleres.FindAsync(id);

            if (taller == null)
            {
                return NotFound();
            }

            return taller;
        }

        // PUT: api/Tallers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaller(int id, Taller taller)
        {
            if (id != taller.id)
            {
                return BadRequest();
            }

            _context.Entry(taller).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TallerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tallers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Taller>> PostTaller(Taller taller)
        {
            _context.Talleres.Add(taller);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaller", new { id = taller.id }, taller);
        }

        // DELETE: api/Tallers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaller(int id)
        {
            var taller = await _context.Talleres.FindAsync(id);
            if (taller == null)
            {
                return NotFound();
            }

            _context.Talleres.Remove(taller);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("Filtrar")]
        public async Task<ActionResult<IEnumerable<Taller>>> GetMantenimientosConFiltros(int? tallerId, string? searchTerm, String? Ciudad)
        {
            var query = _context.Talleres.AsQueryable();

            // Filtro por el ID del Taller
            if (tallerId.HasValue)
            {
                query = query.Where(m => m.id == tallerId.Value);
            }

            // Filtro por término de búsqueda en el tipo de mantenimiento
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(m => m.Nombre.Contains(searchTerm));
            }

            // Filtro por pendiente (solo si ciudad tiene un valor)
            if(!string.IsNullOrEmpty(Ciudad))
            {
                query = query.Where(m => m.Ciudad.Contains(Ciudad));
            }


            var data = await query.ToListAsync();
            return data;
        }

        private bool TallerExists(int id)
        {
            return _context.Talleres.Any(e => e.id == id);
        }
    }
}
