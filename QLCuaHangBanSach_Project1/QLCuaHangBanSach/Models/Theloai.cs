using System;
using System.Collections.Generic;

namespace QLCuaHangBanSach.Models
{
    public partial class Theloai
    {
        public Theloai()
        {
            Saches = new HashSet<Sach>();
        }

        public string MaTl { get; set; } = null!;
        public string TenTl { get; set; } = null!;
        public int SoLuong { get; set; }

        public virtual ICollection<Sach> Saches { get; set; }
    }
}
