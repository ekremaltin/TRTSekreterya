//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TRTSekreterya.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class sirket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sirket()
        {
            this.iletisimToSirkets = new HashSet<iletisimToSirket>();
            this.kisis = new HashSet<kisi>();
        }
    
        public int sirketID { get; set; }
        public string sirketAdi { get; set; }
        public string sirketSektor { get; set; }
        public Nullable<int> sirketAdresID { get; set; }
        public string sirketSorumluAdiSoyadi { get; set; }
        public string sirketSorumluTel { get; set; }
    
        public virtual adre adre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<iletisimToSirket> iletisimToSirkets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<kisi> kisis { get; set; }
    }
}
