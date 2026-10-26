# Báo cáo sử dụng AI trong bài tập cá nhân Buổi 2

## 1. Công cụ và phạm vi hỗ trợ

Công cụ: ChatGPT.

Bài tập tiếp tục giao diện Everyday UI tham khảo Coolmate từ Buổi 1. Khu vực cải tiến là danh sách bốn áo thun và vùng giới thiệu dẫn tới danh sách. AI hỗ trợ phân tích mã, đề xuất và điều chỉnh HTML/CSS, giải thích kỹ thuật, kiểm tra tĩnh và tổ chức tài liệu. Bài sử dụng HTML5 và CSS3, không dùng JavaScript, Bootstrap hoặc Tailwind CSS.

## 2. Các prompt theo tiến trình thực hành

Các prompt dưới đây được chuẩn hóa từ chín prompt mẫu trong BaiTap_CaNhan_Buoi2.pdf, gắn với mã và yêu cầu của bài Coolmate. Đây là cách trình bày lại yêu cầu theo từng bước thực hành, không phải bản sao nguyên văn lịch sử trò chuyện. Mã đầu vào được đối chiếu từ bộ tệp Buổi 1 đã cung cấp và bản index.html, css/style.css của Buổi 2.

### Prompt 1 — Phân tích mã nguồn hiện tại

```text
Tôi đang làm bài tập cá nhân Buổi 2 môn Phát triển ứng dụng thương mại điện tử.
Bài Buổi 1 của tôi là giao diện Everyday UI tham khảo Coolmate, gồm bốn áo thun:
Everyday Cotton, Active Dry, Relaxed Fit và Essential Basic.

Dựa trên index.html và css/style.css của Buổi 1 đã cung cấp, hãy:
1. Xác định các thành phần HTML và bố cục CSS đang có.
2. Phân biệt lỗi cần sửa với đề xuất cải thiện cách trình bày.
3. Chỉ ra ít nhất ba vấn đề có căn cứ trong vùng giới thiệu và thẻ sản phẩm.
4. Đề xuất cách xử lý bằng HTML5 và CSS3 cơ bản, giải thích tác dụng.
5. Sắp xếp thứ tự thực hiện để tôi hiểu mã trước khi sửa.

Không tạo website khác. Không coi những kỹ thuật đã có từ Buổi 1 là phần mới.
Giữ danh sách sản phẩm, giá mẫu và ảnh gốc để đối chiếu trước sau.
```

**Kết quả hỗ trợ:** Mã cũ đã có cấu trúc HTML có ngữ nghĩa, CSS ngoài, Flexbox, Grid và responsive. Các điểm được chọn để cải thiện là vùng intro có khoảng đệm và tiêu đề lớn, khung ảnh 5/6 chiếm nhiều chiều dọc, mô tả mở rộng chung và nhãn điều khiển chưa phân biệt trạng thái đóng mở.

**Đối chiếu và điều chỉnh:** Không ghi nhận “thiếu semantic HTML” hoặc “chưa có responsive” như lỗi của bản cũ. Thứ tự xử lý là rà cấu trúc, điều chỉnh khoảng cách và bố cục, làm rõ nội dung cùng trạng thái, sau đó kiểm tra tài nguyên và tài liệu.

### Prompt 2 — Kiểm tra cấu trúc HTML5

```text
Tôi muốn kiểm tra cấu trúc HTML5 của giao diện áo thun kế thừa từ Buổi 1.
Hãy đọc index.html của bài và:
1. Giải thích vai trò của header, nav, main, section, article và footer đang dùng.
2. Kiểm tra thứ bậc h1, h2, h3 và các thuộc tính aria-labelledby liên quan.
3. Kiểm tra img, alt, liên kết a và đích id trong trang.
4. Kiểm tra ul/li cho menu và danh sách thuộc tính sản phẩm.
5. Rà thẻ đóng mở và cách lồng phần tử.
6. Giải thích vì sao details/summary phù hợp với việc mở thông tin tại chỗ.

Chỉ sửa phần chưa phù hợp; không thêm thẻ chỉ để mã dài hơn.
Không đổi điều khiển mở thông tin thành liên kết tới trang chi tiết không tồn tại.
```

