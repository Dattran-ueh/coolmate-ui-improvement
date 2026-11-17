# JavaScript Labs – Trần Gia Đạt
MSSV: 31241024727 · Lớp: 26C1INF50902702

## Mục tiêu
Thực hành 29 Exercise theo Lab 08 (11), Lab 09 (8), Lab 10 (10).

## Cấu trúc
- index.html: thông tin sinh viên, liên kết, kiến thức và tự đánh giá.
- Lab08, Lab09, Lab10: index và cặp exerciseNN.html / exerciseNN.js.
- css, js, assets: giao diện, jQuery và hình từ bộ tài nguyên thực hành.
- Lab10/data: JSON mẫu dùng cho HTTP GET.

## Chạy thử
Tại thư mục cha của JavaScript-Labs chạy `python -m http.server 8000`, truy cập http://localhost:8000/JavaScript-Labs/. GET cần HTTP; không mở bằng file://.

## Triển khai
Thêm thư mục JavaScript-Labs vào repository cũ https://github.com/Dattran-ueh/coolmate-ui-improvement. Không xóa bài cũ. Khi GitHub Pages xuất bản từ main, đường dẫn: https://dattran-ueh.github.io/coolmate-ui-improvement/JavaScript-Labs/. Đã triển khai và mở kiểm tra trực tiếp trên GitHub Pages ngày 17/11/2026.

## Nguồn và AI
Đối chiếu tài liệu Lab của Randy Connolly và Ricardo Hoar, Fundamentals of Web Development 2nd ed., 2017. Hình du lịch, cờ và jQuery lấy từ tài nguyên kèm bộ Lab tham khảo do người dùng cung cấp. Mã bài thực hành được biên soạn lại với hỗ trợ của ChatGPT/Codex. Sinh viên cần đọc, hiểu mã nguồn và điều chỉnh phần giới thiệu, tự đánh giá theo trải nghiệm thực tế trước khi nộp.

## Ghi chú kỹ thuật
GET sử dụng JSON cùng nguồn thay cho endpoint HTTP cũ để tránh mixed-content khi chạy GitHub Pages HTTPS. Có nút Test failure để thử fail/always. Exercise 9.6 có debugger; mở DevTools và rê chuột vào khối để dừng tại breakpoint.

## Kiểm tra sản phẩm
Đã đối chiếu 29 trang Exercise, 3 trang Lab (11/8/10 liên kết), kết quả tính tiền, thao tác DOM, trạng thái form trống/đủ dữ liệu, ẩn/hiện, preview ảnh, chuỗi animation, GET thành công và GET thất bại. 31 tệp JS qua node --check; đường dẫn tài nguyên cục bộ tồn tại. Ảnh minh chứng được chụp từ website đã triển khai, đưa vào báo cáo Word riêng. Kiểm tra tắt JavaScript trong cài đặt trình duyệt và thao tác Debugger thủ công không thuộc các kiểm tra tương tác đã thực hiện.
