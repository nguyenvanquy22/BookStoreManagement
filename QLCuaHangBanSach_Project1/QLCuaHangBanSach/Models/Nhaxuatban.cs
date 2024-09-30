using System;
using System.Collections.Generic;

namespace QLCuaHangBanSach.Models
{
    public partial class Nhaxuatban
    {
        public Nhaxuatban()
        {
            Saches = new HashSet<Sach>();
        }

        public string MaNxb { get; set; } = null!;
        public string TenNxb { get; set; } = null!;

        public virtual ICollection<Sach> Saches { get; set; }
    }
}
