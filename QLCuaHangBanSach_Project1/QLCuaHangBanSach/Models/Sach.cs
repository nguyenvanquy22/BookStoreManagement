using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLCuaHangBanSach.Models
{
    public partial class Sach
    {
        [Required(ErrorMessage = "Bạn phải nhập mã sách")]
        public string MaSach { get; set; } = null!;

        [Required(ErrorMessage = "Bạn phải nhập tên sách")]
        public string TenSach { get; set; } = null!;

        [Required(ErrorMessage = "Bạn phải nhập số lượng sách")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng nhập số lượng lớn hơn 1")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập giá nhập sách")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng nhập số lượng lớn hơn 1")]
        public int GiaNhap { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập giá bán")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng nhập số lượng lớn hơn 1")]
        public int GiaBan { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập số trang")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng nhập số lượng lớn hơn 1")]
        public int SoTrang { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập khối lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng nhập số lượng lớn hơn 1")]
        public int KhoiLuong { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập ảnh")]
        public string Anh { get; set; } = null!;

        public string MoTa { get; set; } = null!;

        [Required(ErrorMessage = "Bạn phải nhập mã nhà xuất bản")]
        public string MaNxb { get; set; } = null!;

        [Required(ErrorMessage = "Bạn phải nhập mã nhà xuất bản")]
        public string MaTg { get; set; } = null!;

        [Required(ErrorMessage = "Bạn phải nhập mã nhà xuất bản")]
        public string MaTl { get; set; } = null!;

        public virtual Nhaxuatban? MaNxbNavigation { get; set; }
        public virtual Tacgia? MaTgNavigation { get; set; }
        public virtual Theloai? MaTlNavigation { get; set; }
    }
}
