# Design System — Hotel & Booking Management
**Phiên bản:** 1.0  
**Nền tảng mục tiêu:** C# WinForms · .NET 10 · Windows Desktop  
**Ngôn ngữ giao diện:** Tiếng Việt  
**Đối tượng sử dụng:** Quản lý khách sạn, lễ tân

---

## 1. Mục tiêu thiết kế

- Ưu tiên tốc độ thao tác và độ chính xác cho nghiệp vụ lễ tân.
- Trạng thái phòng, đặt phòng và thanh toán phải dễ nhận biết, không chỉ dựa vào màu sắc.
- Giao diện nhất quán giữa Dashboard, Phòng, Đặt phòng, Khách hàng, Nhận/Trả phòng, Thanh toán và Báo cáo.
- Dùng các control WinForms tiêu chuẩn, dễ triển khai và bảo trì.
- Các thao tác có ảnh hưởng dữ liệu phải có xác nhận hoặc thông báo kết quả rõ ràng.

## 2. Hướng mỹ thuật

**Phong cách:** Hospitality Admin — hiện đại, tin cậy, gọn gàng.  
**Tông chính:** xanh navy cho cấu trúc và độ tin cậy; xanh teal cho hành động chính; nền xám xanh rất nhạt để giảm mỏi mắt.  
**Nguyên tắc:** nền trung tính, một màu nhấn chính, màu trạng thái chỉ dùng cho trạng thái nghiệp vụ.

## 3. Design tokens

### 3.1 Màu sắc

| Token | HEX | Công dụng |
|---|---|---|
| `Color.Brand.900` | `#15324B` | Sidebar, tiêu đề quan trọng |
| `Color.Brand.700` | `#205574` | Hover/active đậm, liên kết |
| `Color.Primary.600` | `#087E8B` | Nút chính, lựa chọn đang hoạt động |
| `Color.Primary.700` | `#066873` | Hover nút chính |
| `Color.Primary.100` | `#DDF3F3` | Nền nhấn nhẹ |
| `Color.Background` | `#F4F7FA` | Nền ứng dụng |
| `Color.Surface` | `#FFFFFF` | Panel, card, form |
| `Color.Border` | `#DCE4EA` | Viền control, đường phân cách |
| `Color.Text.Primary` | `#172B3A` | Nội dung chính |
| `Color.Text.Secondary` | `#5B6B78` | Mô tả, nhãn phụ |
| `Color.Text.Disabled` | `#91A0AA` | Nội dung vô hiệu hóa |
| `Color.Success` | `#21845B` | Thành công, phòng trống |
| `Color.Success.Bg` | `#E5F5EC` | Nền trạng thái thành công |
| `Color.Warning` | `#A86608` | Chờ xác nhận, sắp đến hạn |
| `Color.Warning.Bg` | `#FFF3D8` | Nền cảnh báo |
| `Color.Danger` | `#B93838` | Lỗi, hủy, quá hạn |
| `Color.Danger.Bg` | `#FCE9E9` | Nền lỗi |
| `Color.Info` | `#2864A5` | Thông tin, đã đặt |
| `Color.Info.Bg` | `#E8F1FC` | Nền thông tin |
| `Color.Neutral.Bg` | `#EDF1F4` | Trạng thái trung tính |

**Quy tắc:** Không dùng màu đỏ/xanh làm tín hiệu duy nhất. Luôn hiển thị nhãn chữ đi kèm, ví dụ `● Đang sử dụng`.

### 3.2 Typography

Ưu tiên font có sẵn trên Windows để tránh lỗi đóng gói:

| Token | Font | Cỡ gợi ý | Dùng cho |
|---|---|---:|---|
| `Type.PageTitle` | Segoe UI Semibold | 20 pt | Tiêu đề màn hình |
| `Type.SectionTitle` | Segoe UI Semibold | 12 pt | Tiêu đề nhóm/card |
| `Type.Body` | Segoe UI | 10 pt | Nội dung và bảng |
| `Type.Label` | Segoe UI | 9 pt | Nhãn trường |
| `Type.Caption` | Segoe UI | 8.5 pt | Gợi ý, metadata |
| `Type.Button` | Segoe UI Semibold | 9.5 pt | Nút bấm |
| `Type.KPI` | Segoe UI Semibold | 22–26 pt | Số liệu dashboard |

Không dùng quá 3 cấp cỡ chữ trong cùng một vùng nội dung. Tránh chữ in hoa toàn bộ cho tiêu đề dài.

### 3.3 Spacing, kích thước và hình dạng

Dùng thang khoảng cách theo bội số 4:

