using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSolution.Relatorica.Entities
{
    public class CompraEntity
    {
        public Int32? CompraId { set; get; }
        public Int32? PadreId { set; get; }
        public Int32? HistoriaId { set; get; }
        public DateTime FechaCompra { set; get; }
        public String Estado { set; get; }
        public Decimal? Costo { set; get; }
    }
}