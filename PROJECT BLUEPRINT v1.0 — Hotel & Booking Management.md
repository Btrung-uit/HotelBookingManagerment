# PROJECT BLUEPRINT v1.0

## Hotel & Booking Management System

**Course:** Lập trình trực quan  
**Language:** C#  
**GUI Framework:** Windows Forms (WinForms)  
**IDE:** Visual Studio  
**Database:** SQL Server  
**Team size:** 4 members  
**Project role:** Team Leader + Developer  
**Target:** Hoàn thành trước giữa tháng 11/2026

---

# 1. PROJECT OVERVIEW

## 1.1. Problem Statement

Các nghiệp vụ khách sạn cơ bản thường liên quan đến nhiều đối tượng và trạng thái:

- Khách hàng
- Phòng
- Loại phòng
- Đặt phòng
- Nhận phòng
- Trả phòng
- Thanh toán
- Hóa đơn

Nếu quản lý thủ công hoặc bằng các bảng dữ liệu rời rạc, dễ xảy ra:

- Trùng lịch đặt phòng.
- Không theo dõi chính xác trạng thái phòng.
- Tính tiền sai.
- Khó tra cứu lịch sử khách hàng.
- Khó theo dõi doanh thu.
- Dữ liệu không nhất quán giữa các nghiệp vụ.

## 1.2. Proposed Solution

Xây dựng hệ thống **Hotel & Booking Management System** bằng C# WinForms, cung cấp một giao diện desktop thân thiện cho nhân viên khách sạn.

Hệ thống tập trung vào quy trình:

```text
Customer
   ↓
Booking
   ↓
Check-in
   ↓
Stay
   ↓
Check-out
   ↓
Payment / Invoice
   ↓
Room Cleaning
   ↓
Available
```

## 1.3. Target Users

### Primary User

**Receptionist / Front Desk Staff**

Thực hiện:

- Quản lý khách hàng.
- Kiểm tra phòng.
- Tạo booking.
- Check-in.
- Check-out.
- Thanh toán.

### Secondary User

**Manager**

Thực hiện:

- Theo dõi dashboard.
- Xem phòng.
- Xem booking.
- Xem doanh thu.
- Quản lý người dùng.

---

# 2. PROJECT OBJECTIVES

## 2.1. Functional Objectives

Hệ thống phải hỗ trợ:

1. Đăng nhập.
2. Quản lý khách hàng.
3. Quản lý loại phòng.
4. Quản lý phòng.
5. Tìm phòng trống.
6. Tạo booking.
7. Kiểm tra booking trùng lịch.
8. Hủy booking.
9. Check-in.
10. Check-out.
11. Tính tiền.
12. Thanh toán.
13. Hóa đơn.
14. Dashboard.
15. Báo cáo cơ bản.

## 2.2. Technical Objectives

Nhóm phải thể hiện được:

- Object-Oriented Programming.
- Separation of concerns.
- Layered architecture.
- Database relational design.
- CRUD.
- Validation.
- Exception handling.
- Business logic.
- SQL queries.
- Git/GitHub workflow.
- Debugging.
- Testing.

## 2.3. UX Objectives

Giao diện phải:

- Nhất quán.
- Dễ học.
- Không quá màu mè.
- Dễ nhìn.
- Dữ liệu thẳng hàng.
- Nút chức năng rõ ràng.
- Có validation.
- Có thông báo lỗi/thành công.
- Có confirmation với thao tác nguy hiểm.

---

# 3. PROJECT SCOPE

## 3.1. IN SCOPE — BẮT BUỘC

### Authentication

- Login.
- Logout.
- Role cơ bản.

### Customer Management

- Create.
- Read.
- Update.
- Delete / deactivate.
- Search.
- View booking history.

### Room Management

- Room list.
- Room type.
- Room price.
- Capacity.
- Status.
- Search/filter.

### Booking Management

- Create booking.
- Select customer.
- Select dates.
- Search available rooms.
- Add room.
- Calculate estimated total.
- Cancel booking.
- View booking details.

### Check-in

- Find booking.
- Verify guest.
- Confirm check-in.
- Update room status.

### Check-out

- View current stay.
- Calculate final amount.
- Confirm payment.
- Complete checkout.
- Update room status.

### Payment

- Cash.
- Bank transfer/card nếu nhóm có thời gian.
- Payment status.
- Payment history.

