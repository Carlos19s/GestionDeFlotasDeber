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
    public class MantenimientoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MantenimientoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Mantenimientoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mantenimiento>>> GetMantenimiento()
        {
            return await _context.Mantenimiento.ToListAsync();
        }
        // Método para filtrar mantenimientos
        [HttpGet("Filtrar")]
        public async Task<ActionResult<IEnumerable<Mantenimiento>>> GetMantenimientosConFiltros(int? tallerId, string? searchTerm, Boolean? Pendiente)
        {
            var query = _context.Mantenimiento.AsQueryable();

            // Filtro por el ID del Taller
            if (tallerId.HasValue)
            {
                query = query.Where(m => m.TallerId == tallerId.Value);
            }

            // Filtro por término de búsqueda en el tipo de mantenimiento
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(m => m.tipoMantenimiento.Contains(searchTerm));
            }

            // Filtro por pendiente (solo si Pendiente tiene un valor)
            if (Pendiente.HasValue)
            {
                query = query.Where(m => m.Pendiente == Pendiente.Value);
            }

            var data = await query.ToListAsync();
            return data;
        }




        // GET: api/Mantenimientoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mantenimiento>> GetMantenimiento(int id)
        {
            var mantenimiento = await _context.Mantenimiento.FindAsync(id);

            if (mantenimiento == null)
            {
                return NotFound();
            }

            return mantenimiento;
        }

        // PUT: api/Mantenimientoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMantenimiento(int id, Mantenimiento mantenimiento)
        {
            if (id != mantenimiento.Id)
            {
                return BadRequest();
            }

            _context.Entry(mantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MantenimientoExists(id))
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

        // POST: api/Mantenimientoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mantenimiento>> PostMantenimiento(Mantenimiento mantenimiento)
        {
            _context.Mantenimiento.Add(mantenimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMantenimiento", new { id = mantenimiento.Id }, mantenimiento);
        }

        // DELETE: api/Mantenimientoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMantenimiento(int id)
        {
            var mantenimiento = await _context.Mantenimiento.FindAsync(id);
            if (mantenimiento == null)
            {
                return NotFound();
            }

            _context.Mantenimiento.Remove(mantenimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MantenimientoExists(int id)
        {
            return _context.Mantenimiento.Any(e => e.Id == id);
        }
    }
}
