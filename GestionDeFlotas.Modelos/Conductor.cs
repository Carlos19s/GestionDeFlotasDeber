using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeFlotas.Modelos
{
    public class Conductor
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Licencia { get; set; }
        public DateTime date { get; set; }
        public List<Camion>? camiones { get; set; }
    }
}
