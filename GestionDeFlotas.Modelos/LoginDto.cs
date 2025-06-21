using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeFlotas.Modelos
{
    public class LoginDto
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
