//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppSoftClean.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Levantamiento_Equipos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Levantamiento_Equipos()
        {
            this.Pedidos_Area = new HashSet<Pedidos_Area>();
        }
    
        public int id { get; set; }
        public Nullable<int> idHotel { get; set; }
        public Nullable<int> idDivision { get; set; }
        public Nullable<System.DateTime> dteFecha { get; set; }
        public Nullable<int> NumHoja { get; set; }
    
        public virtual AdmDivisiones AdmDivisiones { get; set; }
        public virtual Hoteles Hoteles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedidos_Area> Pedidos_Area { get; set; }
    }
}
