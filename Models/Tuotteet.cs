//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppTilausDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Tuotteet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tuotteet()
        {
            this.Tilausrivit = new HashSet<Tilausrivit>();
        }
    
        [Key]
        public int TuoteID { get; set; }
        public string Nimi { get; set; }
        public Nullable<decimal> Ahinta { get; set; }
        public byte[] Kuva { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tilausrivit> Tilausrivit { get; set; }
    }
}
