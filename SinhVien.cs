//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentManage
{
    using System;
    using System.Collections.Generic;
    
    public partial class SinhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SinhVien()
        {
            this.Diems = new HashSet<Diem>();
        }

        public SinhVien(int MaSinhVien, string HoTen, Nullable<System.DateTime> NgaySinh, string Lop, string GioiTinh)
        {
            this.MaSinhVien = MaSinhVien;
            this.HoTen = HoTen;
            this.NgaySinh = NgaySinh;
            this.Lop = Lop;
            this.GioiTinh = GioiTinh;
            this.Diems = new HashSet<Diem>();
        }

        public int MaSinhVien { get; set; }
        public string HoTen { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Lop { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diem> Diems { get; set; }
    }
}