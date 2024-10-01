using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLCuaHangBanSach.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Hdbs = new HashSet<Hdb>();
        }

        public string MaKh { get; set; } = null;

        [Required(ErrorMessage = "Bạn chưa nhập họ và tên")]
        [Display(Name = "Họ và tên: ")]
        public string TenKh { get; set; } = null!;
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        [Display(Name = "Số điện thoại: ")]
        [RegularExpression(@"(?=.*[0-9]).{10,10}$", ErrorMessage = "Bạn phải nhập đủ 10 số")]
        public string? Sdt { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        [Display(Name = "Địa chỉ: ")]
        public string? DiaChi { get; set; }

        public virtual ICollection<Hdb> Hdbs { get; set; } = null;
    }
}
