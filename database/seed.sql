-- Mục đích: Nạp dữ liệu mẫu ban đầu phục vụ kiểm thử và chạy Demo dự án

use HotelBookingDB;
go

-- 1. Nạp dữ liệu tài khoản mẫu (Bảng USERS)
-- Lưu ý: Mật khẩu được băm trực tiếp bằng thuật toán SHA2_256 (Khớp với C# .NET)
insert into dbo.Users(Username, PasswordHash, FullName, Role, IsActive)
values
(
	'admin',
	convert(varchar(64), hashbytes('SHA2_256', 'Admin@123'), 2),
	N'Nguyễn Quản Lý',
	'Manager',
	1
),
(
	'receptionist',
	convert(varchar(64), hashbytes('SHA2_256', 'Reception@123'), 2),
	N'Lê Tiếp Tân',
	'Receptionist',
	1
);
go

-- 2. Nạp danh mục loại phòng (Bảng ROOMTYPES)
-- Lưu ý: Phải có chữ N'...' trước chuỗi ký tự tiếng Việt có dấu
insert into dbo.RoomTypes (Name, Capacity, PricePerNight, Description)
values
(
    N'Standard Single', 
    1, 
    350000.00, 
    N'Phòng 1 giường đơn tiêu chuẩn, tiện nghi cơ bản, wifi tốc độ cao'
),
(
    N'Standard Double', 
    2, 
    500000.00, 
    N'Phòng 1 giường đôi lớn, ban công thoáng mát, thích hợp cho cặp đôi'
),
(
    N'Deluxe Family', 
    4, 
    850000.00, 
    N'Phòng gia đình 2 giường đôi lớn, view thành phố, trang bị tủ lạnh mini'
),
(
    N'Suite VIP', 
    2, 
    1500000.00, 
    N'Căn hộ cao cấp có phòng khách riêng, bồn tắm nằm cao cấp, view toàn cảnh'
);
go

-- 3. Nạp danh sách 20 căn phòng (Bảng ROOMS)
-- RoomTypeId: 1 = Standard Single | 2 = Standard Double | 3 = Deluxe Family | 4 = Suite VIP
insert into dbo.Rooms (RoomNumber, RoomTypeId, Floor, Status, Description)
values
-- TẦNG 1: 4 Phòng Standard Single (RoomTypeId = 1)
('101', 1, 1, 'AVAILABLE',   N'Phòng đơn tầng 1, gần sảnh chính'),
('102', 1, 1, 'OCCUPIED',    N'Phòng đơn đang có khách ở'),
('103', 1, 1, 'AVAILABLE',   N'Phòng đơn tầng 1, thoáng mát'),
('104', 1, 1, 'MAINTENANCE', N'Đang hỏng điều hòa, tạm khóa không cho đặt'), -- Test bẫy lỗi

-- TẦNG 2: 4 Phòng Standard Double (RoomTypeId = 2)
('201', 2, 2, 'AVAILABLE',   N'Phòng đôi tầng 2, ban công hướng vườn'),
('202', 2, 2, 'RESERVED',    N'Đã có khách đặt giữ trước'),
('203', 2, 2, 'CLEANING',    N'Khách vừa trả phòng lúc sáng, đang dọn vệ sinh'),
('204', 2, 2, 'AVAILABLE',   N'Phòng đôi tầng 2, yên tĩnh'),

-- TẦNG 3: 4 Phòng Deluxe Family (RoomTypeId = 3)
('301', 3, 3, 'AVAILABLE',   N'Phòng gia đình tầng 3, view thành phố'),
('302', 3, 3, 'OCCUPIED',    N'Gia đình 4 người đang lưu trú'),
('303', 3, 3, 'AVAILABLE',   N'Phòng gia đình tiện nghi đầy đủ'),
('304', 3, 3, 'AVAILABLE',   N'Phòng gia đình góc tầng 3 rộng rãi'),

-- TẦNG 4: 4 Phòng Deluxe Family (RoomTypeId = 3)
('401', 3, 4, 'AVAILABLE',   N'Phòng gia đình tầng 4 view cao đẹp'),
('402', 3, 4, 'AVAILABLE',   N'Phòng gia đình tầng 4'),
('403', 3, 4, 'RESERVED',    N'Khách đoàn đã đặt trước'),
('404', 3, 4, 'AVAILABLE',   N'Phòng gia đình thoáng mát'),

-- TẦNG 5: 4 Phòng Suite VIP (RoomTypeId = 4)
('501', 4, 5, 'AVAILABLE',   N'Phòng VIP tổng thống 501, view biển trực diện'),
('502', 4, 5, 'OCCUPIED',    N'Khách VIP đang ở nghỉ dưỡng'),
('503', 4, 5, 'AVAILABLE',   N'Phòng VIP 503 đầy đủ tiện nghi cao cấp'),
('504', 4, 5, 'MAINTENANCE', N'Đang sơn sửa lại ban công, tạm ngưng phục vụ'); -- Test bẫy lỗi VIP
go

-- 4. Nạp hồ sơ 10 khách hàng mẫu (Bảng CUSTOMERS)
-- Định dạng ngày sinh chuẩn quốc tế: 'YYYY-MM-DD'
insert into dbo.Customers (FullName, Phone, Email, IdentityNumber, Address, DateOfBirth)
values
(
    N'Nguyễn Văn An', 
    '0901234567', 
    'an.nguyen@gmail.com', 
    '079201001234', 
    N'123 Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh', 
    '1995-04-12'
),
(
    N'Trần Thị Bích', 
    '0912345678', 
    'bich.tran@gmail.com', 
    '001198002345', 
    N'45 Cầu Giấy, Hà Nội', 
    '1998-08-25'
),
(
    N'Lê Hoàng Cường', 
    '0987654321', 
    'cuong.le@yahoo.com', 
    '048192003456', 
    N'78 Nguyễn Văn Linh, Hải Châu, Đà Nẵng', 
    '1992-11-03'
),
(
    N'Phạm Minh Đức', 
    '0933456789', 
    'duc.pham@outlook.com', 
    '079185004567', 
    N'12 Đại lộ Bình Dương, Thủ Dầu Một, Bình Dương', 
    '1985-06-19'
),
(
    N'Hoàng Thị Mai', 
    '0978123456', 
    'mai.hoang@gmail.com', 
    '036200005678', 
    N'89 Lê Lợi, Ngô Quyền, Hải Phòng', 
    '2000-01-15'
),
(
    N'Đỗ Quốc Khánh', 
    '0945678901', 
    'khanh.do@gmail.com', 
    '092197006789', 
    N'56 30 Tháng 4, Ninh Kiều, Cần Thơ', 
    '1997-09-30'
),
(
    N'Vũ Hải Yến', 
    '0961234890', 
    'yen.vu@gmail.com', 
    '079199007890', 
    N'34 Thùy Vân, TP. Vũng Tàu', 
    '1999-12-05'
),
(
    N'Bùi Gia Huy', 
    '0909876543', 
    'huy.bui@gmail.com', 
    '001188008901', 
    N'67 Trần Hưng Đạo, Hạ Long, Quảng Ninh', 
    '1988-03-22'
),
(
    N'Đặng Thùy Linh', 
    '0918765432', 
    NULL, -- Khách không đăng ký email
    '049202009012', 
    N'90 Hùng Vương, TP. Huế', 
    '2002-07-10'
),
(
    N'Ngô Đình Trọng', 
    '0938112233', 
    'trong.ngo@gmail.com', 
    '079193001122', 
    NULL, -- Khách chưa cung cấp địa chỉ
    '1993-05-18'
);
go

-- 5. Nạp đơn đặt phòng và chi tiết đặt phòng (Bảng BOOKINGS & BOOKINGDETAILS)

-- A. Nạp 7 đơn đặt phòng (Bảng BOOKINGS)
-- CreatedBy = 2 (Tài khoản Lễ tân 'receptionist' tạo đơn)
insert into dbo.Bookings (CustomerId, BookingDate, CheckInDate, CheckOutDate, Status, TotalAmount, CreatedBy)
values
-- Đơn 1: Khách đang ở phòng 102 (3 đêm x 350k = 1.050.000)
(1, '2026-10-06 09:00:00', '2026-10-06 14:00:00', '2026-10-09 12:00:00', 'CHECKED_IN', 1050000.00, 2),

-- Đơn 2: Khách đang ở phòng 302 (3 đêm x 850k = 2.550.000)
(2, '2026-10-07 10:30:00', '2026-10-07 14:00:00', '2026-10-10 12:00:00', 'CHECKED_IN', 2550000.00, 2),

-- Đơn 3: Khách VIP đang ở phòng 502 (4 đêm x 1.5tr = 6.000.000)
(3, '2026-10-05 15:00:00', '2026-10-07 14:00:00', '2026-10-11 12:00:00', 'CHECKED_IN', 6000000.00, 2),

-- Đơn 4: Khách đặt trước phòng 202 (2 đêm x 500k = 1.000.000)
(4, '2026-10-08 08:00:00', '2026-10-09 14:00:00', '2026-10-11 12:00:00', 'CONFIRMED', 1000000.00, 2),

-- Đơn 5: Khách đoàn đặt trước 2 phòng 403 & 404 (2 đêm x 850k x 2 phòng = 3.400.000)
(5, '2026-10-08 11:00:00', '2026-10-12 14:00:00', '2026-10-14 12:00:00', 'CONFIRMED', 3400000.00, 2),

-- Đơn 6: Khách vừa trả phòng 203 sáng nay (4 đêm x 500k = 2.000.000)
(6, '2026-10-04 14:00:00', '2026-10-04 14:00:00', '2026-10-08 10:00:00', 'COMPLETED', 2000000.00, 2),

-- Đơn 7: Khách đã hủy đơn đặt phòng 101 (Test bẫy lỗi)
(7, '2026-10-08 09:30:00', '2026-10-15 14:00:00', '2026-10-17 12:00:00', 'CANCELLED', 700000.00, 2);
go

-- B. Nạp chi tiết các phòng cho từng đơn (Bảng BOOKINGDETAILS)
insert into dbo.BookingDetails (BookingId, RoomId, PricePerNight, NumberOfNights, Subtotal)
values
-- Đơn 1 gắn với phòng 102 (RoomId = 2)
(1, 2, 350000.00, 3, 1050000.00),

-- Đơn 2 gắn với phòng 302 (RoomId = 10)
(2, 10, 850000.00, 3, 2550000.00),

-- Đơn 3 gắn với phòng 502 (RoomId = 18)
(3, 18, 1500000.00, 4, 6000000.00),

-- Đơn 4 gắn với phòng 202 (RoomId = 6)
(4, 6, 500000.00, 2, 1000000.00),

-- Đơn 5 gắn với 2 phòng cùng lúc: Phòng 403 (RoomId = 15) và Phòng 404 (RoomId = 16)
(5, 15, 850000.00, 2, 1700000.00),
(5, 16, 850000.00, 2, 1700000.00),

-- Đơn 6 gắn với phòng 203 (RoomId = 7)
(6, 7, 500000.00, 4, 2000000.00),

-- Đơn 7 gắn với phòng 101 (RoomId = 1)
(7, 1, 350000.00, 2, 700000.00);
go

-- 6. Nạp lịch sử giao dịch thanh toán (Bảng PAYMENTS)
-- Phương thức: CASH (Tiền mặt), BANK_TRANSFER (Chuyển khoản), CREDIT_CARD (Thẻ)
-- Trạng thái: COMPLETED (Thành công), REFUNDED (Hoàn tiền)
insert into dbo.Payments (BookingId, Amount, PaymentMethod, PaymentDate, Status)
values
-- Đơn 1: Cọc trước 500k qua chuyển khoản
(1, 500000.00, 'BANK_TRANSFER', '2026-10-06 09:15:00', 'COMPLETED'),

-- Đơn 2: Quẹt thẻ thanh toán đủ 2.550.000 lúc nhận phòng
(2, 2550000.00, 'CREDIT_CARD', '2026-10-07 14:10:00', 'COMPLETED'),

-- Đơn 3: Khách VIP cọc 50% (3 triệu) qua chuyển khoản
(3, 3000000.00, 'BANK_TRANSFER', '2026-10-05 15:30:00', 'COMPLETED'),

-- Đơn 4: Khách cọc trước 500k
(4, 500000.00, 'BANK_TRANSFER', '2026-10-08 08:30:00', 'COMPLETED'),

-- Đơn 5: Khách đoàn đặt cọc 1 triệu
(5, 1000000.00, 'BANK_TRANSFER', '2026-10-08 11:20:00', 'COMPLETED'),

-- Đơn 6: Khách trả phòng sáng nay thanh toán đủ 2 triệu tiền mặt
(6, 2000000.00, 'CASH', '2026-10-08 10:05:00', 'COMPLETED'),

-- Đơn 7: Hoàn tiền 350k cho khách do đã hủy phòng (Test trường hợp Refund)
(7, 350000.00, 'BANK_TRANSFER', '2026-10-08 10:00:00', 'REFUNDED');
go

print N'>>> CHÚC MỪNG: DỮ LIỆU MẪU ĐÃ ĐƯỢC NẠP HOÀN TẤT VÀO HotelBookingDB! <<<';
go