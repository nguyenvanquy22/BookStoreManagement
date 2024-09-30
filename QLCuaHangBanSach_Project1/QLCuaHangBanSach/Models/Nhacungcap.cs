using System;
using System.Collections.Generic;

namespace QLCuaHangBanSach.Models
{
    public partial class Nhacungcap
    {
        public Nhacungcap()
        {
            Hdms = new HashSet<Hdm>();
        }

        public string MaNcc { get; set; } = null!;
        public string TenNcc { get; set; } = null!;
        public string? Sdt { get; set; }
        public string? DiaChi { get; set; }

        public virtual ICollection<Hdm> Hdms { get; set; }
    }
}
