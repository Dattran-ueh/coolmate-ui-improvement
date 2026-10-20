# AI Usage Log

## 1. Công cụ AI
ChatGPT, các phiên hỗ trợ ngày 19–20/10/2026.

## 2. Mục đích sử dụng
Hỗ trợ đọc đề, khảo sát phiên trình duyệt Coolmate, phân tích UX, chọn cải tiến, tạo HTML/CSS, giải thích mã và chuẩn bị tài liệu.

## 3. Prompt đã sử dụng
Em sử dụng AI để hỗ trợ khảo sát Coolmate, phân tích giao diện, đề xuất và tạo mã HTML/CSS, giải thích mã và biên soạn báo cáo. Các prompt dưới đây được chuẩn hóa từ nội dung trao đổi theo mẫu đề bài để lưu lại và tái lập quá trình làm việc với AI.

### Prompt 1 phân tích website
Bạn là chuyên gia phân tích nghiệp vụ và thiết kế trải nghiệm người dùng cho website thương mại điện tử. Tôi khảo sát Coolmate tại https://www.coolmate.me/. Dựa trên các ghi nhận thực tế được cung cấp, hãy lập bảng cho năm khu vực: trang chủ, danh mục áo thun nam, chi tiết sản phẩm, tìm kiếm và giỏ hàng. Mỗi khu vực cần có mục đích, thành phần giao diện, điểm thuận tiện, một vấn đề có thể phát sinh và một đề xuất cải tiến. Không khẳng định tính năng chưa kiểm chứng; tách quan sát, đánh giá và đề xuất AI. Không suy từ một phiên khảo sát thành kết luận về toàn bộ website.

### Prompt 2 đề xuất giao diện
Tôi thực hiện bài tập HTML5/CSS3 cho khách mua sắm trực tuyến tại Việt Nam. Tôi muốn thử phương án thẻ sản phẩm tối giản, giá dễ so sánh, thao tác luôn hiển thị và bố cục phù hợp điện thoại. Coolmate đã có giá gốc, giá giảm và bộ lọc, nên không được ghi các mục này là tính năng website còn thiếu. Hãy giải thích bố cục, thành phần HTML, tổ chức CSS, cách hiển thị máy tính/điện thoại và các điểm kiểm tra khả năng sử dụng. Trước tiên giải thích phương án, chưa tạo toàn bộ mã nguồn.

### Prompt 3 tạo mã
Dựa trên phương án thẻ sản phẩm đã trình bày, tạo một trang HTML5/CSS3 có tiêu đề, khu vực sản phẩm và ít nhất bốn sản phẩm mẫu. Mỗi sản phẩm có ảnh, tên, giá và nút thao tác. Tách CSS thành css/style.css; không dùng backend, CSDL hoặc thư viện không cần thiết. Dùng ảnh SVG tự vẽ, dữ liệu giả định có nhãn rõ ràng. Dùng details/summary cho thao tác Xem thông tin thực sự hoạt động mà không cần JavaScript. Giải thích cách chạy, responsive và những nội dung cần kiểm tra trên trình duyệt.

### Prompt hỗ trợ kiểm tra và điều chỉnh ngày 20/10/2026
Nội dung yêu cầu được chuẩn hóa từ trao đổi: Hướng dẫn kiểm tra giao diện ở nhiều kích thước bằng Firefox. Em nhận thấy giao diện cần CTA rõ hơn; hãy phân tích vị trí cần bổ sung, hướng dẫn sửa HTML/CSS và giải thích cách kiểm tra sau thay đổi.

ChatGPT phân biệt thao tác “Xem thông tin” đã có trên từng thẻ với CTA chính còn thiếu ở phần giới thiệu; hướng dẫn thêm liên kết “Khám phá áo thun”, CSS tương ứng và kiểm tra bằng Firefox. Em thực hiện sửa trên máy, cung cấp ảnh kiểm tra và mã nguồn để đối chiếu.

## 4. Kết quả AI đề xuất
Bảng khảo sát năm khu vực, bảng phân tích AI năm khu vực, một phương án thẻ sản phẩm responsive, bốn sản phẩm giả định, ảnh SVG tự vẽ, mã HTML/CSS và giải thích. Phiên khảo sát đã thử size M, giá thấp đến cao, tìm áo thun, thêm/tăng/xóa sản phẩm trong giỏ. Không gửi đơn, không thanh toán. AI còn đề xuất bổ sung CTA điều hướng ở phần giới thiệu và giải thích đoạn HTML/CSS liên quan.

## 5. Kiểm tra và điều chỉnh
Đã đối chiếu: Coolmate có giá gốc/giá bán/giảm giá, tìm kiếm có gợi ý và giỏ có cập nhật tiền. Loại nhận xét không có căn cứ rằng website thiếu những chức năng này. Đã kiểm tra tĩnh bốn thẻ, bốn ảnh và bốn summary; liên kết tài nguyên hợp lệ; CSS riêng; không có script hoặc backend. 

Ngày 20/10/2026, em trực tiếp mở trang bằng Firefox, nhận thấy phần giới thiệu cần một CTA nổi bật và bổ sung theo hướng dẫn của AI:
- HTML: thêm a.primary-cta, nhãn “Khám phá áo thun”, href="#products". Trước thay đổi, phần giới thiệu chưa có liên kết này.
- CSS: thêm .primary-cta và .primary-cta:hover; dùng inline-flex, min-height:48px, padding, màu nền/chữ, khoảng cách và giới hạn chiều rộng. Trước thay đổi, chưa có hai quy tắc này.
- Kiểm tra responsive: 1366 × 900 px bốn cột; 768 × 1024 px hai cột; 390 × 844 px và 320 × 800 px một cột.
- Kiểm tra thao tác: CTA dẫn đúng vùng sản phẩm; dùng Tab/Enter được; bốn phần thông tin mở/đóng được; không cuộn ngang ở 390/320 px.

Minh chứng gồm ảnh chụp bằng Firefox Responsive Design Mode và mã nguồn sau chỉnh sửa. Em trực tiếp chạy thử và xác nhận kết quả thao tác; ChatGPT đối chiếu ảnh, kiểm tra mã CTA, CSS và các đường dẫn tài nguyên trong ZIP ngày 20/10/2026.

## 6. Phần em hiểu và có thể giải thích
Các thành phần quan trọng và vai trò:
- article: nhóm nội dung của từng sản phẩm; img/alt và đường dẫn tương đối: hiển thị ảnh, cung cấp văn bản thay thế.
- strong/del: phân biệt giá bán với giá tham chiếu.
- details/summary: mở và đóng mô tả bằng cơ chế HTML gốc.
- CSS Grid và media query: chuyển số cột theo độ rộng; minmax(0,1fr), flex-wrap và box-sizing giúp nội dung co, xuống dòng và kiểm soát kích thước.
- Phần đã sửa: a với href="#products" điều hướng trong trang; class primary-cta liên kết HTML với CSS; inline-flex căn nội dung; min-height và padding tạo vùng thao tác; max-width giới hạn chiều rộng; hover phản hồi khi đưa chuột lên.
- Quy tắc a:focus-visible có sẵn áp dụng cho CTA mới, giúp nhận biết liên kết khi dùng Tab; Enter kích hoạt liên kết.

Những giải thích này được dùng để đối chiếu với mã và các thao tác em đã thực hiện trong phần kiểm tra.
