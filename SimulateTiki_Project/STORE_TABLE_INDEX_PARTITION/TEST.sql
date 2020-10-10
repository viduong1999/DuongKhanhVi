USE NGHIEPVU
GO
--TEST
--INSERT NHACUNGCAP
EXEC sp_InNCC @TEN_NCC = N'Adios',         -- ntext
              @DIACHI = N'TP Đà Lạt, Tỉnh Lâm Đồng',    -- ntext
              @MADKKD = 'LDADIOS',           -- text
              @MATKHAU = '1',          -- text
		      @WEBSITE = 'nhangivan.com',          -- text
              @EMAIL = 'xinxeoaaqq@gmail.com',            -- text
              @HOTLINE = '9120293311',          -- varchar(100)
              @QUYMO = N'Lớn',           -- nvarchar(10)
              @NGAYTL = '2000-11-20', -- date
              @LOAI = N'B',            -- ntext
              @DAIDIEN = N'Trần Minh Anh',         -- ntext
              @CHUCVU = N'CEO',          -- ntext
              @EMAILPER = 'minan@gmail.com',         -- text
              @SDT = '0345662212',              -- varchar(100)
              @TRANGTHAIXN = 1    -- bit
GO

EXEC sp_InNCC @TEN_NCC = N'Lực lưỡng',         -- ntext
              @DIACHI = N'Hoàng Diệu 2, Quận Thủ Đức, TP HCM',    -- ntext
              @MADKKD = 'YCBD',           -- text
              @MATKHAU = '1',          -- text
		      @WEBSITE = 'ytroibd.com',          -- text
              @EMAIL = 'beydequa@gmail.com',            -- text
              @HOTLINE = '9120293311',          -- varchar(100)
              @QUYMO = N'Lớn',           -- nvarchar(10)
              @NGAYTL = '2000-11-20', -- date
              @LOAI = N'C',            -- ntext
              @DAIDIEN = N'Dương Ý',         -- ntext
              @CHUCVU = N'CEO',          -- ntext
              @EMAILPER = 'ycub@gmail.com',         -- text
              @SDT = '0345612341',              -- varchar(100)
              @TRANGTHAIXN = 1    -- bit
GO
SELECT * FROM NHACUNGCAP
GO
--LOGIN NCC
EXEC dbo.sp_LoginNCC @MADKKD = 'LDADIOS',  -- varchar(100)
                     @MATKHAU = '1' -- nvarchar(100)
GO

--UPDATE NCC
EXEC dbo.sp_UPNCC @TEN_NCC = N'Lực lưỡng',         -- ntext
                  @DIACHI = N'Hoàng Diệu 2, Quận Thủ Đức, TP HCM',    -- ntext
                  @MADKKD = 'YCBD',           -- text
                  @MATKHAU = '1',          -- text
                  @WEBSITE = 'phuongtheo.com',          -- text
                  @EMAIL = 'ytroibd@gmail.com',            -- text
                  @HOTLINE = '9-2999-1999',          -- varchar(100)
                  @QUYMO = N'Lớn',           -- nvarchar(10)
                  @NGAYTL = '2007-07-07', -- date
                  @LOAI = N'B',            -- ntext
                  @DAIDIEN = N'Dương Ý',         -- ntext
                  @CHUCVU = N'BG',          -- ntext
                  @EMAILPER = 'ybdqua@gmail.com',         -- text
                  @SDT = '066666182'              -- varchar(100)
GO
SELECT * FROM NHACUNGCAP
GO

--DELETE NCC
EXEC dbo.sp_DelNCC @MANV = 520,
				   @MATKHAU = '1',
				   @MADKKD = 'YCBD'
GO
SELECT * FROM dbo.NHACUNGCAP
GO
SELECT * FROM dbo.NHANVIEN
GO

--ĐÁNH GIÁ NCC
EXEC sp_InDGNCC @NAM=2019,
				@MA_NCC=1
GO

/* NHÂN VIÊN */
--INSERT
EXEC dbo.sp_InNHANVIEN @HOTEN = N'Đặng Thị Vân Khánh',
					   @CHUCVU = N'Quản lý kho',
					   @MATKHAU = '1'
SELECT * FROM NHANVIEN
GO

--UPDATE
EXEC dbo.sp_UpNHANVIEN @MANV = 11,
					   @HOTEN = N'Phan Văn Thỏ',
					   @CHUCVU = N'Giao hàng',
					   @MATKHAU = '1'
SELECT * FROM NHANVIEN
GO

--DELETE
EXEC sp_DelNVIEN @MANV = 81
SELECT * FROM NHANVIEN
GO

/* THÀNH TÍCH NHÂN VIÊN */
EXEC sp_InTTNV @MANV = 100,
				@NAM = 2019,
				@SODON_HOANTHANH = 720,
				@SO_REPORT = 10
GO
SELECT * FROM THANHTICHNV

/* MẶT HÀNG */
--INSERT
EXEC sp_InMH @TENMH = N'Đau khổ rời bỏ xa hoa',
			 @LOAIHANG =N'Sách',
			 @THUONGHIEU = N'BCD',
			 @GIATIEN = 300000,
			 @TGBAOHANH = 3
SELECT * FROM MATHANG
GO

--UPDATE
EXEC sp_UpMH @MAMH = 93,
			 @TENMH = N'Sống với deadline',
			 @LOAIHANG =N'Sách',
			 @THUONGHIEU = N'BCD',
			 @GIATIEN = 250000,
			 @TGBAOHANH = 1