**Kết quả hỗ trợ:** Bản mới giữ header/nav/main/section/article/footer và h1–h3. Bốn sản phẩm có article riêng, img có alt; liên kết nội trang trỏ tới #top, #intro và #products. Nội dung chi tiết dùng ul/li trong details; summary có nhãn đóng mở, tên sản phẩm cho trình đọc màn hình và biểu tượng trang trí aria-hidden.

**Cách kiểm tra:** Bộ phân tích HTML kiểm tra đóng/lồng thẻ, id duy nhất và đích liên kết. Kết quả kiểm tra tĩnh không phát hiện sai lệch trong các mục này.

### Prompt 3 — Tạo và liên kết CSS cơ bản

```text
Website của tôi đã có index.html, css/style.css và bốn ảnh SVG trong images/.
Tôi muốn tiếp tục dùng CSS ngoài của Buổi 1, không tạo lại cấu trúc dự án.

Hãy kiểm tra và giải thích:
1. Liên kết stylesheet trong head có đúng đường dẫn css/style.css không.
2. Font, màu chữ, màu nền và margin của body.
3. Cách dùng biến CSS để thống nhất màu sắc.
4. Quy tắc giúp ảnh không vượt phần tử chứa và không bị kéo méo.
5. Cách kiểm tra CSS và ảnh khi mở index.html bằng trình duyệt.

Chỉ dùng CSS cần cho khu vực đã chọn, không dùng framework.
Phân biệt việc tệp CSS tồn tại với việc trình duyệt đã tải và áp dụng CSS.
```

**Kết quả hỗ trợ:** HTML liên kết css/style.css. CSS dùng Arial, body margin:0 và các biến --ink, --muted, --accent, --line, --paper. Ảnh có width:100%, height:auto và object-fit:contain.

**Cách kiểm tra:** Tệp CSS và cả bốn ảnh có đúng đường dẫn tương đối. Việc kiểm tra áp dụng kiểu trong trình duyệt được thực hiện theo hướng dẫn chạy và đối chiếu giao diện, tách khỏi kiểm tra đường dẫn tĩnh.

### Prompt 4 — Thực hành selector và Box Model

```text
Tôi muốn cải thiện một thẻ áo thun trong danh sách hiện có bằng CSS selector
và Box Model. Hãy dựa trên article.product-card và các phần tử bên trong để:
1. Chỉ ra element selector, class selector và descendant selector.
2. Giải thích margin, padding, border và box-sizing:border-box bằng chính mã bài.
3. Điều chỉnh khoảng cách và bo góc để phân tách ảnh, tên, chất liệu, giá, thao tác.
4. Đề xuất khung ảnh thấp hơn tỷ lệ 5/6 nhưng vẫn giữ trọn SVG.
5. Đặt nhãn giảm giá lên vùng ảnh và giải thích containing block của nhãn.
6. Giữ đúng chức năng của summary khi định dạng thành điều khiển thao tác.

Chỉ đưa ra CSS và thay đổi HTML cần thiết. Không thay sản phẩm hay ảnh ngẫu nhiên.
Giải thích vì sao thay đổi giải quyết vấn đề trình bày đã nêu.
```

**Kết quả hỗ trợ:** Product-body có padding 16px, mobile 18px; card có border 1px, border-radius 12px. Khung ảnh chuyển sang 3/2 trên màn hình rộng và 4/3 trên mobile. Card đặt position:relative; badge đặt absolute với top/left 12px. Giá chính 24px, giá gốc dùng del.

**Đối chiếu và điều chỉnh:** Giữ ảnh SVG gốc, dùng contain để thấy toàn bộ minh họa. Không coi việc thay màu hoặc bo góc đơn thuần là sửa lỗi chức năng. Với cùng bề rộng 280px, chiều cao theo tỷ lệ 5/6 là 336px, còn 3/2 khoảng 187px; đây là phép tính thiết kế, không phải số đo trình duyệt.

