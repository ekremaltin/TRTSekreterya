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
    
    public partial class kisi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public kisi()
        {
            this.adres = new HashSet<adre>();
            this.iletisimToKisis = new HashSet<iletisimToKisi>();
            this.planToKisis = new HashSet<planToKisi>();
        }
    
        public int kisiID { get; set; }
        public string kisiAdi { get; set; }
        public string kisiSoyadi { get; set; }
        public string kisiMeslek { get; set; }
        public string kisiUnvan { get; set; }
        public Nullable<System.DateTime> kisiKayitTarihi { get; set; }
        public string kisiEkBilgi { get; set; }
        public Nullable<int> kisiSirketID { get; set; }
        public Nullable<bool> kisiTakvimKilit { get; set; }
        public Nullable<int> birimID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<adre> adres { get; set; }
        public virtual birim birim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<iletisimToKisi> iletisimToKisis { get; set; }
        public virtual sirket sirket { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<planToKisi> planToKisis { get; set; }
    }
}