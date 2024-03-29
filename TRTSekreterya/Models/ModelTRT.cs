﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class adre
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public adre()
    {
        this.sirkets = new HashSet<sirket>();
    }

    public int adresID { get; set; }
    public int adresUlkeID { get; set; }
    public Nullable<int> adresILID { get; set; }
    public string adresIlce { get; set; }
    public string adresPostaKodu { get; set; }
    public string adresAdres { get; set; }
    public string adresTip { get; set; }
    public Nullable<int> adresKisiID { get; set; }

    public virtual kisi kisi { get; set; }
    public virtual sehir sehir { get; set; }
    public virtual ulke ulke { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<sirket> sirkets { get; set; }
}

public partial class birim
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public birim()
    {
        this.kisis = new HashSet<kisi>();
        this.users = new HashSet<user>();
    }

    public int birimID { get; set; }
    public string birimAdi { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<kisi> kisis { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<user> users { get; set; }
}

public partial class iletisim
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public iletisim()
    {
        this.iletisimToKisis = new HashSet<iletisimToKisi>();
        this.iletisimToSirkets = new HashSet<iletisimToSirket>();
    }

    public int iletisimID { get; set; }
    public string iletisimTip { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<iletisimToKisi> iletisimToKisis { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<iletisimToSirket> iletisimToSirkets { get; set; }
}

public partial class iletisimToKisi
{
    public int itkID { get; set; }
    public Nullable<int> iletisimID { get; set; }
    public Nullable<int> kisiID { get; set; }
    public string value { get; set; }

    public virtual iletisim iletisim { get; set; }
    public virtual kisi kisi { get; set; }
}

public partial class iletisimToSirket
{
    public int itsID { get; set; }
    public Nullable<int> iletisimID { get; set; }
    public Nullable<int> sirketID { get; set; }
    public string value { get; set; }

    public virtual iletisim iletisim { get; set; }
    public virtual sirket sirket { get; set; }
}

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

public partial class plan
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public plan()
    {
        this.planToKisis = new HashSet<planToKisi>();
    }

    public int planID { get; set; }
    public string planUzunBilgi { get; set; }
    public string planKisaBilgi { get; set; }
    public Nullable<System.DateTime> planStartTarih { get; set; }
    public Nullable<System.DateTime> planEndTarih { get; set; }
    public string planMekan { get; set; }
    public Nullable<bool> planisCompleted { get; set; }
    public Nullable<bool> planFullDay { get; set; }
    public string planEkBilgi { get; set; }
    public string planColor { get; set; }
    public Nullable<int> planUserID { get; set; }

    public virtual user user { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<planToKisi> planToKisis { get; set; }
}

public partial class planToKisi
{
    public int pkID { get; set; }
    public Nullable<int> pkKisiID { get; set; }
    public Nullable<int> pkPlanID { get; set; }
    public Nullable<bool> pkisSource { get; set; }

    public virtual kisi kisi { get; set; }
    public virtual plan plan { get; set; }
}

public partial class sehir
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public sehir()
    {
        this.adres = new HashSet<adre>();
    }

    public int sehirID { get; set; }
    public int sehirUlkeID { get; set; }
    public string sehirAdi { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<adre> adres { get; set; }
    public virtual ulke ulke { get; set; }
}

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

public partial class ulke
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ulke()
    {
        this.adres = new HashSet<adre>();
        this.sehirs = new HashSet<sehir>();
    }

    public int ulkeID { get; set; }
    public string ulkeAdi { get; set; }
    public string ulkeTelKod { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<adre> adres { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<sehir> sehirs { get; set; }
}

public partial class user
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public user()
    {
        this.plans = new HashSet<plan>();
    }

    public int userID { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public Nullable<int> userYetkiID { get; set; }
    public string userAdSoyad { get; set; }
    public Nullable<int> userBirimID { get; set; }

    public virtual birim birim { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<plan> plans { get; set; }
    public virtual yetkiLogin yetkiLogin { get; set; }
}

public partial class yetkiLogin
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public yetkiLogin()
    {
        this.users = new HashSet<user>();
    }

    public int yetkiID { get; set; }
    public string yetki { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<user> users { get; set; }
}
