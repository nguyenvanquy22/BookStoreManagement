using System;
using System.Collections.Generic;

namespace QLCuaHangBanSach.Models
{
    public partial class ChiTietHdm
    {
        public string MaSach { get; set; } = null!;
        public short SoLuong { get; set; }
        public byte GiamGia { get; set; }
        public int DonGia { get; set; }
        public string SoHd { get; set; } = null!;

        public virtual Sach MaSachNavigation { get; set; } = null!;
        public virtual Hdm SoHdNavigation { get; set; } = null!;
    }
}
