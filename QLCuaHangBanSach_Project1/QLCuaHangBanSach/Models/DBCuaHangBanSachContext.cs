using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLCuaHangBanSach.Models
{
    public partial class DBCuaHangBanSachContext : DbContext
    {
        public DBCuaHangBanSachContext()
        {
        }

        public DBCuaHangBanSachContext(DbContextOptions<DBCuaHangBanSachContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietHdb> ChiTietHdbs { get; set; } = null!;
        public virtual DbSet<ChiTietHdm> ChiTietHdms { get; set; } = null!;
        public virtual DbSet<Hdb> Hdbs { get; set; } = null!;
        public virtual DbSet<Hdm> Hdms { get; set; } = null!;
        public virtual DbSet<Khachhang> Khachhangs { get; set; } = null!;
        public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; } = null!;
        public virtual DbSet<Nhanvien> Nhanviens { get; set; } = null!;
        public virtual DbSet<Nhaxuatban> Nhaxuatbans { get; set; } = null!;
        public virtual DbSet<Sach> Saches { get; set; } = null!;
        public virtual DbSet<Tacgia> Tacgia { get; set; } = null!;
        public virtual DbSet<Theloai> Theloais { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DBCuaHangBanSach;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHdb>(entity =>
            {
                entity.HasKey(e => new { e.SoHd, e.MaSach });

                entity.ToTable("ChiTietHDB");

                entity.Property(e => e.MaSach)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SoHd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SoHD");

                entity.HasOne(d => d.MaSachNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaSach)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietHD__MaSac__4F7CD00D");

                entity.HasOne(d => d.SoHdNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.SoHd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietHDB__SoHD__4E88ABD4");
            });

            modelBuilder.Entity<ChiTietHdm>(entity =>
            {
                entity.HasKey(e => new { e.SoHd, e.MaSach });

                entity.ToTable("ChiTietHDM");

                entity.Property(e => e.MaSach)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SoHd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SoHD");

                entity.HasOne(d => d.MaSachNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaSach)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietHD__MaSac__5165187F");

                entity.HasOne(d => d.SoHdNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.SoHd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietHDM__SoHD__52593CB8");
            });

            modelBuilder.Entity<Hdb>(entity =>
            {
                entity.HasKey(e => e.SoHd)
                    .HasName("PK__HDB__BC3CAB5770FBE871");

                entity.ToTable("HDB");

                entity.Property(e => e.SoHd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SoHD");

                entity.Property(e => e.ChietKhau).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaKH");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaNV");

                entity.Property(e => e.NgayBan).HasColumnType("datetime");

                entity.Property(e => e.TongTien).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.VAT)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("VAT");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Hdbs)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HDB__MaKH__48CFD27E");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.Hdbs)
                    .HasForeignKey(d => d.MaNv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HDB__MaNV__47DBAE45");
            });

            modelBuilder.Entity<Hdm>(entity =>
            {
                entity.HasKey(e => e.SoHd)
                    .HasName("PK__HDM__BC3CAB57A83160C1");

                entity.ToTable("HDM");

                entity.Property(e => e.SoHd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SoHD");

                entity.Property(e => e.ChietKhau).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaNCC");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaNV");

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.Property(e => e.TongTien).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.VAT)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("VAT");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.Hdms)
                    .HasForeignKey(d => d.MaNcc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HDM__MaNCC__4CA06362");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.Hdms)
                    .HasForeignKey(d => d.MaNv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HDM__MaNV__4BAC3F29");
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PK__KHACHHAN__2725CF1E06DFC62C");

                entity.ToTable("KHACHHANG");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaKH");

                entity.Property(e => e.DiaChi).HasMaxLength(45);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(45)
                    .HasColumnName("TenKH");
            });

            modelBuilder.Entity<Nhacungcap>(entity =>
            {
                entity.HasKey(e => e.MaNcc)
                    .HasName("PK__NHACUNGC__3A185DEBA207EF45");

                entity.ToTable("NHACUNGCAP");

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaNCC");

                entity.Property(e => e.DiaChi).HasMaxLength(45);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenNcc)
                    .HasMaxLength(45)
                    .HasColumnName("TenNCC");
            });

            modelBuilder.Entity<Nhanvien>(entity =>
            {
                entity.HasKey(e => e.MaNv)
                    .HasName("PK__NHANVIEN__2725D70A19ED2C3B");

                entity.ToTable("NHANVIEN");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaNV");

                entity.Property(e => e.ChucVu).HasMaxLength(50);

                entity.Property(e => e.DiaChi).HasMaxLength(45);

                entity.Property(e => e.GioiTinh).HasMaxLength(5);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TenNv)
                    .HasMaxLength(45)
                    .HasColumnName("TenNV");
            });

            modelBuilder.Entity<Nhaxuatban>(entity =>
            {
                entity.HasKey(e => e.MaNxb)
                    .HasName("PK__NHAXUATB__3A19482C2DE1F713");

                entity.ToTable("NHAXUATBAN");

                entity.Property(e => e.MaNxb)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaNXB");

                entity.Property(e => e.TenNxb)
                    .HasMaxLength(45)
                    .HasColumnName("TenNXB");
            });

            modelBuilder.Entity<Sach>(entity =>
            {
                entity.HasKey(e => e.MaSach)
                    .HasName("PK__SACH__B235742D444D1407");

                entity.ToTable("SACH");

                entity.Property(e => e.MaSach)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Anh).HasMaxLength(50);

                entity.Property(e => e.MaNxb)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaNXB");

                entity.Property(e => e.MaTg)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaTG");

                entity.Property(e => e.MaTl)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaTL");

                entity.Property(e => e.MoTa).HasColumnType("text");

                entity.Property(e => e.TenSach).HasMaxLength(45);

                entity.HasOne(d => d.MaNxbNavigation)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.MaNxb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SACH__MaNXB__4316F928");

                entity.HasOne(d => d.MaTgNavigation)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.MaTg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SACH__MaTG__440B1D61");

                entity.HasOne(d => d.MaTlNavigation)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.MaTl)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SACH__MaTL__44FF419A");
            });

            modelBuilder.Entity<Tacgia>(entity =>
            {
                entity.HasKey(e => e.MaTg)
                    .HasName("PK__TACGIA__27250074C51153B2");

                entity.ToTable("TACGIA");

                entity.Property(e => e.MaTg)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaTG");

                entity.Property(e => e.DiaChi).HasMaxLength(45);

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.TenTg)
                    .HasMaxLength(45)
                    .HasColumnName("TenTG");
            });

            modelBuilder.Entity<Theloai>(entity =>
            {
                entity.HasKey(e => e.MaTl)
                    .HasName("PK__THELOAI__2725007124190682");

                entity.ToTable("THELOAI");

                entity.Property(e => e.MaTl)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaTL");

                entity.Property(e => e.TenTl)
                    .HasMaxLength(45)
                    .HasColumnName("TenTL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
