//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MakeSolution.Relatorica.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pregunta
    {
        public int PreguntaId { get; set; }
        public string Enunciado { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string Estatus { get; set; }
        public Nullable<int> HistoriaId { get; set; }
    
        public virtual Historia Historia { get; set; }
    }
}
