using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSolution.Relatorica.Entities
{
    public class AvanceEntity
    {
        public Int32? AvanceId { set; get; }
        public Int32? HijoId { set; get; }
        public Decimal PorcentajeAvance { set; get; }
        public Int32? ParrafoId { set; get; }
        public String Estado { set; get; }
        public DateTime FechaRegistro { set; get; }
    }
}
