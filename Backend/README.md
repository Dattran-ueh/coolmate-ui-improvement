# Sổ thu chi

Web quản lý khoản thu và khoản chi bằng đồng Việt Nam. Giao diện có ba thẻ thống kê ở trên, biểu mẫu thêm hoặc sửa giao dịch và danh sách giao dịch. Trên màn hình nhỏ, biểu mẫu và danh sách xếp dọc.

## Cách chạy

1. Mở Terminal hoặc PowerShell trong thư mục `Backend`, nơi có tệp `app.py`.
2. Dùng Python 3.10 trở lên. Chạy `py app.py` trên Windows hoặc `python3 app.py` trên macOS/Linux. Nếu Windows nhận lệnh `python`, có thể dùng `python app.py`.
3. Mở [http://127.0.0.1:8000](http://127.0.0.1:8000) trên trình duyệt. Giữ Terminal mở trong lúc sử dụng và nhấn `Ctrl+C` để dừng.

Ứng dụng dùng thư viện có sẵn của Python, không cần cài thêm gói. Nếu cổng 8000 đang bận, thêm `--port 8001` vào lệnh chạy và mở [http://127.0.0.1:8001](http://127.0.0.1:8001).

## Thử các chức năng

1. Thêm khoản **Thu** 200.000 đồng ở trạng thái **Đã hoàn tất**.
2. Thêm khoản **Chi** 50.000 đồng ở trạng thái **Chờ thực hiện**. Số dư vẫn là 200.000 đồng.
3. Bấm nút **Chờ thực hiện ↔** của khoản chi. Tổng chi thành 50.000 đồng và số dư còn 150.000 đồng.
4. Bấm **Sửa**, đổi tiền chi thành 60.000 đồng và lưu. Số dư còn 140.000 đồng.
5. Bấm **Xóa** khoản chi và xác nhận. Số dư trở lại 200.000 đồng.
6. Dừng rồi chạy lại ứng dụng. Khoản thu đã thêm vẫn còn.

Thống kê tính trên toàn bộ giao dịch đã hoàn tất. Bộ lọc trạng thái chỉ áp dụng cho danh sách giao dịch.

## Mã nguồn và cơ sở dữ liệu

| Tệp | Vai trò |
| --- | --- |
| `app.py` | Chạy máy chủ, kiểm tra dữ liệu, xử lý thao tác và tính thống kê |
| `static/index.html`, `static/style.css` | Giao diện web |
| `static/app.js` | Hộp thoại xác nhận xóa |
| `data/schema.sql` | Cấu trúc bảng `transactions` |
| `data/so-thu-chi.sqlite3` | Cơ sở dữ liệu SQLite, được tạo khi chạy lần đầu |

Dữ liệu lưu vào SQLite sau mỗi lần thêm, sửa, xóa hoặc đổi trạng thái. Máy chủ tự khởi tạo cơ sở dữ liệu nếu tệp SQLite chưa tồn tại.

## Kiểm thử và minh chứng

Xem [kết quả kiểm thử](KIEM-THU.md) và các [ảnh chụp từ trình duyệt](minh-chung/).
