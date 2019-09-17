using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSolution.Relatorica.Entities
{
    public class HistoryEntity
    {
        public Int32? HistoriaId { set; get; }
        public String Nombre { set; get; }
        public Int32? UsuarioId { set; get; }
        public String Argumento { set; get; }
        public DateTime FechaRegistro { set; get; }
        public Decimal Precio { set; get; }
        public String Editorial { set; get; }
    }
}
