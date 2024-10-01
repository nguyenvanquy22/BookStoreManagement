using System;
using System.Collections.Generic;

namespace QLCuaHangBanSach.Models
{
    public partial class Hdm
    {
        public string SoHd { get; set; } = null!;
        public DateTime NgayNhap { get; set; }
        public string MaNv { get; set; } = null!;
        public string MaNcc { get; set; } = null!;
        public decimal? ChietKhau { get; set; }
        public decimal? VAT { get; set; }
        public decimal TongTien { get; set; }

        public virtual Nhacungcap MaNccNavigation { get; set; } = null!;
        public virtual Nhanvien MaNvNavigation { get; set; } = null!;
    }
}
