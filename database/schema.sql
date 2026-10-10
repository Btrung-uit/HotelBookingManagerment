-- Tạo Database HotelBookingDB nếu chưa có
if not exists (select name from sys.databases where name = N'HotelBookingDB')
begin
	create database HotelBookingDB;
end
go

-- Chuyển ngữ cảnh sang Database vừa tạo
use HotelBookingDB;
go

-- Tạo bảng
-- Nguyên tắc: Bảng nào không phụ thuộc vào ai (không có khóa ngoại thì phải tạo trước

-- Bảng Users - Tài khoản nhân viên
create table dbo.Users
(
	UserId int identity(1,1) not null,
	-- identity(1, 1) có nghĩa là bắt đầu từ 1, mỗi lần thêm người mới sẽ tự động tăng thêm 1 đơn vị -> Không cần tự nhập ID
	Username varchar(50) not null,
	-- Username chỉ gồm chữ cái tiếng Anh, số, không có dấu -> dùng varchar để tiết kiệm bộ nhớ
	PasswordHash varchar(256) not null,
	FullName nvarchar(100) not null,
	-- Có thể là tên tiếng Việt có dấu Unicode -> dùng nvarchar
	Role varchar(20) not null,
	IsActive bit not null constraint DF_Users_IsActive default 1,
	-- Kiểu bit nhận giá trị 0 hoặc 1. Mặc định tạo tài khoản mới là kích hoạt 1
	CreatedAt datetime2 not null constraint DF_Users_CreatedAt default getdate(),
	-- Tự động lấy ngày giờ hiện tại của hệ thống máy tính ngay lúc thêm bản ghi (datetime2 là dạng mới khuyên dùng)
	constraint PK_Users primary key clustered (UserId),
	-- Khóa chính: UserId
	constraint UQ_Users_Username unique (Username),
	-- Tạo rằng buộc để giá trị username là độc nhất, tức không trùng username với nhau
	constraint CK_Users_Role check (Role in ('Manager', 'Receptionist'))
	-- Rằng buộc check để ngăn người dùng nhập bậy một vai trò lạ

	-- Ghi chú: not null được thêm xuyên suốt để tạo rào chắn bảo vệ, bắt buộc mỗi khi thêm một dòng mới thì ô đó tuyệt đối không được phép để trống
);
go

-- Bảng Customers - Khách hàng
create table dbo.Customers
(
	CustomerId int identity(1,1) not null,
	-- Tự tạo id, khi thêm customer mới id cộng thêm 1
	FullName nvarchar(100) not null,
	-- Tên có thể là tiếng Việt chứa unicode nên phải dùng nvarchar
	Phone varchar(15) not null,
	Email varchar(100) null,
	IdentityNumber varchar(20) not null,
	-- Số cccd hoặc hộ chiếu
	Address nvarchar(255) null,
	DateOfBirth date null,
	-- Dùng date vì chỉ cần lấy ngày tháng năm, nếu dùng datetime thì sẽ có giờ phút giây nên không cần thiết
	CreatedAt datetime2 not null constraint DF_Customers_CreatedAt default getdate(),

	constraint PK_Customers primary key clustered (CustomerId),
	-- Khóa chính CustomerId
	constraint UQ_Customers_Phone unique (Phone),
	-- Rằng buộc số điện thoại là độc nhất
	constraint UQ_Customers_IdentityNumber unique (IdentityNumber) 
	-- Rằng buộc IdentityNumber là độc nhất
);
go

-- Bảng RoomTypes - Loại phòng
create table dbo.RoomTypes
(
	RoomTypeId int identity(1, 1) not null,
	Name nvarchar(50) not null,
	Capacity int not null,
	PricePerNight decimal(18, 2) not null,
	Description nvarchar(255) null,

	constraint PK_RoomTypes primary key clustered (RoomTypeId),
	constraint UQ_RoomTypes_Name unique (Name),
	constraint CK_RoomTypes_Capacity check (Capacity > 0),
	constraint CK_RoomTypes_PricePerNight check (PricePerNight >= 0)
);
go

-- Bảng khóa ngoại
-- Bảng Rooms - Danh mục phòng
create table dbo.Rooms
(
	RoomId int identity(1, 1) not null,
	RoomNumber varchar(10) not null,
	RoomTypeId int not null,
	Floor int not null,
	Status varchar(20) not null constraint DF_Rooms_Status default 'AVAILABLE',
	Description nvarchar(255) null,

	constraint PK_Rooms primary key clustered (RoomId),
	constraint UQ_Rooms_RoomNumber unique (RoomNumber),
	constraint CK_Rooms_Floor check (Floor > 0),
	constraint CK_Rooms_Status check (Status in ('AVAILABLE', 'RESERVED', 'OCCUPIED', 'CLEANING', 'MAINTENANCE')),

	-- Khóa ngoại
	constraint FK_Rooms_RoomTypes foreign key (RoomTypeId)
		references dbo.RoomTypes (RoomTypeId)
		on delete no action
		on update cascade
);
go

-- Bảng Bookings - Đặt phòng
create table dbo.Bookings
(
	BookingId int identity(1, 1) not null,
	CustomerId int not null,
	BookingDate datetime2 not null constraint DF_Bookings_BookingDate default getdate(),
	CheckInDate datetime2 not null,
	CheckOutDate datetime2 not null,
	Status varchar(20) not null constraint DF_Bookings_Status default 'PENDING',
	TotalAmount decimal(18, 2) not null constraint DF_Bookings_TotalAmount default 0,
	CreatedBy int not null,
	CreatedAt datetime2 not null constraint DF_Bookings_CreatedAt default getdate(),

	constraint PK_Bookings primary key clustered (BookingId),

	-- Rằng buộc nghiệp vụ BR-01:
	constraint CK_Bookings_Dates check (CheckOutDate > CheckInDate),
	constraint CK_Bookings_Status check (Status in ('PENDING', 'CONFIRMED', 'CHECKED_IN', 'COMPLETED', 'CANCELLED')),
	constraint CK_Bookings_TotalAmount check (TotalAmount >= 0),

	-- 2 Khóa ngoại:
	constraint FK_Bookings_Customers foreign key (CustomerId)
		references dbo.Customers (CustomerId)
		on delete no action
		on update cascade,
	constraint FK_Bookings_Users foreign key (CreatedBy)
		references dbo.Users (UserId)
		on delete no action

	/* Giải thích: 
		no action (chặn đứng lại): Nếu Cha đang có Con, thì tuyệt đối không cho phép đụng vào Cha. Nếu cố tình xóa/sửa Cha, SQL Server sẽ quăng lỗi và dừng lại ngay lập tức.
		cascade (Thác đổ/Lan truyền): Cha làm sao thì Con làm vậy. Nếu cha đổi số -> Con tự động đổi theo. Nếu Cha bị xóa -> Con tự động bị xóa sổ theo luôn
	 
	 */
);
go

-- Bảng BookingDetails - Chi tiết đặt phòng
create table dbo.BookingDetails
(
	BookingDetailId int identity(1, 1) not null,
	BookingId int not null,
	RoomId int not null,
	PricePerNight decimal(18, 2) not null,
	NumberOfNights int not null,
	Subtotal decimal(18, 2) not null,

	constraint PK_BookingDetails primary key clustered (BookingDetailId),

	constraint UQ_BookingDetails_Booking_Room unique (BookingId, RoomId),
	-- Chống chọn trùng 1 phòng trong 1 đơn

	constraint CK_BookingDetails_PricePerNight check (PricePerNight >= 0),
	constraint CK_BookingDetails_NumberOfNights check (NumberOfNights > 0),
	constraint CK_BookingDetails_Subtotal check (Subtotal >= 0),

	-- 2 Khóa ngoại:
	constraint FK_BookingDetails_Bookings foreign key (BookingId)
		references dbo.Bookings (BookingId)
		on delete cascade,
	constraint FK_BookingDetails_Rooms foreign key (RoomId)
		references dbo.Rooms (RoomId)
		on delete no action
);
go

-- Bảng Payment - Thanh toán
create table dbo.Payments
(
	PaymentId int identity(1, 1) not null,
	BookingId int not null,
	Amount decimal(18, 2) not null,
	PaymentMethod varchar(20) not null,
	PaymentDate datetime2 not null constraint DF_Payments_PaymentDate default getdate(),
	Status varchar(20) not null constraint DF_Payments_Status default 'COMPLETED',

	constraint PK_Payments primary key clustered (PaymentId),
	constraint CK_Payments_Amount check (Amount >= 0),
	constraint CK_Payments_Method check (PaymentMethod in ('CASH', 'BANK_TRANSFER', 'CREDIT_CARD')),
	constraint CK_Payments_Status check (Status in ('PENDING', 'COMPLETED', 'REFUNDED')),

	-- Khóa ngoại:
	constraint FK_Payments_Bookings foreign key (BookingId)
		references dbo.Bookings (BookingId)
		on delete no action
);
go

-- Tạo indexes tối ưu hiệu năng truy vấn

-- 1. Tìm kiếm khách hàng nhanh theo SĐT hoặc CCCD
create nonclustered index IX_Customers_Phone on dbo.Customers (Phone);
create nonclustered index IX_Customers_IdentityNumber on dbo.Customers (IdentityNumber);

-- 2. Lọc phòng trống theo Loại phòng và Trạng thái
create nonclustered index IX_Rooms_RoomTypeId_Status on dbo.Rooms (RoomTypeId, Status);

-- 3. Tối ưu thuật toán kiểm tra trùng lịch đặt phòng (Overlap Detection BR-02)
create nonclustered index IX_Bookings_Dates_Status on dbo.Bookings (CheckInDate, CheckOutDate, Status) include (BookingId, CustomerId);

-- 4. Tối ưu phép JOIN trên các Khóa ngoại
create nonclustered index IX_BookingDetails_BookingId on dbo.BookingDetails (BookingId);
create nonclustered index IX_BookingDetails_RoomId on dbo.BookingDetails (RoomId);
create nonclustered index IX_Payments_BookingId on dbo.Payments (BookingId);
go

print N'>>> CƠ SỞ DỮ LIỆU KHỞI TẠO HOÀN TẤT 100%! <<<';
go