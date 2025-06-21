using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionDeFlotas.Modelos;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Sensores.Modelos;

namespace AlertasYSensores.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertasController : ControllerBase
    {
        private readonly DbContext _context;

        public AlertasController(DbContext context)
        {
            _context = context;
        }
        [HttpGet("GenerarMantenimientoPredictivo")]
        public async Task<IActionResult> GenerarAlertasMantenimientoPredictivo()
        {
            var sensores = await _context.Set<Sensor>().ToListAsync();
            var alertasGeneradasList = new List<Alerta>();

            foreach (var sensor in sensores)
            {
                if (sensor.Kilometraje>10000 )
                {
                    // Buscar si ya existe una alerta para el camión
                    var alertaExistente = await _context.Alerta
                        .FirstOrDefaultAsync(a => a.IdCamion == sensor.IDCamion);

                    if (alertaExistente != null)
                    {
                        // Si ya existe, actualizamos la alerta
                        alertaExistente.TipoAlerta = "Mantenimiento Preventivo";
                        alertaExistente.Mensaje = $"El camión {sensor.IDCamion} ha alcanzado {sensor.Kilometraje} km  el motor en estado {sensor.EstadoMotor} y necesita mantenimiento.";
                        alertaExistente.Fecha = DateTime.UtcNow; // Actualizamos la fecha
                    }
                    else
                    {
                        // Si no existe, creamos una nueva alerta
                        var alerta = new Alerta
                        {
                            IdCamion = sensor.IDCamion,
                            TipoAlerta = "Mantenimiento Preventivo",
                            Mensaje = $"El camión {sensor.IDCamion} ha alcanzado {sensor.Kilometraje} km  el motor en estado {sensor.EstadoMotor}  y necesita mantenimiento.",
                            Fecha = DateTime.UtcNow, // Convierte la fecha a UTC
                        };

                        _context.Alerta.Add(alerta);
                        alertasGeneradasList.Add(alerta);
                    }
                }
            }

            await _context.SaveChangesAsync();

            if (alertasGeneradasList.Any())
            {
                return Ok(alertasGeneradasList);
            }
            else
            {
                return NoContent();
            }
        }


        // GET: api/Alertas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alerta>>> GetAlerta()
        {
            return await _context.Alerta.ToListAsync();
        }

        // GET: api/Alertas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alerta>> GetAlerta(int id)
        {
            var alerta = await _context.Alerta.FindAsync(id);

            if (alerta == null)
            {
                return NotFound();
            }

            return alerta;
        }

        // PUT: api/Alertas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlerta(int id, Alerta alerta)
        {
            
            if (id != alerta.Id)
            {
                return BadRequest();
            }
            alerta.Fecha = DateTime.UtcNow;
            _context.Entry(alerta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertaExists(id))
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

        // POST: api/Alertas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alerta>> PostAlerta(Alerta alerta)
        {
            alerta.Fecha = DateTime.UtcNow;
            _context.Alerta.Add(alerta);
           
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlerta", new { id = alerta.Id }, alerta);
        }

        // DELETE: api/Alertas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlerta(int id)
        {
            var alerta = await _context.Alerta.FindAsync(id);
            if (alerta == null)
            {
                return NotFound();
            }

            _context.Alerta.Remove(alerta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool AlertaExists(int id)
        {
            return _context.Alerta.Any(e => e.Id == id);
        }
    }
}