### Prompt 5 — Thực hành Flexbox

```text
Tôi muốn thực hành Flexbox trên header, menu, nhóm giá và summary của bài áo thun.
Dựa trên HTML/CSS hiện có, hãy:
1. Xác định phần tử cha cần display:flex.
2. Giải thích justify-content, align-items và gap theo trục của từng nhóm.
3. Căn thương hiệu và menu ở hai phía trên màn hình rộng.
4. Căn chữ và mũi tên của summary, giữ khoảng cách giữa hai thành phần.
5. Giải thích flex-wrap để menu hoặc nhóm giá có thể xuống dòng khi thiếu chỗ.
6. So sánh tác dụng của space-between với center trong summary.

Giữ cấu trúc hiện có, không viết lại toàn bộ giao diện và không dùng framework.
Phân biệt phần Flexbox kế thừa với phần điều chỉnh trong Buổi 2.
```

**Kết quả hỗ trợ:** Header-row dùng space-between, align-items:center và gap 16px. Menu và nhóm giá có flex-wrap. Summary dùng space-between để nhãn và biểu tượng nằm ở hai phía, gap 12px để giữ khoảng cách tối thiểu.

**Cách kiểm tra:** Đối chiếu selector cha trong CSS. Với hướng hàng mặc định, justify-content căn theo chiều ngang và align-items theo chiều dọc; khi đổi flex-direction, hướng trục cũng đổi. Kiểm tra trực quan cần quan sát khi thu hẹp cửa sổ và khi nhãn đóng mở thay đổi.

### Prompt 6 — Thực hành CSS Grid

```text
Danh sách bốn áo thun của tôi đã có CSS Grid từ Buổi 1.
Hãy giải thích và điều chỉnh bố cục đang có, không tạo lại danh sách:
1. Giải thích display:grid, grid-template-columns, repeat, minmax và gap.
2. Giữ bốn cột trên màn hình rộng, hai cột ở tối đa 1000px,
   một cột ở tối đa 540px; giải thích thứ tự media query.
3. Giữ khoảng cách giữa các sản phẩm và tránh ép nội dung vượt cột.
4. Giải thích aspect-ratio kết hợp object-fit:contain cho ảnh SVG.
5. So sánh contain với cover và chọn cách phù hợp để giữ toàn bộ hình áo.
6. Hướng dẫn đối chiếu ở 1366px, 768px và 390px.

Chỉ viết CSS cần cho danh sách hiện có. Không gọi cơ chế Grid kế thừa là cải tiến mới.
```

**Kết quả hỗ trợ:** Product-grid tiếp tục bố cục 4/2/1 cột với các ngưỡng 1000px và 540px. Minmax(0, 1fr) chia đều phần trống và cho phép cột co lại; gap là 20px. Ảnh đổi tỷ lệ theo kích thước màn hình nhưng vẫn dùng contain.

**Đối chiếu và điều chỉnh:** Prompt mẫu của đề minh họa hai cột và object-fit:cover. Bài điều chỉnh theo danh sách bốn sản phẩm đã có, chọn contain để giữ toàn bộ hình; cover có thể cắt bớt nội dung. Việc thay đổi này được giải thích theo dữ liệu thực tế của bài.

### Prompt 7 — Bổ sung hiệu ứng CSS

```text
Tôi muốn làm rõ phản hồi tương tác của liên kết và summary trong giao diện áo thun.
Dựa trên HTML/CSS hiện tại, hãy:
1. Tạo hover đổi màu nhẹ, không gây dịch chuyển bố cục.
2. Giới hạn transition ở các thuộc tính màu cần thiết.
3. Giữ focus-visible và chọn màu viền phù hợp trên nền sáng, nền intro tối.
4. Đổi nhãn và hướng biểu tượng theo details[open].
5. Tôn trọng prefers-reduced-motion.
6. Giải thích cách thử bằng chuột, Tab, Enter và phím cách.

Chỉ đưa ra phần CSS hoặc nhãn HTML cần điều chỉnh, không dùng JavaScript.
Không gán aria-expanded cố định làm lệch trạng thái của details.
```

