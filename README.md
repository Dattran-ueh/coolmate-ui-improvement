# Everyday UI Bài tập cá nhân Buổi 2

## 1. Giới thiệu
Giao diện tiếp tục danh sách bốn áo thun từ Buổi 1, thực hành HTML5 và CSS3. Tên, giá và ảnh là dữ liệu minh họa của bài tập, không phải dữ liệu bán hàng của Coolmate.

## 2. Website tham khảo và phạm vi cải tiến
Website tham khảo: https://www.coolmate.me/
Repository: https://github.com/Dattran-ueh/coolmate-ui-improvement
Website: https://dattran-ueh.github.io/coolmate-ui-improvement/

Giữ bốn article và ảnh gốc, sửa bố cục giới thiệu, tỷ lệ khung ảnh, phân cấp thông tin, nhãn mở đóng và nội dung từng mẫu. Bản giao diện trong gói là bản chỉnh mới; tải nội dung gói mã nguồn vào gốc repository để đồng bộ mã và Pages.

## 3. Kỹ thuật HTML5 và CSS3
HTML có header/nav/main/section/article/footer, h1–h3, img/alt, a, ul/li, details/summary. CSS ngoài dùng selector, margin/padding/border, border-box, Flexbox, Grid, media query, hover, focus-visible và transition. Không sử dụng JavaScript hay framework.

## 4. Cách chạy
Mở index.html bằng trình duyệt, giữ css/ và images/ cạnh tệp.

## 5. Thay đổi và lý do
- Intro cũ có vùng đệm 50px/34px, tiêu đề lớn và nhiều đoạn trước danh sách. Intro mới gọn, chia chữ và CTA trên màn hình rộng, đưa lưu ý dữ liệu mẫu xuống footer.
- Ảnh cũ có tỷ lệ 5/6, chiếm nhiều chiều dọc. Khung mới 3/2 trên màn hình rộng và 4/3 trên điện thoại; object-fit:contain giữ trọn ảnh gốc.
- Đặt nhãn giá trên khung ảnh, phân cấp tên/chất liệu/giá và nút thao tác. Đây là thay đổi cách trình bày, không tuyên bố chức năng cũ tính giá sai.
- Bốn mô tả chung được thay bằng danh sách thuộc tính riêng.
- Nhãn đóng “Thông tin & kích cỡ”, nhãn mở “Thu gọn thông tin”; mũi tên đổi theo open. Giữ cơ chế details/summary và viền focus đậm.

## 6. AI và kiểm tra
AI_USAGE.md ghi yêu cầu thực tế, kết quả và phần điều chỉnh theo phản biện người học. Kiểm tra tĩnh đối chiếu thẻ, đường dẫn, alt, id điều hướng và CSS của bản mới. Quy trình chạy, chụp ảnh trước–sau và tự sửa mã được hướng dẫn ở tài liệu bên ngoài website/.

## 7. Cấu trúc tệp
Gói mã nguồn gồm 8 tệp để tải lên repository: index.html; css/style.css; images/everyday.svg, active.svg, relaxed.svg, essential.svg; README.md; AI_USAGE.md. Giữ .gitattributes đang có trên GitHub. Báo cáo và hướng dẫn được bàn giao riêng, không trộn với mã chạy.
