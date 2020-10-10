USE HTBanHang
GO

/*---------------------------------- MATHANG ----------------------------------*/
INSERT INTO MATHANG VALUES (1,N'Quạt đứng',1,450000,50,200,104,1),(2,N'Bắp cải trắng',2,35000,100,250,189,2),(3,N'Laptop H2007',3,30450000,50,500,142,3)
,(4,N'Nước hoa hồng Some By Me',4,213000,100,300,190,400),(5,N'Chết có kế hoạch',5,104000,100,300,145,89),(6,N'Bút chì 7B',6,45000,100,300,115,92)
,(7,N'Cúc vạn thọ',7,40000,10,100,45,70),(8,N'Mèo Ai Cập',8,15000000,10,100,11,30),(9,N'Bột bê tông',9,160000,100,300,122,41)
,(10,N'Cuốc đất M567300',10,230000,100,300,101,36),(11,N'Ananas Vintas Saigon 1980s',11,480000,100,300,121,419),(12,N'Strongbow dark fruit',12,18000,100,250,203,301)
,(13,N'Voucher buffet lẩu-nướng King BBQ',13,350000,100,300,121,399),(14,N'Account Netflix 1 năm',14,189000,100,300,179,298),(15,N'Thuốc bổ thận tráng dương',15,340000,100,250,209,378)
GO

CREATE PROC InMH @N INT
AS
	DECLARE @I INT=(SELECT MAX(MAMH) FROM MATHANG)+1
	WHILE(@I<@N)
	BEGIN
		IF(@I%3=0)
			INSERT INTO MATHANG VALUES (@I,N'MH '+CAST(@I AS NVARCHAR(50)),1,239000,50,200,134,10)
		ELSE IF(@I%4=0)
			INSERT INTO MATHANG VALUES (@I,N'MH '+CAST(@I AS NVARCHAR(50)),4,567000,100,300,134,20)
		ELSE IF(@I%5=0)
			INSERT INTO MATHANG VALUES (@I,N'MH '+CAST(@I AS NVARCHAR(50)),5,150000,100,300,149,21)
		ELSE IF(@I%7=0)
			INSERT INTO MATHANG VALUES (@I,N'MH '+CAST(@I AS NVARCHAR(50)),6,89000,100,300,210,19)
		ELSE
			INSERT INTO MATHANG VALUES (@I,N'MH '+CAST(@I AS NVARCHAR(50)),14,1023000,100,300,298,80)
		SET @I=@I+1
	END
GO
EXEC InMH 500
GO

SELECT*FROM MATHANG
/*---------------------------------- LOAI ----------------------------------*/
INSERT INTO LOAI VALUES (1,N'Gia dụng'),(2,N'Thực phẩm'),(3,N'Điện tử'),(4,N'Mỹ phẩm'),(5,N'Sách'),(6,N'Dụng cụ học tập')
,(7,N'Hoa'),(8,N'Thú cưng'),(9,N'Vật liệu xây dựng'),(10,N'Công cụ'),(11,N'Giày dép'),(12,N'Nước uống'),(13,N'Voucher')
,(14,N'Account'),(15,N'Thực phẩm chức năng')
GO

SELECT*FROM LOAI
/*---------------------------------- NHACUNGCAP ----------------------------------*/
INSERT INTO NHACUNGCAP VALUES (1,N'Ngọc Hân House'),(2,N'Sống Xanh Cùng Khánh Vi'),(3,N'Kem Store'),(4,N'Sống Không Thoát Vị Trí') 
GO

CREATE PROC InNCC @N INT
AS
	DECLARE @I INT=(SELECT MAX(MANCC) FROM NHACUNGCAP)+1
	WHILE(@I<@N)
	BEGIN
		INSERT INTO NHACUNGCAP VALUES (@I,N'NCC'+CAST(@I AS NVARCHAR(50)))
		SET @I=@I+1
	END
GO
EXEC InNCC 500
GO
SELECT*FROM NHACUNGCAP

/*---------------------------------- NHANVIEN ----------------------------------*/
INSERT INTO NHANVIEN VALUES (1,'1',N'Trần Quốc Bảo',N'Giao hàng'),
(2,'2',N'Trương Huỳnh Kim Ngân',N'Bán hàng'),
(3,'3',N'Dương Thị Ý',N'Bán hàng'),
(4,'4',N'Nguyễn Bảo Ngọc',N'Đăng tin'),
(5,'5',N'Võ Văn Trung',N'Quảng cáo'),
(6,'6',N'Nguyễn Phước Anh',N'Quản lí'),
(7,'7',N'Bối Vy Vy',N'Thủ quỹ')
GO

CREATE PROC InNV @N INT
AS
	DECLARE @I INT=(SELECT MAX(MANV) FROM NHANVIEN)+1
	WHILE(@I<@N)
	BEGIN
		IF(@I%3=0)
			INSERT INTO NHANVIEN VALUES (@I,@I,N'Nguyễn Phương '+CAST(@I AS NVARCHAR(50)),N'Giao hàng')
		ELSE IF(@I%4=0)
			INSERT INTO NHANVIEN VALUES (@I,@I,N'Nguyễn Thị Cẩm '+CAST(@I AS NVARCHAR(50)),N'Đăng tin')
		ELSE IF(@I%5=0)
			INSERT INTO NHANVIEN VALUES (@I,@I,N'Lương Tường '+CAST(@I AS NVARCHAR(50)),N'Quảng cáo')
		ELSE IF(@I%7=0)
			INSERT INTO NHANVIEN VALUES (@I,@I,N'Võ Văn '+CAST(@I AS NVARCHAR(50)),N'Thủ quỹ')
		ELSE IF(@I%6=0)
			INSERT INTO NHANVIEN VALUES (@I,@I,N'Huỳnh Thanh '+CAST(@I AS NVARCHAR(50)),N'Quản lí')
		ELSE
			INSERT INTO NHANVIEN VALUES (@I,@I,N'Đặng Thị '+CAST(@I AS NVARCHAR(50)),N'Bán hàng')
		SET @I=@I+1
	END