### Dashboard

- Total rooms.
- Available rooms.
- Occupied rooms.
- Today's check-in.
- Today's check-out.
- Revenue.

---

# 4. OUT OF SCOPE

Để tránh scope creep, v1.0 KHÔNG bắt buộc:

- Online booking qua website.
- Mobile application.
- Email/SMS notification.
- Online payment gateway.
- AI chatbot.
- Integration với OTA như Booking.com/Agoda.
- Multi-branch hotel.
- Real-time cloud synchronization.
- Advanced accounting.
- Inventory management.
- Housekeeping module phức tạp.

Nếu core system hoàn thành sớm mới xem xét feature mở rộng.

---

# 5. ACTORS

```text
                ┌─────────────────────┐
                │ Hotel Management App│
                └──────────┬──────────┘
                           │
              ┌────────────┴────────────┐
              │                         │
       Receptionist                  Manager
              │                         │
       Booking / Check-in        Dashboard / Report
       Check-out / Payment       Room / Customer
```

---

# 6. CORE USER FLOWS

## 6.1. Booking Flow

```text
Open Booking
      ↓
Select Customer
      ↓
Select Check-in / Check-out
      ↓
System checks available rooms
      ↓
Select Room
      ↓
Calculate estimated price
      ↓
Confirm booking
      ↓
Booking Created
```

## 6.2. Check-in Flow

```text
Find Booking
      ↓
Verify Customer
      ↓
Verify Room
      ↓
Confirm Check-in
      ↓
Room = OCCUPIED
      ↓
Booking = CHECKED_IN
```

## 6.3. Check-out Flow

```text
Find Active Booking
      ↓
Calculate room cost
      ↓
Add services / surcharge
      ↓
Apply discount
      ↓
Calculate final total
      ↓
Payment
      ↓
Generate Invoice
      ↓
Booking = COMPLETED
      ↓
Room = CLEANING
```

Sau khi phòng được xử lý:

```text
CLEANING → AVAILABLE
```

---

# 7. SYSTEM ARCHITECTURE

## 7.1. Architecture

Áp dụng layered architecture:

```text
┌──────────────────────────────────────┐
│         PRESENTATION LAYER           │
│             WinForms                 │
│ Login / Dashboard / Forms            │
└──────────────────┬───────────────────┘
                   │
┌──────────────────▼───────────────────┐
│          BUSINESS LAYER              │
│ Services / Validation / Rules        │
│ BookingService                       │
│ RoomService                          │
│ CustomerService                      │
│ PaymentService                       │
└──────────────────┬───────────────────┘
                   │
┌──────────────────▼───────────────────┐
│          DATA ACCESS LAYER           │
│ Repository / Query / DB Access       │
└──────────────────┬───────────────────┘
                   │
┌──────────────────▼───────────────────┐
│              SQL SERVER              │
└──────────────────────────────────────┘
```

## 7.2. Rule

WinForms **không trực tiếp chứa toàn bộ business logic**.

Không nên:

```text
Button_Click()
{
    150 dòng SQL + validation + calculation
}
```

Nên:

```text
Button_Click()
      ↓
BookingService.CreateBooking()
      ↓
BookingRepository
      ↓
SQL Server
```

---

# 8. PROJECT STRUCTURE

Đề xuất:

```text
HotelBookingManagement/
│
├── HotelBookingManagement.sln
│
├── src/
│   │
│   ├── HotelBookingManagement/
│   │   ├── Forms/
│   │   ├── Controls/
│   │   ├── Program.cs
│   │   └── UI/
│   │
│   ├── HotelBookingManagement.Business/
│   │   ├── Services/
│   │   ├── Validators/
│   │   └── Interfaces/
│   │
│   ├── HotelBookingManagement.Data/
│   │   ├── Repositories/
│   │   ├── Database/
│   │   └── Interfaces/
│   │
│   └── HotelBookingManagement.Models/
│       ├── Customer.cs
│       ├── Room.cs
│       ├── RoomType.cs
│       ├── Booking.cs
│       ├── BookingDetail.cs
│       ├── Payment.cs
│       └── User.cs
│
├── database/
│   ├── schema.sql
│   └── seed.sql
│
├── docs/
│   ├── ERD.png
│   ├── Architecture.png
│   ├── UseCase.png
│   ├── UI/
│   └── Report/
│
├── README.md
├── .gitignore
└── LICENSE
```

