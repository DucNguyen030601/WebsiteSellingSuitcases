//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteSellingSuitcases.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tDinhKem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tDinhKem()
        {
            this.tDinhKemSPs = new HashSet<tDinhKemSP>();
        }
    
        public int MaDK { get; set; }
        public string TieuDe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tDinhKemSP> tDinhKemSPs { get; set; }
    }
}
