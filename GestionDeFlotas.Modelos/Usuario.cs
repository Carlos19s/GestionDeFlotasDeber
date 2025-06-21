using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeFlotas.Modelos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string User {  get; set; }
        [EmailAddress]
        public string Email {  get; set; }
        public string Password { get; set; }
    }
}