GO
 
--DELETE
EXEC dbo.sp_DelMH @MAMH = 16
GO
SELECT * FROM MATHANG
SELECT * FROM MH_NCC
GO

/* MẶT HÀNG và NHÀ CUNG CẤP */
EXEC dbo.sp_MHNCC @MAMH = 77,   -- int
                  @MA_NCC = 4,  -- int
				  @SL= 4500,
                  @THAOTAC = 'I' -- varchar(100)
GO

/* THỐNG KÊ MH */
--INSERT
EXEC sp_InTKMH @MA_NCC = 4046,
			 @MAMH = 425,
			 @NAMTK = 2017,
			 @SL_BAN = 11
GO

/* KHO */
--INSERT
EXEC sp_INKHO @DIACHI = N'Hầm Thủ Thiêm, Quận 2, TP HCM',
			  @SLCHUA = 100000
SELECT * FROM KHO
GO

--UPDATE
EXEC sp_UpKHO @MAKHO = 56,
			  @DIACHI = N'Him Lam, Huyện Bình Chánh, TP HCM',
			  @SLCHUA = 4501201
GO

--DELETE 
EXEC sp_DelKHO 52
GO

/* KHO_NV */
--INSERT
EXEC sp_InKHO_NV @MAKHO = 1,
				 @MANV = 2
GO
SELECT*FROM KHO_NV

--UPDATE
EXEC sp_UpKHO_NV @MAKHO = 1,
				 @MANV = 2,
				 @MANV2 = 6
GO

--DELETE
DELETE FROM KHO_NV WHERE MANV=28 AND MAKHO=7
GO

/* HÓA ĐƠN */
--INSERT
EXEC sp_InHOADON @MATK=1,
	@NGAYDAT= '2018-02-14',
	@MAGG= NULL,
	@HUYDON=0,
	@PTTT= N'Tiền mặt',
	@STK= NULL
GO
SELECT*FROM HOADON

--UPDATE
EXEC sp_UpHOADON @MANV = 510,@MATKHAU = '1',@MAHD = 1 ,@TINHTRANG = N'Đã giao'
GO
SELECT*FROM LSDONHANG

/* PHIẾU BẢO HÀNH */
--INSERT 
EXEC sp_InPHIEUBAOHANH @MANV=550 ,
						@MATKHAU='1',
						@MAHD=1,
						@MAMH=16,
						@NGAYBH='2019-11-22',
						@TINHTRANGMH=N'Hư hỏng nhẹ',
						@TINHTRANGBH=N'Đang bảo hành',
						@CHIPHI=0
GO
SELECT*FROM PHIEUBAOHANH

--UPDATE
EXEC sp_UpPHIEUBAOHANH @MANV=550,
						@MATKHAU='1',
						@MAHD=1,
						@MAMH=16,
						@TINHTRANGBH=N'Đã bảo hành'
GO

/* PHIẾU TRẢ HÀNG */
--INSERT
EXEC sp_InPHIEUTRAHANG @MANV=501,@MATKHAU='1',@MAHD=14
GO
SELECT*FROM PHIEUTRAHANG

/* PHIẾU ĐỔI HÀNG */
EXEC sp_InPHIEUDOIHANG @MANV=501,@MATKHAU='1',@MAHD=15
GO
SELECT*FROM PHIEUDOIHANG

/* PHIẾU GIAO HÀNG */
EXEC sp_InPHIEUGIAOHANG @MANVQL=570,
						@MATKHAU='1',
						@MAHD=11,
						@MANV=489,
						@DIACHI=N'Quận Thủ Đức, TP HCM',
						@NGAYXUAT='2019-11-21'
GO
SELECT*FROM PHIEUGIAOHANG

--UPDATE
EXEC sp_UpPHIEUGIAOHANG @MANVQL=570,@MATKHAU='1',@MA_PGIAO=197,@MANV=449
GO

/* TK_MH */
--INSERT
EXEC sp_InTK_MH @MATK=1,@MAMH=2916,@MA_NCC=940,@SL=10
GO
SELECT*FROM TK_MH

--UPDATE
EXEC sp_UpTK_MH @MATK=1,@MAMH=2916,@MA_NCC=940,@SL=21
GO

/* PHIẾU GỬI HÀNG */
EXEC sp_InPGH @MA_NCC =1,
			   @MAKHO = 17,
			   @NGAYGUIDK = '2019-12-04'
GO
SELECT	* FROM dbo.PHIEUGUIHANG
SELECT*FROM NHACUNGCAP
SELECT*FROM KHO
SELECT*FROM KHO_NV
SELECT*FROM NHANVIEN

EXEC sp_InPGH_NV @MANV =225,
				 @MATKHAU ='123' ,
				 @MAKHO = 17,
				 @MA_PGUI = 2985 ,
				 @NGAYNHAP = '2019-12-08'
GO

/* PGH_MH */
EXEC sp_InPGH_MH @MA_PGUI = 2985,
					@MA_NCC  = 943,
					@MAMH = 331 ,
					@NGAYXUAT = '2019-12-28',
					@SL1MH = 20
GO

SELECT * FROM dbo.PHIEUGUIHANG
SELECT * FROM dbo.PGH_MH
SELECT * FROM dbo.MH_NCC
