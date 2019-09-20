using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSolution.Relatorica.Entities
{
    public class HijoEntity
    {
        public Int32? HijoId { set; get; }
        public String NombreCompleto { set; get; }
        public String Estado { set; get; }
        public DateTime FechaRegistro { set; get; }
        public DateTime FechaNacimiento { set; get; }
        public Int32? PadreId { set; get; }
    }
}
