using System;
using System.Collections.Generic;

namespace QLCuaHangBanSach.Models
{
    public partial class Hdb
    {
        public string SoHd { get; set; } = null!;
        public DateTime NgayBan { get; set; }
        public string MaKh { get; set; } = null!;
        public string MaNv { get; set; } = null!;
        public decimal? ChietKhau { get; set; }
        public decimal? VAT { get; set; }
        public decimal TongTien { get; set; }

        public virtual Khachhang MaKhNavigation { get; set; } = null!;
        public virtual Nhanvien MaNvNavigation { get; set; } = null!;
    }
}