| Token | Giá trị | Dùng cho |
|---|---:|---|
| `Space.1` | 4 px | Khoảng cách rất nhỏ |
| `Space.2` | 8 px | Giữa icon và nhãn |
| `Space.3` | 12 px | Khoảng cách trong form |
| `Space.4` | 16 px | Padding card |
| `Space.6` | 24 px | Giữa các nhóm |
| `Space.8` | 32 px | Phân vùng lớn |
| `Control.Height.SM` | 30 px | Nút phụ, ô lọc |
| `Control.Height.MD` | 36 px | TextBox, ComboBox, nút chính |
| `Control.Height.LG` | 42 px | Hành động nổi bật |
| `Radius.SM` | 4 px | TextBox, badge |
| `Radius.MD` | 8 px | Card, panel |
| `Border.Width` | 1 px | Viền tiêu chuẩn |

WinForms không hỗ trợ bo góc đồng nhất trên mọi control theo mặc định. Nếu không có custom control, dùng panel chữ nhật, viền mảnh và khoảng trắng nhất quán thay vì giả lập bo góc thiếu ổn định.

## 4. Khung bố cục ứng dụng

### 4.1 Khung desktop

- Kích thước thiết kế tham chiếu: `1440 × 900`.
- Sidebar trái: `220–240 px`, màu `Brand.900`.
- Thanh tiêu đề nội dung: `64–72 px`.
- Padding vùng nội dung: `24 px`.
- Khoảng cách giữa card: `16 px`.
- Vùng nội dung chính dùng `Panel`/`TableLayoutPanel`; bảng dữ liệu chiếm phần chiều cao linh hoạt.
- Hỗ trợ DPI scaling; tránh đặt vị trí tuyệt đối cho toàn bộ control. Dùng `Dock`, `Anchor`, `TableLayoutPanel` và `FlowLayoutPanel`.

### 4.2 Điều hướng đề xuất

1. Tổng quan
2. Đặt phòng
3. Phòng
4. Khách hàng
5. Nhận phòng / Trả phòng
6. Thanh toán & Hóa đơn
7. Báo cáo
8. Người dùng & Phân quyền (chỉ quản lý)
9. Cài đặt

Menu đang chọn dùng nền `#205574`, vạch nhấn `#19B6B2` và chữ trắng. Mục không khả dụng phải có trạng thái disabled rõ ràng.

### 4.3 Cấu trúc màn hình chuẩn

Mỗi màn hình quản lý nên có:
- Tiêu đề và mô tả ngắn (nếu cần).
- Một hàng hành động chính: `+ Thêm mới`, xuất dữ liệu hoặc thao tác nghiệp vụ.
- Hàng tìm kiếm/lọc.
- Bảng hoặc lưới dữ liệu.
- Phân trang hoặc tổng số bản ghi nếu dữ liệu dài.
- Vùng trạng thái rỗng, đang tải, lỗi và không có kết quả.

## 5. Component library

### 5.1 Buttons

| Variant | Màu | Dùng khi |
|---|---|---|
| Primary | Nền `Primary.600`, chữ trắng | Lưu, tạo đặt phòng, xác nhận nghiệp vụ |
| Secondary | Nền trắng, viền `Border`, chữ `Text.Primary` | Hủy form, quay lại, xem chi tiết |
| Tertiary | Không nền, chữ `Primary.600` | Tác vụ phụ trong hàng |
| Danger | Nền `Danger`, chữ trắng | Xóa/hủy không thể hoàn tác |
| Disabled | Nền `#E7ECEF`, chữ `#91A0AA` | Không thể thao tác |

Quy tắc:
- Nhãn bắt đầu bằng động từ: `Lưu thay đổi`, `Tạo đặt phòng`, `Xác nhận trả phòng`.
- Không đặt hai nút Primary cạnh nhau.
- Nút hủy/xóa phải tách khỏi nút lưu và có xác nhận khi thao tác không thể hoàn tác.
- Nút có trạng thái: mặc định, hover, pressed, disabled và loading.

### 5.2 TextBox

- Chiều cao tiêu chuẩn: `36 px`.
- Nhãn nằm phía trên, không chỉ dùng placeholder làm nhãn.
- Placeholder là ví dụ ngắn, ví dụ `Nhập mã đặt phòng`.
- Lỗi hiển thị ngay dưới trường, dùng chữ `Danger` và thông điệp cụ thể.
- Với trường bắt buộc, dùng dấu `*` và chú giải `(*) Trường bắt buộc`.
- CCCD, số điện thoại, mã phòng phải được lưu/hiển thị như chuỗi nếu có thể chứa số 0 ở đầu.

