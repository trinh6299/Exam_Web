//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exam_Web_Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class HocSinh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HocSinh()
        {
            this.LanThis = new HashSet<LanThi>();
            this.PhanHois = new HashSet<PhanHoi>();
        }
    
        public int HocSinhID { get; set; }
        public Nullable<int> TaiKhoanID { get; set; }
        public string TenHS { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public Nullable<bool> GioiTinh { get; set; }
        public string Email { get; set; }
    
        public virtual TaiKhoan TaiKhoan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LanThi> LanThis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhanHoi> PhanHois { get; set; }
    }
}