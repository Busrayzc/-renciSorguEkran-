//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityOrnek
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblNotlar
    {
        public int NotId { get; set; }
        public Nullable<int> Ogr { get; set; }
        public Nullable<int> Ders { get; set; }
        public Nullable<short> sinav1 { get; set; }
        public Nullable<short> sinav2 { get; set; }
        public Nullable<short> sinav3 { get; set; }
        public Nullable<decimal> ortalama { get; set; }
        public Nullable<bool> durum { get; set; }
    
        public virtual TblDersler TblDersler { get; set; }
        public virtual TblOgrenci TblOgrenci { get; set; }
    }
}