### 5.3 ComboBox / DateTimePicker / Numeric input

- ComboBox dùng khi lựa chọn từ danh mục cố định: loại phòng, phương thức thanh toán, trạng thái.
- DateTimePicker dùng cho ngày đến/ngày đi; ngày đi phải sau ngày đến.
- Số lượng, số tiền không chấp nhận giá trị âm.
- Tiền tệ hiển thị theo định dạng Việt Nam, ví dụ `1.250.000 ₫`; thống nhất một định dạng trong toàn hệ thống.
- Không tự đổi dữ liệu người dùng khi chưa thông báo.

### 5.4 DataGridView

- Header nền `#EAF0F4`, chữ `#263B4A`, font Semibold.
- Chiều cao hàng gợi ý: `40–44 px`.
- Chọn một hàng: nền `#DDF3F3`; vẫn đảm bảo độ tương phản.
- Căn trái: tên, email, địa chỉ. Căn giữa: mã, trạng thái. Căn phải: số lượng, đơn giá, tổng tiền.
- Không nhồi quá nhiều cột; đưa thao tác phụ vào menu/hàng chi tiết.
- Có sắp xếp, tìm kiếm/lọc, thông báo không có dữ liệu và xử lý lỗi tải.
- Trước thao tác xóa/hủy, yêu cầu xác nhận hoặc dùng luồng nghiệp vụ có thể khôi phục.

### 5.5 Cards / KPI

Card gồm nhãn nhỏ, giá trị chính, mô tả thời gian hoặc thay đổi và icon tùy chọn. Không dùng quá nhiều màu nền khác nhau.

KPI dashboard gợi ý:
- Tổng số phòng
- Phòng đang sử dụng
- Phòng trống
- Đặt phòng hôm nay
- Doanh thu trong kỳ

Số liệu phải có đơn vị và mốc thời gian rõ ràng.

### 5.6 Dialog / Toast

- Dialog chỉ dùng cho quyết định quan trọng hoặc form ngắn.
- Tiêu đề mô tả hành động, ví dụ `Hủy đặt phòng?`.
- Nội dung nêu ảnh hưởng của thao tác.
- Nút chính thể hiện quyết định: `Xác nhận hủy`, không dùng nhãn mơ hồ `OK`.
- Toast thành công ngắn gọn; lỗi phải nêu cách xử lý tiếp theo nếu biết.
- Không chỉ báo lỗi bằng màu đỏ; luôn có nội dung chữ.

## 6. Trạng thái nghiệp vụ

### 6.1 Trạng thái phòng

| Trạng thái | Nhãn tiếng Việt | Màu chữ | Nền |
|---|---|---|---|
| `Available` | Phòng trống | `#21845B` | `#E5F5EC` |
| `Reserved` | Đã đặt | `#2864A5` | `#E8F1FC` |
| `Occupied` | Đang sử dụng | `#A86608` | `#FFF3D8` |
| `Cleaning` | Đang dọn | `#5B6B78` | `#EDF1F4` |
| `Maintenance` | Bảo trì | `#B93838` | `#FCE9E9` |

### 6.2 Trạng thái đặt phòng

| Trạng thái | Nhãn đề xuất | Cách thể hiện |
|---|---|---|
| Pending | Chờ xác nhận | Badge cảnh báo |
| Confirmed | Đã xác nhận | Badge thông tin |
| Checked-in | Đã nhận phòng | Badge thành công/đậm |
| Checked-out | Đã trả phòng | Badge trung tính |
| Cancelled | Đã hủy | Badge lỗi/trung tính |
| No-show | Không đến | Badge lỗi |

Tên trạng thái trên đây là nhãn giao diện đề xuất; cần ánh xạ theo enum/giá trị thực tế của dự án.

### 6.3 Thanh toán

- Chưa thanh toán: `Chưa thanh toán` — cảnh báo.
- Thanh toán một phần: `Thanh toán một phần` — thông tin/cảnh báo.
- Đã thanh toán: `Đã thanh toán` — thành công.
- Hoàn tiền: `Đã hoàn tiền` — trung tính hoặc thông tin.
- Giao dịch lỗi: `Giao dịch thất bại` — lỗi.

## 7. Patterns nghiệp vụ

### 7.1 Tạo đặt phòng
1. Chọn khách hàng hoặc tạo khách hàng mới.
2. Chọn ngày nhận/trả phòng.
3. Kiểm tra phòng trống theo toàn bộ khoảng thời gian.
4. Chọn phòng và hiển thị giá, số đêm, tạm tính.
5. Xác nhận thông tin trước khi lưu.
6. Hiển thị mã đặt phòng sau khi tạo thành công.

