using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSolution.Relatorica.Entities
{
    public class PreguntaEntity
    {
        public Int32? PreguntaId { set; get; }
        public String Enunciado { set; get; }
        public DateTime FechaRegistro { set; get; }
        public String Estado { set; get; }
        public Int32? HistoriaId { set; get; }
    }
}