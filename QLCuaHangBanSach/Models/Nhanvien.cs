using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLCuaHangBanSach.Models
{
    public partial class Nhanvien
    {
        public Nhanvien()
        {
            Hdbs = new HashSet<Hdb>();
            Hdms = new HashSet<Hdm>();
        }

        [Display(Name = "Mã Nhân Viên")]
        public string MaNv { get; set; } = null;
        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = " Bạn chưa nhập tên !")]
        [Display(Name = "Tên Nhân Viên")]
        public string TenNv { get; set; } = null!;

        [RegularExpression("^(Nam|Nữ)$", ErrorMessage = "Giới tính phải là Nam hoặc Nữ.")]
        [Required(ErrorMessage = " Bạn chưa nhập giới tính !")]
        [Display(Name = "Giới Tính")]
        public string GioiTinh { get; set; } = null!;

        [Required(ErrorMessage = " Số điện thoại bắt buộc và có 10 số ")]
        [RegularExpression(@"(?=.*^0[0-9]).{10}$")]
        [Display(Name = "Số Điện Thoại")]
        public string Sdt { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ !")]
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; } = null!;

        [Required(ErrorMessage = " Bạn chưa nhập ngày sinh! ")]
        [Display(Name = "Ngày Sinh")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập lương !")]
        [Display(Name = "Lương")]
        public int Luong { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập chức vụ !")]
        [Display(Name = "Chức Vụ")]
        public string ChucVu { get; set; } = null!;

        [Required(ErrorMessage = "Bạn chưa nhập tên đăng nhập !")]
        [Display(Name = "Tên Đăng Nhập")]
        public string TenDangNhap { get; set; } = null!;

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu !")]
        [Display(Name = "Mật Khẩu")]
        public string MatKhau { get; set; } = null!;

        public virtual ICollection<Hdb> Hdbs { get; set; }
        public virtual ICollection<Hdm> Hdms { get; set; }
    }
}
