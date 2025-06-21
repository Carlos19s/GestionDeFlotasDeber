using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestionDeFlotas.Modelos
{
    public class Mantenimiento
    {
        public int Id {  get; set; }
        public DateTime Fecha { get; set; }
        public string tipoMantenimiento { get; set; }
        [ForeignKey("Taller")]
        public int TallerId {  get; set; }
       
        public Taller? taller { get; set; }

        [ForeignKey("Camion")]
        public int CamionId { get; set; }

        public Camion? camion { get; set; }
        public Boolean Pendiente { get; set; } = false;
    }
}
