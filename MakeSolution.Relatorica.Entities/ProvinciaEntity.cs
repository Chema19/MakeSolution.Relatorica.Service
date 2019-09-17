using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeSolution.Relatorica.Entities
{
    public class ProvinciaEntity
    {
        public Int32? ProvinciaId { set; get; }
        public String Nombre { set; get; }
        public Int32? DepartamentoId { set; get; }
    }
}
