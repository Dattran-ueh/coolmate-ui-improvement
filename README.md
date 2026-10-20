# E-commerce UI Improvement

## 1. Giới thiệu
Bài tập cá nhân Chương 1 khảo sát Coolmate và xây dựng một phương án thẻ sản phẩm gọn, dễ đọc, thích ứng màn hình bằng HTML5/CSS3. Tên giao diện mẫu là Everyday UI. Đây là thiết kế học tập độc lập, không phải website chính thức của Coolmate.

## 2. Website tham khảo
- Tên website: Coolmate.
- URL: https://www.coolmate.me/
- Khu vực đã khảo sát: trang chủ, danh mục áo thun nam, chi tiết áo Coolmate Basics, tìm kiếm và giỏ hàng.
- Các phiên khảo sát có ChatGPT hỗ trợ: 19–20/10/2026. Phiên ngày 20/10 có ảnh minh chứng năm khu vực.
- Ngày chạy thử và chỉnh sửa giao diện trên máy cá nhân: 20/10/2026, bằng Firefox.

## 3. Chức năng
Hiển thị bốn sản phẩm mẫu với ảnh, tên, giá và thao tác Xem thông tin. Bố cục có bốn cột trên màn hình rộng, hai cột ở cỡ trung, một cột ở màn hình hẹp. details/summary mở và đóng mô tả bằng HTML, không dùng JavaScript. CTA “Khám phá áo thun” ở phần giới thiệu dẫn đến danh sách sản phẩm qua liên kết #products. Bản mẫu không triển khai giỏ hàng hoặc thanh toán.

## 4. Công nghệ
HTML5, CSS3; bốn ảnh SVG tự vẽ bằng mã. Không dùng framework, backend, cơ sở dữ liệu hoặc thư viện ngoài.

## 5. Cách chạy
Giải nén toàn bộ thư mục, mở index.html bằng trình duyệt. Có thể mở thư mục trong VS Code để đọc HTML và css/style.css. Không di chuyển riêng index.html ra khỏi thư mục chứa css/ và images/.

## 6. Cải tiến đã thực hiện
Bản mẫu cụ thể hóa đề xuất đơn giản hóa thẻ: giá bán nổi bật, giá tham chiếu có gạch ngang, nhãn giảm giá bằng chữ, thao tác luôn hiển thị; ở màn hình hẹp chuyển một cột để giữ diện tích đọc. Đây là phương án thử nghiệm, chưa có kiểm thử người dùng chứng minh tốt hơn Coolmate. Tất cả tên, giá, thông số sản phẩm mẫu là giả định. Sau khi chạy thử, em nhận thấy phần giới thiệu cần một CTA chính nổi bật. Em bổ sung liên kết “Khám phá áo thun” với class primary-cta và href="#products" trong index.html, đồng thời thêm CSS cho màu nền, chữ, khoảng cách, chiều cao tối thiểu 48 px và trạng thái hover. Liên kết sử dụng quy tắc focus-visible đã có để hỗ trợ thao tác bàn phím.

Em đã kiểm tra bố cục bằng Firefox Responsive Design Mode: 1366 × 900 px có bốn cột; 768 × 1024 px có hai cột; 390 × 844 px và 320 × 800 px có một cột. Em đã thử CTA bằng chuột và Tab/Enter, mở/đóng bốn phần “Xem thông tin” và kiểm tra không cuộn ngang ở hai kích thước điện thoại. Ảnh minh chứng và kết quả được lưu trong hồ sơ bài tập.

## 7. Hỗ trợ từ AI
ChatGPT hỗ trợ khảo sát phiên trình duyệt, lập bảng phân tích, đề xuất bố cục, tạo mã và giải thích. Đã kiểm tra tĩnh đủ bốn thẻ, ảnh tồn tại và CSS riêng. Em trực tiếp chạy giao diện, phát hiện nhu cầu bổ sung CTA, lưu thay đổi HTML/CSS và gửi mã nguồn cùng ảnh kiểm tra. ChatGPT đối chiếu thay đổi và cập nhật tài liệu theo minh chứng, đồng thời hướng dẫn giải thích các thành phần mã đã sửa. Chi tiết ở AI_USAGE.md.
