using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSolution.Relatorica.Entities
{
    public class UsuarioEntity
    {
        public Int32? UsuarioId { set; get; }
        public String Nombres { set; get; }
        public String Apellidos { set; get; }
        public String Credenciales { set; get; }
        public String Contrasenia { set; get; }
        public String Correo { set; get; }
        public String Estado { set; get; }
        public DateTime FechaRegistro { set; get; }
    }
}
