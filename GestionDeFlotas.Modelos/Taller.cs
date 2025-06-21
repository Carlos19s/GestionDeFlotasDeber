using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeFlotas.Modelos
{
    public class Taller
    {
        public int id {  get; set; }
        public string Nombre {  get; set; }
        public string Ciudad { get; set; }
        public int CapacidadMaximaDeReparacionesSimultaneas {  get; set; }
        public List<Mantenimiento>? Mantenimientos { get; set; }

    }
}
