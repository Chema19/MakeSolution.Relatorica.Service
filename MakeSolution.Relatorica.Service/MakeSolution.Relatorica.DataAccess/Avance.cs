//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MakeSolution.Relatorica.Service.MakeSolution.Relatorica.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Avance
    {
        public int AvanceId { get; set; }
        public Nullable<int> HijoId { get; set; }
        public Nullable<decimal> PorcentajeAvance { get; set; }
        public Nullable<int> ParrafoId { get; set; }
        public string Estado { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
    
        public virtual Hijo Hijo { get; set; }
        public virtual Parrafo Parrafo { get; set; }
    }
}