---

# 9. DATABASE DESIGN

## 9.1. Main Tables

### Users

```text
UserId
Username
PasswordHash
FullName
Role
IsActive
CreatedAt
```

### Customers

```text
CustomerId
FullName
Phone
Email
IdentityNumber
Address
DateOfBirth
CreatedAt
```

### RoomTypes

```text
RoomTypeId
Name
Capacity
PricePerNight
Description
```

### Rooms

```text
RoomId
RoomNumber
RoomTypeId
Floor
Status
Description
```

Status:

```text
AVAILABLE
RESERVED
OCCUPIED
CLEANING
MAINTENANCE
```

### Bookings

```text
BookingId
CustomerId
BookingDate
CheckInDate
CheckOutDate
Status
TotalAmount
CreatedBy
CreatedAt
```

Status:

```text
PENDING
CONFIRMED
CHECKED_IN
COMPLETED
CANCELLED
```

### BookingDetails

```text
BookingDetailId
BookingId
RoomId
PricePerNight
NumberOfNights
Subtotal
```

### Payments

```text
PaymentId
BookingId
Amount
PaymentMethod
PaymentDate
Status
```

---

# 10. DATABASE RELATIONSHIPS

```text
User
 │
 └──────< Booking

Customer
 │
 └──────< Booking
              │
              └──────< BookingDetail >────── Room
                                             │
                                             └──── RoomType

Booking
 │
 └──────< Payment
```

Relationship summary:

```text
Customer 1 ─── N Booking
User     1 ─── N Booking
Booking  1 ─── N BookingDetail
Room     1 ─── N BookingDetail
RoomType 1 ─── N Room
Booking  1 ─── N Payment
```

---

# 11. IMPORTANT BUSINESS RULES

## BR-01 — Check-out must be after check-in

```text
CheckOutDate > CheckInDate
```

Nếu không:

```text
Reject booking
```

## BR-02 — Không được double booking

Hai booking bị xem là overlap nếu:

```text
ExistingCheckIn < NewCheckOut
AND
ExistingCheckOut > NewCheckIn
```

Nếu tồn tại booking overlap:

```text
Room không khả dụng
```

## BR-03 — Không check-in booking đã cancelled

```text
CANCELLED → Check-in prohibited
```

## BR-04 — Không check-out booking chưa check-in

```text
CONFIRMED/PENDING → Check-out prohibited
```

## BR-05 — Room status phải đồng bộ

Ví dụ:

```text
Check-in  → OCCUPIED
Check-out → CLEANING
Cleaning complete → AVAILABLE
```

## BR-06 — Tổng tiền

```text
RoomCost
= NumberOfNights × PricePerNight

FinalAmount
= RoomCost
+ ServiceCost
+ Surcharge
- Discount
```

## BR-07 — Không được thanh toán vượt tổng tiền

```text
PaymentAmount <= RemainingAmount
```

---

# 12. CORE ALGORITHMS

## Algorithm 1 — Room Availability

### Input

```text
CheckInDate
CheckOutDate
```

### Processing

Tìm các room có booking overlap:

```text
ExistingCheckIn < RequestedCheckOut
AND
ExistingCheckOut > RequestedCheckIn
```

Loại các phòng có:

```text
MAINTENANCE
OCCUPIED
```

Tùy business rule, phòng RESERVED cũng phải được loại nếu overlap.

### Output

```text
List<Room>
```

---

# 13. ALGORITHM 2 — PRICE CALCULATION

```text
NumberOfNights
= CheckOutDate - CheckInDate

RoomCost
= NumberOfNights × PricePerNight

FinalAmount
= RoomCost
+ ServiceCost
+ Surcharge
- Discount
```

Hệ thống phải đảm bảo:

```text
FinalAmount >= 0
```

---

# 14. ALGORITHM 3 — ROOM STATUS TRANSITION

```text
AVAILABLE
    │
    ├── Booking ──→ RESERVED
    │
RESERVED
    │
    └── Check-in ──→ OCCUPIED
                         │
                         └── Check-out ──→ CLEANING
                                             │
                                             └── AVAILABLE
```

Một số trạng thái không được phép chuyển trực tiếp nếu vi phạm nghiệp vụ.

---

# 15. UI/UX DESIGN SYSTEM

## 15.1. Overall Style

Phong cách:

**Modern Hotel Management Dashboard**

