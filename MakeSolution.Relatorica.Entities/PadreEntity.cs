using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSolution.Relatorica.Entities
{
    public class PadreEntity
    {
        public Int32? PadreId { set; get; }
        public String Nombres { set; get; }
        public String Apellidos { set; get; }
        public String Credenciales { set; get; }
        public String Contrasenia { set; get; }
        public String Correo { set; get; }
        public String Celular { set; get; }
        public String Estado { set; get; }
        public DateTime FechaRegistro { set; get; }
        public Int32? DistritoId { set; get; }
        public DateTime FechaNacimiento { set; get; }

    }
}