Không cho phép đặt phòng nếu ngày trả không sau ngày nhận hoặc phòng bị trùng lịch.

### 7.2 Check-in / Check-out
- Hiển thị mã đặt phòng, khách, phòng và thời gian dự kiến.
- Kiểm tra điều kiện nghiệp vụ trước khi xác nhận.
- Check-out cần hiển thị tạm tính, phụ phí (nếu có) và số tiền còn phải thanh toán.
- Chỉ chuyển phòng sang `Cleaning` khi quy trình trả phòng thành công theo quy tắc nghiệp vụ.

### 7.3 Thanh toán
- Tách rõ tổng tiền, đã thanh toán, giảm giá/phụ phí và còn phải trả.
- Hiển thị phương thức thanh toán và mã giao dịch nếu có.
- Không báo thành công trước khi lưu giao dịch thành công.
- Hóa đơn cần có thao tác xem/in/xuất theo phạm vi chức năng hiện thực.

### 7.4 Xóa và hủy
- Phân biệt `Xóa` dữ liệu và `Hủy` nghiệp vụ.
- Ưu tiên hủy/đánh dấu trạng thái thay vì xóa lịch sử giao dịch.
- Thông báo rõ nếu thao tác ảnh hưởng đến đặt phòng, hóa đơn hoặc trạng thái phòng.

## 8. Accessibility & usability

- Độ tương phản chữ/nền hướng tới WCAG AA (tối thiểu 4.5:1 cho chữ thường).
- Tất cả thao tác chính dùng được bằng bàn phím; thứ tự Tab theo luồng nhập liệu.
- Focus indicator phải nhìn thấy rõ.
- Không dùng màu sắc làm tín hiệu duy nhất.
- Nút có tên dễ hiểu; icon-only button cần AccessibleName/tooltip.
- Lỗi validation chỉ rõ trường sai và cách sửa.
- Không xóa dữ liệu người dùng đã nhập khi validation thất bại.

## 9. Quy ước triển khai WinForms

### 9.1 Tạo lớp theme tập trung

Đặt các token ở một nơi, ví dụ `UI/Theme/AppTheme.cs`. Tránh viết mã màu trực tiếp rải rác trong từng Form.

```csharp
using System.Drawing;

namespace HotelBookingManagement.UI.Theme;

public static class AppTheme
{
    public static readonly Color Brand900 = ColorTranslator.FromHtml("#15324B");
    public static readonly Color Brand700 = ColorTranslator.FromHtml("#205574");
    public static readonly Color Primary600 = ColorTranslator.FromHtml("#087E8B");
    public static readonly Color Primary700 = ColorTranslator.FromHtml("#066873");
    public static readonly Color Background = ColorTranslator.FromHtml("#F4F7FA");
    public static readonly Color Surface = ColorTranslator.FromHtml("#FFFFFF");
    public static readonly Color Border = ColorTranslator.FromHtml("#DCE4EA");
    public static readonly Color TextPrimary = ColorTranslator.FromHtml("#172B3A");
    public static readonly Color TextSecondary = ColorTranslator.FromHtml("#5B6B78");
    public static readonly Color Success = ColorTranslator.FromHtml("#21845B");
    public static readonly Color Warning = ColorTranslator.FromHtml("#A86608");
    public static readonly Color Danger = ColorTranslator.FromHtml("#B93838");

    public static readonly Font BodyFont = new("Segoe UI", 10F, FontStyle.Regular);
    public static readonly Font LabelFont = new("Segoe UI", 9F, FontStyle.Regular);
    public static readonly Font SectionFont = new("Segoe UI Semibold", 12F, FontStyle.Regular);
    public static readonly Font PageTitleFont = new("Segoe UI Semibold", 20F, FontStyle.Regular);
}
```

### 9.2 Hàm áp dụng theme cho Form