Nguyên tắc:

- Clean.
- Professional.
- Consistent.
- Information-first.
- Không sử dụng quá nhiều animation.

## 15.2. Layout

Main window:

```text
┌─────────────────────────────────────────────────────────┐
│ Logo             Hotel Management          User ▼       │
├───────────────┬─────────────────────────────────────────┤
│ Dashboard     │                                         │
│               │                 Content                 │
│ Bookings      │                                         │
│               │                                         │
│ Customers     │                                         │
│               │                                         │
│ Rooms         │                                         │
│               │                                         │
│ Payments      │                                         │
│               │                                         │
│ Reports       │                                         │
└───────────────┴─────────────────────────────────────────┘
```

## 15.3. Design Rules

Tất cả form:

- Label thẳng hàng.
- TextBox cùng chiều rộng trong cùng nhóm.
- Button cùng style.
- Table có header rõ.
- Padding nhất quán.
- Error message rõ nghĩa.
- Delete luôn yêu cầu confirmation.

## 15.4. Color Roles

Không cần khóa exact hex ở v1.0; nhóm nên chọn **1 màu primary + neutral + semantic colors**.

Semantic:

```text
Success → Green
Warning → Yellow/Orange
Danger  → Red
Info    → Blue
```

---

# 16. SCREENS

## Screen 01 — Login

Elements:

```text
Username
Password
Login
Remember me
Error message
```

---

## Screen 02 — Dashboard

Cards:

```text
Total Rooms
Available
Occupied
Today's Bookings
Today's Check-in
Today's Check-out
Revenue
```

---

## Screen 03 — Customer Management

```text
Search
Add
Edit
Deactivate
Refresh

DataGridView:
ID
Name
Phone
Email
Identity
```

---

## Screen 04 — Room Management

```text
Search
Filter by status
Filter by room type
Add
Edit
Update status
```

Table:

```text
Room Number
Room Type
Capacity
Price
Floor
Status
```

---

## Screen 05 — Room Type Management

```text
Type Name
Capacity
Price
Description
```

---

## Screen 06 — Booking

Đây là màn hình quan trọng nhất.

Layout:

```text
Customer
[ Search Customer ]

[ Check-in Date ]

[ Check-out Date ]

[ Search Available Rooms ]

Available Rooms
────────────────────
Room 101
Room 102
Room 205

Selected Rooms
────────────────────

Price Summary
────────────────────

Subtotal
Discount
Surcharge
Total

[ Confirm Booking ]
```

---

## Screen 07 — Check-in

```text
Search Booking
      ↓
Booking information
      ↓
Guest information
      ↓
Room information
      ↓
[ Confirm Check-in ]
```

---

## Screen 08 — Check-out / Invoice

```text
Booking
Customer
Room
Check-in
Check-out
Number of nights

Room cost
Service cost
Surcharge
Discount
Total

Payment method

[ Complete Payment ]
[ Generate Invoice ]
```

---

## Screen 09 — Reports

Có thể gồm:

```text
Revenue by date
Booking count
Occupancy
Room status
```

Nếu không đủ thời gian, chỉ cần báo cáo dạng bảng.

---

# 17. TEAM STRUCTURE

## MEMBER 01 — LEADER

### Owner

- Architecture.
- GitHub.
- Authentication.
- Dashboard.
- Integration.
- Database coordination.
- Code review.
- Report integration.
- Demo.

### Expected contribution

Khoảng 25%.

---

## MEMBER 02 — CUSTOMER & ROOM

### Owner

- Customer.
- Room Type.
- Room.

### Deliverables

- Forms.
- CRUD.
- Validation.
- Search/filter.
- Repository.
- Service.

### Expected contribution

Khoảng 25%.

---

## MEMBER 03 — BOOKING

### Owner

- Booking.
- Booking Detail.
- Room availability.
- Booking conflict detection.

### Deliverables

- Booking UI.
- Availability logic.
- Validation.
- Repository.
- Service.
- Tests.

### Expected contribution

Khoảng 25%.

---

## MEMBER 04 — CHECK-IN / CHECK-OUT / PAYMENT

### Owner

- Check-in.
- Check-out.
- Payment.
- Invoice.
- Basic report.

### Deliverables

- Forms.
- Services.
- Calculation.
- State transitions.
- Tests.

### Expected contribution

Khoảng 25%.

---

# 18. IMPORTANT TEAM RULE

