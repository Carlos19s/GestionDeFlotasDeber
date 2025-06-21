using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionDeFlotas.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sensores.Modelos;
using System.Net.Http;

namespace AlertasYSensores.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly DbContext _context;
        private readonly HttpClient _client;
        private static bool _simulacionActiva = false;
        // Inyectamos HttpClient en el constructor
        public SensorsController(DbContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

        // GET: api/Sensors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensor()
        {
            return await _context.Sensor.ToListAsync();
        }

        // GET: api/Sensors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetSensor(int id)
        {
            var sensor = await _context.Sensor.FindAsync(id);

            if (sensor == null)
            {
                return NotFound();
            }

            return sensor;
        }

        // POST: api/Sensors
        [HttpPost]
        public async Task<ActionResult> VerificarCamionExistente(Sensor sensor)
        {
            try
            {
                // URL de la API para consultar la base de datos SQL Server
                var camionesApiUrl = $"https://localhost:7259/api/Camions/{sensor.IDCamion}";

                using (var client = new HttpClient())
                {
                    // Hacemos la solicitud GET a la API de SQL Server para obtener el camión
                    var response = await client.GetAsync(camionesApiUrl);

                    // Si la respuesta no es exitosa, retornar que el camión no existe
                    if (!response.IsSuccessStatusCode)
                    {
                        return NotFound($"Camión con ID {sensor.IDCamion} no encontrado en la API de camiones.");
                    }
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserializar la cadena JSON a un objeto Camion
                    Camion camion = JsonConvert.DeserializeObject<Camion>(jsonResponse);
                    
                    camion.KilometrajeActual = sensor.Kilometraje;
                    camion.Estado = sensor.EstadoMotor;
                    var updateResponse = await client.PutAsJsonAsync(camionesApiUrl, camion);
                    sensor.FechaRegistro = DateTime.UtcNow;
                    _context.Sensor.Add(sensor);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetSensor", new { id = sensor.Id }, sensor);
                    
                    
                }
            }
            catch (HttpRequestException ex)
            {
                // Capturamos cualquier error al realizar la solicitud HTTP
                return StatusCode(500, $"Error de conexión con la API de camiones: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Capturamos cualquier otro error inesperado
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }
        [HttpPost("SimularSensoresAutomáticamente")]
        public async Task<ActionResult> SimularSensoresAutomáticamente()
        {
            _simulacionActiva = true;
            try
            {
                var random = new Random();

                while (_simulacionActiva)
                {
                    await Task.Delay(10000); // Esperar 10 segundos

                    var sensor = new Sensor
                    {
                        IDCamion = random.Next(1, 5),
                        Kilometraje = random.Next(1000, 500000),
                        EstadoMotor = random.Next(0, 2) == 0 ? "Bueno" : "Malo",
                        FechaRegistro = DateTime.UtcNow
                    };

                    var camionesApiUrl = $"https://localhost:7259/api/Camions/{sensor.IDCamion}";
                    var response = await _client.GetAsync(camionesApiUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        return NotFound($"Camión con ID {sensor.IDCamion} no encontrado.");
                    }
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserializar la cadena JSON a un objeto Camion
                    Camion camion = JsonConvert.DeserializeObject<Camion>(jsonResponse);

                    camion.KilometrajeActual = sensor.Kilometraje;
                    camion.Estado = sensor.EstadoMotor;
                    var updateResponse = await _client.PutAsJsonAsync(camionesApiUrl, camion);


                    _context.Sensor.Add(sensor);
                    await _context.SaveChangesAsync();

                    Console.WriteLine($"Sensor creado para camión {sensor.IDCamion} - Km: {sensor.Kilometraje} - Estado: {sensor.EstadoMotor}");
                }

                return Ok("Simulación detenida correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al simular sensores: {ex.Message}");
            }
        }

        [HttpPost("DetenerSimulacion")]
        public IActionResult DetenerSimulacion()
        {
            _simulacionActiva = false;
            return Ok("Simulación detenida por el usuario.");
        }




        // DELETE: api/Sensors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var sensor = await _context.Sensor.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }

            _context.Sensor.Remove(sensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool SensorExists(int id)
        {
            return _context.Sensor.Any(e => e.Id == id);
        }
    }
}
