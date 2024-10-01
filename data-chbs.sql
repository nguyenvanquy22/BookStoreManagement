use DBCuaHangBanSach;

-- create table

CREATE TABLE SACH
(
  MaSach varchar(10) PRIMARY KEY NOT NULL,
  TenSach nvarchar(45) NOT NULL,
  SoLuong INT NOT NULL,
  GiaNhap INT NOT NULL,
  GiaBan INT NOT NULL,
  SoTrang INT NOT NULL,
  KhoiLuong INT NOT NULL,
  Anh varchar(50) NOT NULL,
  MoTa text NOT NULL,
  MaNXB varchar(10) NOT NULL,
  MaTG varchar(10) NOT NULL,
  MaTL varchar(10) NOT NULL,
  FOREIGN KEY (MaNXB) REFERENCES NHAXUATBAN(MaNXB),
  FOREIGN KEY (MaTG) REFERENCES TACGIA(MaTG),
  FOREIGN KEY (MaTL) REFERENCES THELOAI(MaTL)
);

CREATE TABLE TACGIA
(
  MaTG varchar(10) PRIMARY KEY NOT NULL,
  TenTG nvarchar(45) NOT NULL,
  NamSinh int,
  DiaChi nvarchar(45),
  Email varchar(45)
);

CREATE TABLE THELOAI
(
  MaTL varchar(10) PRIMARY KEY NOT NULL,
  TenTL nvarchar(45) NOT NULL,
  SoLuong INT NOT NULL
);

CREATE TABLE NHACUNGCAP
(
  MaNCC varchar(10) PRIMARY KEY NOT NULL,
  TenNCC nvarchar(45) NOT NULL,
  SDT varchar(15),
  DiaChi nvarchar(45)
);

CREATE TABLE NHAXUATBAN
(
	MaNXB varchar(10) PRIMARY KEY NOT NULL,
    TenNXB nvarchar(45) NOT NULL
);

CREATE TABLE NHANVIEN
(
  MaNV varchar(10) PRIMARY KEY NOT NULL,
  TenNV nvarchar(45) NOT NULL,
  GioiTinh nvarchar(5) NOT NULL,
  SDT varchar(15) NOT NULL,
  DiaChi nvarchar(45) NOT NULL,
  NgaySinh date NOT NULL,
  Luong INT NOT NULL,
  ChucVu nvarchar(50) NOT NULL,
  TenDangNhap varchar(30) NOT NULL,
  MatKhau varchar(30) NOT NULL
);

CREATE TABLE KHACHHANG
(
  MaKH varchar(10) 	PRIMARY KEY NOT NULL,
  TenKH nvarchar(45) NOT NULL,
  SDT varchar(15),
  DiaChi nvarchar(45)
);

CREATE TABLE HDB
(
  SoHD varchar(10) NOT NULL,
  NgayBan datetime NOT NULL,
  MaKH varchar(10) NOT NULL,
  MaNV varchar(10) NOT NULL,
  ChietKhau decimal(5,2),
  VAT decimal(5,2),
  TongTien decimal(12,2),
  PRIMARY KEY (SoHD),
  FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV),
  FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)
);

CREATE TABLE HDM
(
  SoHD varchar(10) NOT NULL,
  NgayNhap datetime NOT NULL,
  MaNV varchar(10) NOT NULL,
  MaNCC varchar(10) NOT NULL,
  ChietKhau decimal(5,2),
  VAT decimal(5,2),
  TongTien decimal(12,2),
  PRIMARY KEY (SoHD),
  FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV),
  FOREIGN KEY (MaNCC) REFERENCES NHACUNGCAP(MaNCC)
);

create TABLE ChiTietHDB
(
  MaSach varchar(10) NOT NULL,
  SoLuong smallint NOT NULL,
  GiamGia tinyint NOT NULL,
  DonGia int NOT NULL,
  SoHD varchar(10) NOT NULL,
  FOREIGN KEY (SoHD) REFERENCES HDB(SoHD),
  FOREIGN KEY (MaSach) REFERENCES SACH(MaSach)
);

CREATE TABLE ChiTietHDM
(
  MaSach varchar(10) NOT NULL,
  SoLuong smallint NOT NULL,
  GiamGia tinyint NOT NULL,
  DonGia int NOT NULL,
  SoHD varchar(10) NOT NULL,
  FOREIGN KEY (MaSach) REFERENCES SACH(MaSach),
  FOREIGN KEY (SoHD) REFERENCES HDM(SoHD)
);