Phân chia 25% chỉ là **khung trách nhiệm**, không phải con số dùng để "chia điểm" một cách máy móc.

Thực tế contribution sẽ được chứng minh bằng:

```text
Tasks
+
Issues
+
Commits
+
Pull Requests
+
Code
+
Report
+
Presentation
```

---

# 19. GITHUB WORKFLOW

## Branches

```text
main
develop

feature/auth
feature/customer
feature/room
feature/booking
feature/checkin
feature/payment
feature/dashboard
```

## Flow

```text
Issue
 ↓
Feature branch
 ↓
Code
 ↓
Commit
 ↓
Push
 ↓
Pull Request
 ↓
Review
 ↓
Merge develop
 ↓
Release main
```

---

# 20. COMMIT CONVENTION

Sử dụng:

```text
feat:
fix:
refactor:
docs:
test:
chore:
```

Ví dụ:

```text
feat: add customer management form
feat: implement room availability checking
feat: add booking creation workflow
fix: prevent overlapping bookings
fix: validate invalid checkout date
refactor: move booking logic to service layer
test: add booking validation tests
docs: update database setup guide
```

Không dùng commit:

```text
update
abc
test
final
final2
fix fix
```

---

# 21. ISSUE NAMING

Format:

```text
[MODULE] Task description
```

Ví dụ:

```text
[AUTH] Implement login
[CUSTOMER] Create customer form
[ROOM] Implement room CRUD
[BOOKING] Implement availability checking
[BOOKING] Prevent duplicate booking
[PAYMENT] Implement invoice calculation
[UI] Standardize form layout
[DOCS] Write installation guide
```

---

# 22. GITHUB PROJECT BOARD

Columns:

```text
BACKLOG
   ↓
TODO
   ↓
IN PROGRESS
   ↓
CODE REVIEW
   ↓
TESTING
   ↓
DONE
```

Mỗi task cần:

```text
Title
Owner
Priority
Deadline
Module
Status
```

---

# 23. BACKLOG V1.0

## A. Planning — 8 tasks

```text
P01 Define problem statement
P02 Define target users
P03 Define system scope
P04 Define functional requirements
P05 Define non-functional requirements
P06 Draw use case diagram
P07 Define user flows
P08 Approve project blueprint
```

---

## B. Architecture — 7 tasks

```text
A01 Create solution
A02 Define project layers
A03 Define Models
A04 Define repository interfaces
A05 Define service interfaces
A06 Setup dependency flow
A07 Create architecture diagram
```

---

## C. Database — 7 tasks

```text
DB01 Create database
DB02 Create Users table
DB03 Create Customers table
DB04 Create RoomTypes table
DB05 Create Rooms table
DB06 Create Bookings/Details/Payments
DB07 Create seed data
```

---

## D. Authentication — 4 tasks

```text
AUTH01 Login form
AUTH02 Authentication service
AUTH03 Password handling
AUTH04 Logout
```

---

## E. Customer — 5 tasks

```text
CUS01 Customer form
CUS02 Customer CRUD
CUS03 Customer validation
CUS04 Customer search
CUS05 Booking history
```

---

## F. Room — 6 tasks

```text
ROOM01 Room type form
ROOM02 Room CRUD
ROOM03 Room status
ROOM04 Room search
ROOM05 Room filtering
ROOM06 Room validation
```

---

## G. Booking — 8 tasks

```text
BOOK01 Booking form
BOOK02 Customer selection
BOOK03 Date validation
BOOK04 Available room query
BOOK05 Overlap detection
BOOK06 Booking creation
BOOK07 Booking cancellation
BOOK08 Price estimation
```

---

## H. Check-in / Check-out — 7 tasks

```text
CI01 Check-in form
CI02 Find booking
CI03 Validate check-in
CI04 Update room status
CO01 Check-out form
CO02 Calculate final bill
CO03 Update booking/room status
```

---

## I. Payment — 5 tasks

```text
PAY01 Payment form
PAY02 Payment validation
PAY03 Payment creation
PAY04 Invoice generation
PAY05 Payment history
```

---

## J. Dashboard / Report — 5 tasks

```text
DASH01 Dashboard layout
DASH02 Room statistics
DASH03 Booking statistics
DASH04 Revenue statistics
DASH05 Report screen
```

---

## K. UI/UX — 6 tasks

