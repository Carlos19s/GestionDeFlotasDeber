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
    public class ConductorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConductorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Conductors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conductor>>> GetConductores()
        {
            return await _context.Conductores.ToListAsync();
        }

        // GET: api/Conductors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conductor>> GetConductor(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);

            if (conductor == null)
            {
                return NotFound();
            }

            return conductor;
        }

        // PUT: api/Conductors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConductor(int id, Conductor conductor)
        {
            if (id != conductor.Id)
            {
                return BadRequest();
            }

            _context.Entry(conductor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConductorExists(id))
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

        // POST: api/Conductors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Conductor>> PostConductor(Conductor conductor)
        {
            _context.Conductores.Add(conductor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConductor", new { id = conductor.Id }, conductor);
        }

        // DELETE: api/Conductors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConductor(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor == null)
            {
                return NotFound();
            }

            _context.Conductores.Remove(conductor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("Filtrar")]
        public async Task<ActionResult<IEnumerable<Conductor>>> GetConductoresConFiltros( string? searchTerm, string? Licencia)
        {
            var query = _context.Conductores.AsQueryable();

            // Filtro por el ID del Taller

            // Filtro por término de búsqueda en el tipo de mantenimiento
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(m => m.Name.Contains(searchTerm));
            }

            // Filtro por pendiente (solo si licencia tiene un valor)

            if (!string.IsNullOrEmpty(Licencia))
            {
                query = query.Where(m => m.Licencia.Contains(Licencia));
            }
            var data = await query.ToListAsync();
            return data;
        }


        private bool ConductorExists(int id)
        {
            return _context.Conductores.Any(e => e.Id == id);
        }
    }
}