-- insert
INSERT INTO CHITIETHDB (SoHD, masach, soluong, dongia, giamgia) 
VALUES 	('HDB001', 'MS002', 1, 120000, 10),
		('HDB002', 'MS002', 3, 120000, 10),
		('HDB003', 'MS001', 2, 100000, 10),
		('HDB004', 'MS001', 2, 150000, 20),
		('HDB005', 'MS005', 1, 200000, 20);

INSERT INTO CHITIETHDM (SoHD, masach, soluong, dongia, giamgia) 
VALUES 	('HDM001', 'MS003', 200, 80000, 20),
		('HDM002', 'MS001', 400, 100000, 20),
		('HDM003', 'MS005', 150, 150000, 20),
		('HDM004', 'MS004', 180, 200000, 20),
		('HDM005', 'MS002', 250, 70000, 20);
        
INSERT INTO HDB
VALUES 	('HDB001', '2023/01/05', 'KH001', 'NV002', 0, 8, 100000),
		('HDB002', '2023/01/17', 'KH002', 'NV001', 0, 8, 100000),
		('HDB003', '2023/02/02', 'KH003', 'NV002', 0, 8, 100000),
		('HDB004', '2023/02/20', 'KH004', 'NV002', 0, 8, 100000),
		('HDB005', '2023/03/11', 'KH005', 'NV003', 0, 8, 100000);

INSERT INTO HDM (sohd, ngaynhap, mancc, manv, chietkhau, vat, tongtien) 
VALUES 	('HDM001', '2022/12/10', 'NCC003', 'NV004', 0, 8, 100000),
		('HDM002', '2022/12/22', 'NCC002', 'NV001', 0, 8, 100000),
		('HDM003', '2023/01/12', 'NCC003', 'NV003', 0, 8, 100000),
		('HDM004', '2023/01/27', 'NCC004', 'NV003', 0, 8, 100000),
		('HDM005', '2023/02/13', 'NCC004', 'NV005', 0, 8, 100000);

INSERT INTO NHAXUATBAN 
VALUES 	('NXB001', N'Nhà Xuất Bản Thế Giới'),
		('NXB002', N'Nhà Xuất Bản Trẻ'),
		('NXB003', N'Nhà Xuất Bản Hội Nhà văn'),
		('NXB004', N'Nhà Xuất Bản Hội Nhà văn'),
		('NXB005', N'Nhà Xuất Bản Thế Giới');

INSERT INTO tacgia (MaTG, TenTG, NamSinh, DiaChi, Email) 
values	('TG001', N'Phạm Thành Long', 1963, 'VIET NAM', 'phamthanhlong@gmail.com'),
	('TG002', N'Nguyễn Hoàng Kim', 1968, 'VIET NAM', 'nguyenhoangkim@gmail.com'),
	('TG003', N'Trần Như Ý', 1954, 'VIET NAM', 'trannhuy@gmail.com'),
	('TG004', N'Lê Xuân Ngọc', 1971, 'VIET NAM', 'lexuanngoc@gmail.com'),
	('TG005', N'Nguyễn Quang Huy', 1950, 'VIET NAM', 'nguyenquanghuy@gmail.com');

INSERT INTO THELOAI 
VALUES ('TL001', 'Tam Ly', 100),
		('TL002', 'Khoa Hoc', 120),
		('TL003', 'Xa Hoi', 150),
		('TL004', 'Quan Tri', 170),
		('TL005', 'Kinh Doanh', 90);

INSERT INTO NHANVIEN
VALUES ('NV001', 'Nguyen Tuan Anh', 'Nam', '0367999777', 'Thanh Hoa', '1989-06-15', 5200000, N'Quản lý', 'nta@gmail.com', '123456'),
		('NV002', 'Vu Manh Thang', 'Nam', '0128789123', 'Ha Noi', '1990-12-17', 4000000, N'Quản lý', 'vmt@gmail.com', '123456'),
		('NV003', 'Nguyen Ba Dat', 'Nam', '0354893998', 'Cao Bang', '1995-08-19', 4500000, N'Nhân viên', 'nbd@gmail.com', '123456'),
		('NV004', 'Nguyen Van Thai', 'Nam', '0351722946', 'Lang Son', '1998-04-30', 3900000, N'Nhân viên', 'nvt@gmail.com', '123456'),
		('NV005', 'Le Dinh Tuan', 'Nam', '0131889456', 'Bac Can', '2000-11-02', 3100000, N'Nhân viên', 'ldt@gmail.com', '123456');