```text
UI01 Design system
UI02 Main navigation
UI03 Standardize buttons
UI04 Standardize forms
UI05 Standardize DataGridView
UI06 Error/success notifications
```

---

## L. Testing / Release — 8 tasks

```text
TEST01 Customer tests
TEST02 Room tests
TEST03 Booking tests
TEST04 Check-in tests
TEST05 Payment tests
TEST06 Integration testing
TEST07 Bug fixing
TEST08 Final regression testing
```

Total backlog:

**76 tasks nhỏ**

Nhóm không nhất thiết phải làm 76 task riêng thành 76 commit. Có thể gom các task nhỏ thành milestone hợp lý.

---

# 24. DEFINITION OF DONE

Một task chỉ được xem là DONE khi:

```text
☐ Code complete
☐ Database works
☐ Validation exists
☐ UI follows design system
☐ Error handling exists
☐ Tested locally
☐ No obvious crash
☐ Commit created
☐ Push to GitHub
☐ Pull Request created
☐ Reviewed
☐ Merged
```

---

# 25. TIMELINE

## PHASE 1 — 01/10 → 05/10

### Design Freeze

Deliverables:

```text
☐ Project Blueprint
☐ Scope
☐ Use Case
☐ User Flow
☐ ERD
☐ Architecture
☐ Wireframes
☐ GitHub
☐ Task board
☐ Team assignments
```

### GATE 1

Không bắt đầu feature lớn nếu:

- Chưa có ERD.
- Chưa có architecture.
- Chưa chia module.
- Chưa có Git workflow.

---

# PHASE 2 — 06/10 → 12/10

## Foundation

Deliverables:

```text
☐ Solution
☐ Project layers
☐ SQL Server database
☐ Connection
☐ Models
☐ Base repositories
☐ Login
☐ Main Form
☐ Navigation
```

### GATE 2

Chạy được:

```text
Login
 ↓
Dashboard
 ↓
Navigation
 ↓
Database
```

---

# PHASE 3 — 13/10 → 19/10

## Core Management

Deliverables:

```text
☐ Customer
☐ Room Type
☐ Room
☐ Booking
☐ Availability
☐ Booking conflict detection
```

### GATE 3

Luồng sau phải chạy:

```text
Customer
 ↓
Search room
 ↓
Book room
 ↓
Booking saved
```

---

# PHASE 4 — 20/10 → 26/10

## Hotel Operations

Deliverables:

```text
☐ Check-in
☐ Check-out
☐ Payment
☐ Invoice
☐ Room state transition
☐ Dashboard basic statistics
```

### GATE 4

Phải hoàn thành:

```text
Booking
 ↓
Check-in
 ↓
Occupied
 ↓
Check-out
 ↓
Payment
 ↓
Invoice
 ↓
Cleaning
 ↓
Available
```

---

# PHASE 5 — 27/10 → 02/11

## UI/UX + Integration

Deliverables:

```text
☐ Standardized UI
☐ Dashboard polishing
☐ Table formatting
☐ Validation
☐ Notifications
☐ Search/filter
☐ Final navigation
```

### GATE 5

Người mới sử dụng hệ thống phải có thể hiểu cách dùng mà không cần developer hướng dẫn từng control.

---

# PHASE 6 — 03/11 → 09/11

## Testing & Stabilization

Deliverables:

```text
☐ Functional testing
☐ Integration testing
☐ Invalid input testing
☐ Booking conflict testing
☐ Payment testing
☐ Bug fixing
☐ Regression testing
```

### GATE 6

Không còn:

```text
Critical bug
Crash ở core workflow
Double booking
Sai tổng tiền
Sai trạng thái phòng
```

---

# PHASE 7 — 10/11 → 15/11

## Finalization

Từ thời điểm này:

**KHÔNG THÊM FEATURE LỚN.**

Chỉ:

```text
☐ Fix bugs
☐ UI polishing
☐ README
☐ Report
☐ Screenshots
☐ Presentation
☐ Demo script
☐ Final GitHub cleanup
```

---

# 26. REPORT STRUCTURE

## Chapter 1 — Introduction

```text
1.1 Problem
1.2 Motivation
1.3 Objectives
1.4 Target Users
1.5 Scope
```

## Chapter 2 — Requirement Analysis

```text
2.1 Functional requirements
2.2 Non-functional requirements
2.3 Use case
2.4 User flow
```

