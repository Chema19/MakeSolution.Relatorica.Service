using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSolution.Relatorica.Entities
{
    public class SonidoEntity
    {
        public Int32? SonidoId { set; get; }
        public String Nombre { set; get; }
        public String Url { set; get; }
        public Int32? GeneroId { set; get; }
        public String Estado { set; get; }
        public DateTime FechaRegistro { set; get; } 
        public Int32? UsuarioId { set; get; }
    }
}
