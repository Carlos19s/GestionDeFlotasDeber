using GestionDeFlotas.Modelos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sensores.Modelos
{
    public class Sensor
    {
        
            [Key]public int Id { get; set; }
            public int IDCamion { get; set; }
            public double Kilometraje { get; set; }
            public string EstadoMotor { get; set; }  
            public DateTime FechaRegistro { get; set; }
        
    }
}