**Kết quả hỗ trợ:** Hover đổi màu; transition áp dụng background-color, color và border-color trong 0,2 giây. Focus-visible dùng outline 3px màu #7a3e00 trên nền sáng, riêng liên kết intro dùng #ffcf7a. Details[open] đổi nhãn sang “Thu gọn thông tin” và xoay biểu tượng. Chế độ giảm chuyển động tắt transition và cuộn mượt.

**Cách kiểm tra:** Đối chiếu quy tắc CSS và hai nhãn trong cả bốn summary. Kiểm tra trạng thái thực tế cần dùng chuột và bàn phím trên cùng bản mã; hover không thay cho kiểm tra focus-visible.

### Prompt 8 — Nhờ AI hỗ trợ kiểm tra lỗi

```text
Tôi đang rà lại giao diện Buổi 2 kế thừa từ Buổi 1.
Điểm cần xử lý trong bản cũ là summary vẫn ghi “Xem thông tin” khi đang mở;
marker mặc định bị ẩn và phần mở rộng chủ yếu là thông báo chung.

Dựa trên index.html và css/style.css đã cung cấp, hãy:
1. Chỉ ra nguyên nhân trong HTML và các selector trạng thái.
2. Đề xuất cách sửa gọn bằng details/summary và CSS [open].
3. Làm rõ nội dung mở rộng riêng cho từng áo từ dữ liệu mẫu đang có.
4. Kiểm tra thẻ, id, đường dẫn CSS/ảnh, alt và nhãn đóng mở.
5. Nêu cách thử lại cả bốn thẻ bằng chuột và bàn phím.
6. Tách kết quả kiểm tra tĩnh khỏi kết quả chạy trên trình duyệt.

Không bịa lỗi đã gặp, kết quả thử hoặc ảnh sau.
Không ghi thao tác AI sửa mã thành thao tác sinh viên tự sửa độc lập.
```

**Kết quả hỗ trợ:** Cách sửa dùng hai span nhãn, selector details[open] và biểu tượng CSS. Nội dung mở rộng chuyển thành danh sách thuộc tính riêng theo áo, thay cho đoạn thông báo chung. Bộ kiểm tra tĩnh đạt 10 mục: đóng/lồng thẻ; id duy nhất; đích liên kết nội trang; tệp CSS/ảnh; số lượng article/details; alt và SVG; nhãn đóng mở; ngoặc CSS; khai báo breakpoint/trạng thái; số tệp bàn giao.

**Phạm vi kết quả:** Các kết quả trên thuộc kiểm tra mã nguồn. Ảnh sau của bản mới, thao tác trình duyệt và phần sinh viên tự sửa trực tiếp được ghi bằng minh chứng thực hành tương ứng; không lấy kết quả phiên bản trước gán cho bản mới.

### Prompt 9 — Kiểm tra README

```text
Tôi đang hoàn thiện README.md của bài tập cá nhân Buổi 2,
tiếp tục repository coolmate-ui-improvement từ Buổi 1.

Hãy rà và trình bày README ngắn gọn, gồm:
1. Giới thiệu bài và dữ liệu sản phẩm minh họa.
2. Website tham khảo Coolmate, khu vực được chọn và giới hạn cải tiến.
3. Kỹ thuật HTML5, selector, Box Model, Flexbox, Grid, hover/focus đã thực hành.
4. Cách mở index.html với css/ và images/ đúng cấu trúc.
5. Các thay đổi có căn cứ và phần kế thừa từ Buổi 1.
6. Công cụ AI, cách kiểm tra và liên kết tới AI_USAGE.md.
7. Link repository https://github.com/Dattran-ueh/coolmate-ui-improvement
   và website https://dattran-ueh.github.io/coolmate-ui-improvement/.

Không ghi đã triển khai bản mới khi chỉ mới chuẩn bị mã cục bộ.
Không thêm tệp kiểm thử thừa vào thư mục mã chạy.
Không thay đổi repository hoặc tải mã lên khi tôi chỉ yêu cầu chuẩn bị tệp.
```