INSERT INTO KHACHHANG VALUES  
		('KH001', 'Nguyen Thi Thu', '0367882979', 'Quang Binh'),
		('KH002', 'Nguyen Duc Anh', '0134676992', 'Quang Ninh'),
		('KH003', 'Le Ngoc Tram', '0392877445', 'Ca Mau'),
		('KH004', 'Nguyen Quoc Anh', '0167192734', 'Tien Giang'),
		('KH005', 'Nguyen Phuong Anh', '0135898721', 'Ha Nam');

INSERT INTO nhaxuatban (MaNXB, TenNXB)
VALUES ('NXB001',	N'Nhà Xuất Bản Thế Giới'),
	('NXB002',	N'Nhà Xuất Bản Trẻ'),
	('NXB003',	N'Nhà Xuất Bản Hội Nhà văn'),
	('NXB004',	N'Nhà Xuất Bản Hội Nhà văn'),
	('NXB005',	N'Nhà Xuất Bản Thế Giới');
    
INSERT INTO NHACUNGCAP 
VALUES('NCC001',	N'TRƯỜNG AN',	'0389666444',	'VIET NAM'),
	  ('NCC002'	,N'TIẾN THỌ',	'0389563728',	'VIET NAM'),
	  ('NCC003'	,N'TẤN PHÁT'	,'0389957384',	'VIET NAM'),
	  ('NCC004'	,N'THÀNH CÔNG',	'0389039845',	'VIET NAM'),
	  ('NCC005'	,N'NGỌC VŨ',	'0389156354',	'VIET NAM');

INSERT INTO sach (MaSach, TenSach, SoLuong, GiaNhap, GiaBan, SoTrang, KhoiLuong, MaNXB, MaTG, MaTL, Anh, Mota ) 
VALUES('MS001',	N'Tony Buổi Sáng – Trên Đường Băng',	100,	100000,	150000,	250, 500, 'NXB001', 'TG001', 'TL001', 'MS001.png', 'mo ta sach'),
	  ('MS002',	N'Đắc Nhân Tâm',	80,	70000,	120000,	150,	500,	'NXB002', 'TG002', 'TL002', 'MS002.png', 'mo ta sach'),
	  ('MS003',	N'Nhà Giả Kim',	90,	80000,	100000,	200,	400,	'NXB003', 'TG003', 'TL003', 'MS003.png', 'mo ta sach'),
	  ('MS004',	N'Tuổi Trẻ Đáng Giá Bao Nhiêu?',	70,	200000,	250000,	260,	550,	'NXB004', 'TG004', 'TL004', 'MS004.png', 'mo ta sach'),
	  ('MS005',	N'Tư Duy Nhanh Và Chậm',	100,	150000,	200000,	237,	400,	'NXB005', 'TG001', 'TL001', 'MS001.png', 'mo ta sach');

INSERT INTO sach (MaSach, TenSach, SoLuong, GiaNhap, GiaBan, SoTrang, KhoiLuong, Mota, MaNXB, MaTG, MaTL, Anh)
VALUES
('MS006',	N'Bong Bóng Lên Trời',90,250000,300000,223,450,'','NXB005','TG001','TL001','MS006.png'),
('MS007',	N'Tiểu Thuyết Bố Già',193,240000,280000,300,460,'','NXB005','TG001','TL001','MS007.png'),
('MS008',	N'Mắt biếc',183,110000,130000,200,300,'','NXB002','TG001','TL001','MS008.png'),
('MS009',	N'Tội Ác Và Hình Phạt',60,220000,275000,360,500,'','NXB005','TG001','TL001','MS009.png'),
('MS0010',N'Tôi thấy Hoa vàng trên Cỏ xanh',140,130000,190000,223,350,'','NXB002','TG001','TL001','MS0010.png'),
('MS0011',N'Cô Gái đến từ Hôm qua',100,150000,200000,237,400,'','NXB002','TG001','TL001','MS0011.png'),
('MS0012',N'Út Quyên và Tôi',95,100000,130000,220,300,'','NXB002','TG001','TL001','MS0012.png'),
('MS0013',N'Con Chó Nhỏ Mang Giỏ Hoa Hồng ',195,150000,170000,260,450,'','NXB002','TG001','TL001','MS0013.png'),
('MS0014',N'Tôi là Bêtô',76,130000,180000,300,500,'','NXB002','TG001','TL001','MS0014.png'),
('MS0015',N'Đi Qua Hoa Cúc',223,120000,190000,260,400,'','NXB002','TG001','TL001','MS0015.png'),
('MS0016',N'Quẳng gánh lo đi và vui sống',100,160000,200000,255,390,'','NXB005','TG001','TL001','MS0016.png'),
('MS0017',N'Ông già và biển cả',230,230000,250000,290,500,'','NXB001','TG001','TL001','MS0017.png');

