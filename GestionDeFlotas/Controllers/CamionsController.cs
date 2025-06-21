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
    public class CamionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CamionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Camions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Camion>>> GetCamiones()
        {
            return await _context.Camiones.ToListAsync();
        }

        // GET: api/Camions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Camion>> GetCamion(int id)
        {
            var camion = await _context.Camiones.FindAsync(id);

            if (camion == null)
            {
                return NotFound();
            }

            return camion;
        }

        // PUT: api/Camions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCamion(int id, Camion camion)
        {
            if (id != camion.Id)
            {
                return BadRequest();
            }

            _context.Entry(camion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CamionExists(id))
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

        // POST: api/Camions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Camion>> PostCamion(Camion camion)
        {
            _context.Camiones.Add(camion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCamion", new { id = camion.Id }, camion);
        }

        // DELETE: api/Camions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCamion(int id)
        {
            var camion = await _context.Camiones.FindAsync(id);
            if (camion == null)
            {
                return NotFound();
            }

            _context.Camiones.Remove(camion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Filtrar")]
        public async Task<ActionResult<IEnumerable<Camion>>> GetCamionesConFiltros( string? searchTerm, string? Placa)
        {
            var query = _context.Camiones.AsQueryable();

            

            // Filtro por término de búsqueda en el tipo de mantenimiento
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(m => m.Marca.Contains(searchTerm));
            }

            // Filtro por pendiente (solo si Pendiente tiene un valor)
            if (!string.IsNullOrEmpty(Placa))
            {
                query = query.Where(m => m.Placa.Contains(Placa));
            }

            var data = await query.ToListAsync();
            return data;
        }
        private bool CamionExists(int id)
        {
            return _context.Camiones.Any(e => e.Id == id);
        }
    }
}