**Kết quả hỗ trợ:** README có giới thiệu, website tham khảo, phạm vi cải tiến, kỹ thuật, cách chạy, lý do thay đổi, AI và kiểm tra, cấu trúc tệp cùng hai đường dẫn. Bộ mã nguồn gồm tám tệp: index.html, css/style.css, bốn SVG, README.md và AI_USAGE.md.

**Cách kiểm tra:** Đối chiếu nội dung README với tệp thực tế và mã đã bàn giao. Người học quản lý việc cập nhật repository; tài liệu không dùng việc chuẩn bị ZIP để khẳng định website đã nhận mã mới.

## 3. Đề xuất AI được phản biện và điều chỉnh

Phương án đầu tập trung vào mô tả, biểu tượng và focus nên khác biệt tổng thể còn nhỏ. Người học phản biện về mức độ cải thiện và cách tổ chức tệp. Theo phản biện đó, phương án chuyển sang xử lý vùng intro cao, khung ảnh cao và phân cấp nội dung thẻ; đồng thời loại các trang kiểm thử phụ khỏi mã chạy.

Giữ lại bốn ảnh gốc để đối chiếu sự kế thừa, thay đổi tỷ lệ khung hiển thị thay vì đổi ảnh chỉ để tạo khác biệt. Không coi việc đổi màu đơn thuần là bằng chứng sửa lỗi chức năng. Không ghi các tính năng đã có như Grid và responsive thành tính năng mới của Buổi 2.

## 4. Phân định phần thực hiện

- **Người học:** cung cấp đề, bài Buổi 1, mã nguồn và phản biện phương án; quyết định việc tải mã lên GitHub.
- **AI:** phân tích và chỉnh HTML/CSS theo yêu cầu, giải thích kỹ thuật, kiểm tra tĩnh, tổ chức tài liệu và chuẩn hóa prompt theo đề.
- **Thực hành độc lập:** yêu cầu sinh viên tự sửa ở mục 11 của đề cần ghi đúng vấn đề quan sát, mã trước/sau, thao tác tự thực hiện và kết quả chạy lại. Phản biện phương án không được thay thế cho thao tác tự sửa mã.

## 5. Nguồn đối chiếu

BaiTap_CaNhan_Buoi2.pdf, các mục 4.2, 5.2, Prompt 3, 7.2, 8.2, 9.2, 10.2, 11.2 và 14.2; mã Buổi 1 do người học cung cấp; index.html, css/style.css và README.md của bộ mã Buổi 2.

## 6. Sửa bổ sung độ rõ của giá gốc

Ngày 27/10/2026, theo yêu cầu tiếp tục rà và sửa bài, ChatGPT chọn điểm cần cải thiện: giá gốc có gạch ngang dùng màu #728194 ở cỡ chữ 12px trên nền trắng. CSS đổi duy nhất màu của .price del sang #526472; cỡ chữ, số tiền, dấu gạch và bố cục được giữ nguyên. Tỷ lệ tương phản tính từ độ chói sRGB tăng từ khoảng 3,97:1 lên 6,13:1. Đây là phép kiểm tra màu và mã, không phải phép đo trên ảnh.

Prompt bổ sung được chuẩn hóa: “Rà một vấn đề nhỏ có căn cứ trong bản Buổi 2; sửa ít thay đổi nhất; ghi nguyên nhân, selector, mã trước sau và cách kiểm tra; đồng bộ Word với ZIP.”

Phần sửa này do ChatGPT thực hiện theo yêu cầu của người học, không được ghi thành thao tác sinh viên tự sửa độc lập. Ảnh trình duyệt trong báo cáo ghi lại bản đã triển khai trước lượt sửa màu bổ sung; tệp CSS mới được bàn giao trong ZIP để người học cập nhật lên GitHub và chạy lại.