GO
EXEC InNV 500
GO
SELECT*FROM NHANVIEN

/*---------------------------------- DONNHAPHANG ----------------------------------*/
INSERT INTO DONNHAPHANG VALUES (1,2,30,3048500000,'2020-01-01',N'Số lượng dưới mức tối thiểu',N'Đã xác nhận')
GO
SELECT*FROM DONNHAPHANG
/*---------------------------------- DSNHAPHANG ----------------------------------*/
INSERT INTO DSNHAPHANG VALUES (1,1,100,2),(1,2,100,3)
GO

/*---------------------------------- DONTRAHANG ----------------------------------*/
INSERT INTO DONTRAHANG VALUES (1,2,'2020-03-02',N'Sống Xanh Cùng Khánh Vi')
GO

/*---------------------------------- DSTRAHANG ----------------------------------*/

/*---------------------------------- HOADON ----------------------------------*/

/*---------------------------------- MATHANGLOI ----------------------------------*/

/*---------------------------------- HDTHE ----------------------------------*/

/*---------------------------------- HD_MH ----------------------------------*/

/*---------------------------------- PHATTINQC ----------------------------------*/

/*---------------------------------- DOITACQC ----------------------------------*/

/*---------------------------------- THONGKECMT ----------------------------------*/
CREATE PROC InTKC @N INT
AS
	DECLARE @I INT=1
	WHILE(@I<@N)
	BEGIN
		INSERT INTO THONGKECMT VALUES (@I,1,'Irene','baedae@gmail.com','TPHCM',N'Abc',1,3)
		SET @I=@I+1
	END
GO
EXEC InTKC 167
GO

CREATE PROC InTKC2 @N INT
AS
	DECLARE @I INT=(SELECT MAX(ID) FROM THONGKECMT)+1
	WHILE(@I<@N)
	BEGIN
		IF(@I%3=0)
			INSERT INTO THONGKECMT VALUES (@I,2,'Miyawaki Sakura','baedaejap@gmail.com',N'Bình Dương','AFC',1,3)
		ELSE IF(@I%4=0)
			INSERT INTO THONGKECMT VALUES (@I,3,'Seulgi','baedae3@gmail.com',N'Huế','BCD',0,3)
		ELSE IF(@I%5=0)
			INSERT INTO THONGKECMT VALUES (@I,4,'Yeri','baedae5@gmail.com',N'Bến Tre','CDD',0,2)
		ELSE IF(@I%7=0)
			INSERT INTO THONGKECMT VALUES (@I,5,'Joy','baedae4@gmail.com',N'An Giang','ADM',1,3)
		ELSE
			INSERT INTO THONGKECMT VALUES (@I,6,'Wendy','baedae2@gmail.com',N'Hà Nội','CNC',1,2)
		SET @I=@I+1
	END
GO
EXEC InTKC2 500
GO

SELECT*FROM THONGKECMT

/*
/*---------------------------------- KHACHHANG ----------------------------------*/
INSERT INTO KHACHHANG VALUES (1,'baedae@gmail.com',N'Irene','TPHCM','0282837329',0),(2,'baedae2@gmail.com',N'Wendy',N'Hà Nội','8274192471',0),
(3,'baedae3@gmail.com',N'Seulgi',N'Huế','5238214190',0),(4,'baedae4@gmail.com',N'Joy',N'An Giang','8723127391',1),
(5,'baedae5@gmail.com',N'Yeri',N'Bến Tre','9828124812',1),(6,'baedaejap@gmail.com',N'Miyawaki Sakura',N'Bình Dương','921709174',0)
GO

CREATE PROC InKH @N INT
AS
	DECLARE @I INT=(SELECT MAX(MAKH) FROM KHACHHANG)+1
	WHILE(@I<@N)
	BEGIN
		IF(@I%3=0)
			INSERT INTO KHACHHANG VALUES (@I,'KH'+CAST(@I AS NVARCHAR(50))+'@gmail.com',N'Nguyễn Thị '+CAST(@I AS NVARCHAR(50)),N'TPHCM','0928021470',0)
		ELSE IF(@I%4=0)
			INSERT INTO KHACHHANG VALUES (@I,'KH'+CAST(@I AS NVARCHAR(50))+'@gmail.com',N'Trần Văn '+CAST(@I AS NVARCHAR(50)),N'TPHCM','0928021470',0)
		ELSE IF(@I%5=0)
			INSERT INTO KHACHHANG VALUES (@I,'KH'+CAST(@I AS NVARCHAR(50))+'@gmail.com',N'Dương Nguyễn '+CAST(@I AS NVARCHAR(50)),N'TPHCM','0928021470',0)
		ELSE IF(@I%7=0)
			INSERT INTO KHACHHANG VALUES (@I,'KH'+CAST(@I AS NVARCHAR(50))+'@gmail.com',N'Võ Văn '+CAST(@I AS NVARCHAR(50)),N'TPHCM','0928021470',0)
		ELSE
			INSERT INTO KHACHHANG VALUES (@I,'KH'+CAST(@I AS NVARCHAR(50))+'@gmail.com',N'Vương '+CAST(@I AS NVARCHAR(50)),N'TPHCM','0928021470',0)
		SET @I=@I+1
	END
GO
EXEC InKH 500
GO

SELECT*FROM KHACHHANG
*/