## Chapter 3 — System Design

```text
3.1 Architecture
3.2 Project structure
3.3 Database
3.4 ERD
```

## Chapter 4 — Implementation

```text
4.1 Authentication
4.2 Customer
4.3 Room
4.4 Booking
4.5 Check-in
4.6 Check-out
4.7 Payment
4.8 Dashboard
```

## Chapter 5 — Algorithms

```text
5.1 Availability checking
5.2 Booking conflict
5.3 Price calculation
5.4 Room state transition
```

## Chapter 6 — Testing

```text
6.1 Test strategy
6.2 Test cases
6.3 Results
6.4 Bug fixes
```

## Chapter 7 — Conclusion

```text
7.1 Achievements
7.2 Limitations
7.3 Future development
```

## Appendix

```text
Screenshots
Database
GitHub
Team contribution
```

---

# 27. PRESENTATION — 6 SLIDES

## Slide 1 — Problem

```text
Hotel management problem
↓
Booking conflicts
↓
Room tracking
↓
Billing
```

## Slide 2 — Solution & Workflow

```text
Customer
→ Booking
→ Check-in
→ Stay
→ Check-out
→ Payment
```

## Slide 3 — Architecture

```text
WinForms
↓
Business
↓
Data
↓
SQL Server
```

## Slide 4 — UI / Core Features

Screenshots:

```text
Dashboard
Booking
Room
Check-in
Check-out
```

## Slide 5 — Technical Highlights

```text
Room availability
Booking conflict detection
Price calculation
State management
Layered architecture
```

## Slide 6 — Result / Contribution

```text
Functional
Usable
Maintainable
GitHub
Team contribution
```

---

# 28. DEMO SCRIPT

Demo không nên click lung tung.

Script chuẩn:

```text
01 Login
02 Dashboard
03 Create/Search customer
04 Open booking
05 Select date
06 Find available room
07 Create booking
08 Check-in
09 Show room = OCCUPIED
10 Check-out
11 Calculate bill
12 Payment
13 Generate invoice
14 Show room = CLEANING
15 Finish
```

Nếu có sự cố, phải có demo dataset dự phòng.

---

# 29. TEST DATA

Nên chuẩn bị sẵn:

```text
20 Customers
20–30 Rooms
4–5 Room Types
10–20 Bookings
5–10 Payments
```

Trong đó cần cố tình có:

```text
Available room
Reserved room
Occupied room
Cleaning room
Maintenance room

Upcoming booking
Current booking
Completed booking
Cancelled booking
```

---

# 30. README REQUIREMENTS

README phải có:

```text
# Hotel & Booking Management

## Overview

## Features

## Technology Stack

## Architecture

## Database

## Project Structure

## Requirements

## Installation

## Database Setup

## Configuration

## Demo Account

## Screenshots

## Team Members

## GitHub Workflow
```

Không commit credential thật.

---

# 31. FINAL QUALITY CHECKLIST

## Functional

```text
☐ Login
☐ Customer CRUD
☐ Room CRUD
☐ Booking
☐ Availability
☐ Check-in
☐ Check-out
☐ Payment
☐ Invoice
☐ Dashboard
```

## UI/UX

```text
☐ Alignment
☐ Consistent spacing
☐ Consistent buttons
☐ Clear typography
☐ Clear validation
☐ Confirmation dialogs
☐ Navigation consistency
☐ No unnecessary decoration
```

## Technical

```text
☐ Layered architecture
☐ OOP
☐ Repository/service separation
☐ Validation
☐ Exception handling
☐ SQL Server
☐ Business logic
☐ Algorithms documented
```

## GitHub

```text
☐ Public repository for report/demo phase
☐ Meaningful commits
☐ Branches
☐ Pull requests
☐ README
☐ No secrets
☐ Contribution traceable
```

## Documentation

```text
☐ Report
☐ ERD
☐ Architecture diagram
☐ Use case
☐ Screenshots
☐ Test cases
☐ Team contribution
```

---

# 32. PROJECT SUCCESS CRITERIA

Đồ án v1.0 được coi là hoàn thành khi đạt 5 điều kiện:

### S1 — Functional

Core workflow chạy end-to-end:

```text
Booking → Check-in → Check-out → Payment
```

### S2 — UX

Người dùng có thể thực hiện nghiệp vụ mà không bị rối.

### S3 — Technical

Source code có architecture rõ ràng.

