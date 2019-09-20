using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSolution.Relatorica.Entities
{
    public class ParrafoEntity
    {
        public Int32? ParrafoId { set; get; }
        public String Texto { set; get; }
        public Int32? Orden { set; get; }
        public String Estado { set; get; }
        public DateTime FechaRegistro { set; get; }
        public Int32? HistoriaId { set; get; }
        public Int32? SonidoId { set; get; }
    }
}