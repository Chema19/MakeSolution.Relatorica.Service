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
    
    public partial class Historia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Historia()
        {
            this.Compra = new HashSet<Compra>();
            this.Parrafo = new HashSet<Parrafo>();
            this.Pregunta = new HashSet<Pregunta>();
        }
    
        public int HistoriaId { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> UsuarioId { get; set; }
        public string Argumento { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string Estado { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public string Editorial { get; set; }
        public string Imagen { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parrafo> Parrafo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pregunta> Pregunta { get; set; }
    }
}