### S4 — Evidence

GitHub thể hiện quá trình đóng góp thực tế.

### S5 — Presentation

Nhóm giải thích được:

```text
Why?
What?
How?
Algorithm?
Architecture?
Database?
Contribution?
```

---

# 33. LEADER OPERATING RULES

Leader phải giữ 6 nguyên tắc:

## Rule 1

Không thêm feature nếu core chưa ổn.

## Rule 2

Không merge code chưa review.

## Rule 3

Không để một thành viên giữ toàn bộ kiến thức của một module mà không ai khác hiểu.

## Rule 4

Mỗi tuần phải có một bản build chạy được.

## Rule 5

Report phải được viết song song với implementation.

## Rule 6

Từ 10/11 trở đi ưu tiên stability, không ưu tiên feature mới.

---

# 34. WEEKLY REVIEW

Mỗi cuối tuần nhóm họp khoảng 30–45 phút.

Agenda:

```text
1. Tuần này đã hoàn thành gì?
2. Task nào đang blocked?
3. Có bug nào?
4. Có thay đổi scope không?
5. Tuần sau làm gì?
6. GitHub có phản ánh đúng contribution không?
7. Report đã cập nhật chưa?
```

Cuối buổi phải có:

```text
DONE
BLOCKED
NEXT WEEK
```

---

# 35. FINAL PROJECT POSITIONING

Tên dự án:

**Hotel & Booking Management System**

Một câu mô tả:

> A desktop hotel management application developed with C# WinForms and SQL Server to manage customers, rooms, bookings, check-in/check-out and payment workflows through a structured and user-friendly interface.

Thông điệp khi báo cáo:

> **Nhóm không tập trung vào số lượng chức năng, mà tập trung xây dựng một quy trình quản lý khách sạn hoàn chỉnh, nhất quán và có thể sử dụng thực tế ở quy mô nhỏ.**

---

# 36. VERSION CONTROL OF THIS BLUEPRINT

**Version:** v1.0  
**Date:** 01/10/2026

Changes after v1.0 should be classified:

```text
Minor
→ UI adjustment
→ text
→ small validation

Moderate
→ additional report
→ minor feature

Major
→ new module
→ database change
→ architecture change
```

Major changes must be reviewed by all 4 members before implementation.

---

# 37. IMMEDIATE ACTIONS — 01/10/2026

Trong buổi họp đầu tiên, nhóm chỉ cần hoàn thành 10 việc sau:

```text
[01] Chốt tên project
[02] Chốt scope
[03] Chốt 4 thành viên + owner
[04] Tạo GitHub repository
[05] Tạo GitHub Project
[06] Chốt architecture
[07] Vẽ ERD
[08] Vẽ 8–9 wireframe màn hình
[09] Tạo solution C#
[10] Tạo milestone G1
```

Khi 10 mục này hoàn thành, nhóm mới bắt đầu implementation.

---

# 38. ONE-PAGE PROJECT MAP

```text
                  HOTEL & BOOKING MANAGEMENT
                              │
             ┌────────────────┼────────────────┐
             │                │                │
          Customer          Room            Booking
             │                │                │
             └────────────────┼────────────────┘
                              │
                         Check-in
                              │
                         Occupied
                              │
                        Check-out
                              │
                           Payment
                              │
                           Invoice
                              │
                          Cleaning
                              │
                          Available

TECHNICAL FOUNDATION
──────────────────────────────────────
C# + WinForms
Layered Architecture
SQL Server
Repository + Service
GitHub
Testing

DOCUMENTATION
──────────────────────────────────────
ERD
Use Case
Architecture
Report
Screenshots
README
Presentation
```

---

# 39. DEFINITION OF PROJECT DONE

Project v1.0 is complete when:

```text
CORE WORKFLOW       ████████████████████ 100%
DATABASE            ████████████████████ 100%
ARCHITECTURE        ████████████████████ 100%
UI/UX               ████████████████████ 100%
TESTING             ████████████████████ 100%
GITHUB              ████████████████████ 100%
REPORT              ████████████████████ 100%
PRESENTATION        ████████████████████ 100%
DEMO                ████████████████████ 100%
```

**Final priority order:**

```text
1. Correctness
2. Core workflow
3. Data integrity
4. UI/UX consistency
5. Architecture
6. Testing
7. Documentation
8. Nice-to-have features
```
