using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GestionDeFlotas.Modelos;

namespace Sensores.Modelos
{
    public class Alerta
    {
        [Key]public int Id { get; set; }
        [ForeignKey("Camion")]
        public int IdCamion { get; set; }   // Opcional: puede no estar asociado a un camión (ej: login)
        public string TipoAlerta { get; set; }    
        public string Mensaje { get; set; }       // Descripción detallada
        public DateTime Fecha { get; set; }       // Fecha y hora de la alerta
    }
}
