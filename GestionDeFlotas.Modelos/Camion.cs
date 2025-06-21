using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Sensores.Modelos;

namespace GestionDeFlotas.Modelos
{
    public class Camion
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string Placa { get; set; }
        public double KilometrajeActual { get; set; }
        public string Estado { get; set; }

        [ForeignKey("Conductor")]
        public int ConductorId {  get; set; }
        [JsonIgnore]
        public Conductor? conductor { get; set; }
        [JsonIgnore]
        public List<Mantenimiento>? Mantenimientos { get; set; }
        public List<Sensor>? sensores { get; set; }

    }
}
