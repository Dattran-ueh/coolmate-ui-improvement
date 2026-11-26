# Kết quả kiểm thử Sổ thu chi

Kiểm thử ngày 26/11/2026 trên Chromium với máy chủ Python chạy cục bộ và cơ sở dữ liệu SQLite. Các khoản dưới đây là dữ liệu mẫu. Giao diện desktop dùng khung nhìn 1200 px, giao diện điện thoại dùng khung nhìn 390 px.

| Mã | Thao tác | Kết quả thực tế |
| --- | --- | --- |
| TC01 | Mở sổ trống, thêm khoản thu 200.000 ₫ đã hoàn tất và khoản chi 50.000 ₫ đang chờ. | Hai giao dịch xuất hiện. Tổng thu 200.000 ₫, tổng chi 0 ₫, số dư 200.000 ₫, có 1 giao dịch chờ. |
| TC02 | Lọc Chờ thực hiện rồi đánh dấu khoản chi hoàn tất. | Bộ lọc chỉ hiện khoản đang chờ. Sau cập nhật, danh sách lọc trống, tổng chi 50.000 ₫ và số dư 150.000 ₫. |
| TC03 | Sửa khoản chi từ 50.000 ₫ thành 60.000 ₫. | Danh sách hiển thị 60.000 ₫, tổng chi 60.000 ₫ và số dư 140.000 ₫. |
| TC04 | Bấm Xóa, hủy một lần rồi xác nhận xóa. | Khi hủy, khoản chi vẫn còn. Khi xác nhận, khoản chi biến mất và số dư trở lại 200.000 ₫. |
| TC05 | Gửi số tiền âm tới máy chủ. | Máy chủ trả HTTP 422 và thông báo yêu cầu nhập số tiền nguyên dương. |
| TC06 | Dừng máy chủ, chạy lại rồi tải lại trang. | Khoản thu 200.000 ₫ vẫn hiển thị. SQLite còn đúng một giao dịch và kiểm tra tính toàn vẹn trả về `ok`. |
| TC07 | Mở trang ở chiều rộng 390 px. | Biểu mẫu và danh sách xếp dọc. Trang không cuộn ngang. |

## Ảnh minh chứng

- [Giao dịch mới và thống kê khi còn khoản chờ](minh-chung/01-giao-dich-dang-cho.png)
- [Biểu mẫu chỉnh sửa khoản chi trước khi lưu](minh-chung/02-sua-giao-dich.png)
- [Giao diện 390 px sau khi lưu khoản chi 60.000 ₫](minh-chung/03-giao-dien-390px.png)
- [Giao dịch và số dư sau khi xóa khoản chi rồi khởi động lại](minh-chung/04-du-lieu-sau-khoi-dong-lai.png)