```csharp
using System.Drawing;
using System.Windows.Forms;

namespace HotelBookingManagement.UI.Theme;

public static class ThemeApplier
{
    public static void Apply(Control root)
    {
        root.BackColor = AppTheme.Background;
        root.ForeColor = AppTheme.TextPrimary;
        root.Font = AppTheme.BodyFont;

        foreach (Control control in root.Controls)
        {
            switch (control)
            {
                case Button button:
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = AppTheme.Border;
                    button.BackColor = AppTheme.Surface;
                    button.ForeColor = AppTheme.TextPrimary;
                    button.Font = new Font("Segoe UI Semibold", 9.5F);
                    button.Height = 36;
                    break;

                case TextBox textBox:
                    textBox.Font = AppTheme.BodyFont;
                    textBox.BackColor = AppTheme.Surface;
                    textBox.ForeColor = AppTheme.TextPrimary;
                    break;

                case Label label:
                    label.ForeColor = AppTheme.TextPrimary;
                    break;

                case DataGridView grid:
                    grid.BackgroundColor = AppTheme.Surface;
                    grid.BorderStyle = BorderStyle.None;
                    grid.EnableHeadersVisualStyles = false;
                    grid.ColumnHeadersDefaultCellStyle.BackColor =
                        ColorTranslator.FromHtml("#EAF0F4");
                    grid.ColumnHeadersDefaultCellStyle.ForeColor =
                        AppTheme.TextPrimary;
                    grid.ColumnHeadersDefaultCellStyle.Font =
                        new Font("Segoe UI Semibold", 9.5F);
                    grid.DefaultCellStyle.Font = AppTheme.BodyFont;
                    grid.DefaultCellStyle.SelectionBackColor =
                        ColorTranslator.FromHtml("#DDF3F3");
                    grid.DefaultCellStyle.SelectionForeColor =
                        AppTheme.TextPrimary;
                    grid.RowTemplate.Height = 42;
                    break;
            }

            if (control.HasChildren)
                ApplyChildren(control);
        }
    }

    private static void ApplyChildren(Control parent)
    {
        foreach (Control child in parent.Controls)
        {
            // Áp dụng đệ quy cho control con mà không đặt lại theme của root.
            if (child is Button button)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = AppTheme.Border;
                button.Font = new Font("Segoe UI Semibold", 9.5F);
                button.Height = 36;
            }
            else if (child is TextBox textBox)
            {
                textBox.Font = AppTheme.BodyFont;
            }
            else if (child is Label label)
            {
                label.Font = AppTheme.LabelFont;
            }

            if (child.HasChildren)
                ApplyChildren(child);
        }
    }
}
```

**Lưu ý triển khai:** Đây là khung khởi đầu, không phải bộ theme hoàn chỉnh cho mọi control. Nên tạo hàm riêng cho `PrimaryButton`, `DangerButton`, badge trạng thái và cấu hình `DataGridView`; tránh gọi `Apply` lặp lại vì có thể tạo nhiều đối tượng Font không cần thiết. Nếu dự án dùng namespace hoặc cấu trúc thư mục khác, điều chỉnh namespace cho phù hợp.

### 9.3 Cấu trúc thư mục đề xuất

```text
src/HotelBookingManagement/
└── UI/
    ├── Theme/
    │   ├── AppTheme.cs
    │   ├── ThemeApplier.cs
    │   └── StatusPalette.cs
    ├── Controls/
    │   ├── PrimaryButton.cs
    │   ├── StatusBadge.cs
    │   └── EmptyStatePanel.cs
    ├── Forms/
    │   ├── DashboardForm.cs
    │   ├── RoomsForm.cs
    │   ├── BookingsForm.cs
    │   ├── CustomersForm.cs
    │   └── PaymentsForm.cs
    └── Resources/
        └── Icons/
```

Tên thư mục trên là cấu trúc khuyến nghị; hãy ghép vào cấu trúc hiện có thay vì tạo bản sao các Form đã tồn tại.

## 10. Checklist nghiệm thu

- [ ] Các Form dùng chung bảng màu, font và khoảng cách.
- [ ] Có phân biệt Primary / Secondary / Danger / Disabled.
- [ ] Các trạng thái phòng và đặt phòng có nhãn chữ rõ ràng.
- [ ] Bảng có trạng thái chọn, tiêu đề cột, căn lề và empty state nhất quán.
- [ ] Tìm kiếm, lọc và validation có phản hồi dễ hiểu.
- [ ] Không mất dữ liệu nhập khi validation thất bại.
- [ ] Các thao tác hủy/xóa có xác nhận phù hợp.
- [ ] Giao diện kiểm tra ở DPI 100%, 125% và kích thước cửa sổ khác nhau.
- [ ] Điều hướng bàn phím và focus được kiểm tra.
- [ ] Các token nằm tập trung, không rải mã màu trong Form.

---

## 11. Phạm vi và giả định

Tài liệu này là đề xuất thiết kế cho ứng dụng desktop quản lý khách sạn, phù hợp với WinForms. Tên trạng thái và luồng nghiệp vụ cần được đối chiếu với enum, database schema và các Form thực tế trong repository trước khi áp dụng vào mã nguồn. Design System không tự thay đổi logic nghiệp vụ hoặc cấu trúc cơ sở dữ liệu.
