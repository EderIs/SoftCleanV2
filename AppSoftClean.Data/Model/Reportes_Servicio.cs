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
    
    public partial class Reportes_Servicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reportes_Servicio()
        {
            this.RegRepSer = new HashSet<RegRepSer>();
        }
    
        public int id { get; set; }
        public Nullable<System.DateTime> dteFecha { get; set; }
        public Nullable<int> idCliente { get; set; }
        public Nullable<int> Folio { get; set; }
        public string Comentarios { get; set; }
    
        public virtual Hoteles Hoteles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegRepSer> RegRepSer { get; set; }
    }
}
