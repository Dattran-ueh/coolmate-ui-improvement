# Đạt Paper — Website văn phòng phẩm

- Sinh viên: Trần Gia Đạt
- MSSV: 31241024727
- Website public: http://datpaper31241024727.somee.com
- Repository: https://github.com/Dattran-ueh/coolmate-ui-improvement

## Mục tiêu

Xây dựng website cửa hàng văn phòng phẩm bằng ASP.NET Core MVC,
cá nhân hóa nội dung với thương hiệu Đạt Paper, dữ liệu sản phẩm,
hình ảnh và thông tin sinh viên.

## Công nghệ

- ASP.NET Core MVC trên .NET 10.
- Entity Framework Core và SQL Server.
- ASP.NET Core Identity.
- Razor Views và Bootstrap.
- Session lưu giỏ hàng.
- Gmail SMTP sử dụng mật khẩu ứng dụng.
- Triển khai trên Somee.

## Chức năng

- Đăng ký, xác nhận tài khoản và đăng nhập.
- Quản lý thể loại và sản phẩm.
- Upload ảnh sản phẩm; giữ ảnh cũ khi sửa mà không chọn ảnh mới.
- Hiển thị danh sách sản phẩm dạng lưới.
- Thêm, cập nhật số lượng và xóa sản phẩm khỏi giỏ hàng.
- Tính lại tổng tiền giỏ hàng.
- Kiểm tra thông tin nhận hàng, giá và tồn kho khi đặt hàng.
- Lưu hóa đơn, chi tiết hóa đơn và thông tin nhận hàng.
- Trừ tồn kho cùng giao dịch lưu đơn.
- Gửi email xác nhận sau khi lưu đơn.
- Giao diện thích ứng với màn hình máy tính và điện thoại.

## Cấu trúc

- SourceCode/31241024727_TranGiaDat/: mã nguồn dự án.
- Report/BaiTap_CaNhan_ASP_CORE_LMS.docx: báo cáo thực hành.
- Report/images/: ảnh minh chứng.

## Cấu hình và chạy trên máy

1. Cài .NET SDK 10 và Git.
2. Mở terminal trong SourceCode/31241024727_TranGiaDat.
3. Sao chép appsettings.example.json thành appsettings.json.
4. Điền kết nối SQL Server của người chạy.
5. Điền Gmail và mật khẩu ứng dụng nếu kiểm tra gửi email.
6. Chạy:

```powershell
dotnet restore
dotnet ef database update
dotnet run --launch-profile https
```

Nếu chưa cài công cụ Entity Framework:

```powershell
dotnet tool install --global dotnet-ef --version "10.0.*"
```

Mở địa chỉ được in trong terminal.
Các package hỗ trợ tạo mã và migration được giữ trong cấu hình Debug.

## Triển khai

Tạo bản Release bằng:

```powershell
dotnet publish -c Release --self-contained false -o ./publish
```

Bản triển khai sử dụng cấu hình thật của chủ website.
Không đưa cấu hình thật, bin, obj, publish hoặc publish.zip lên GitHub.

## Lưu ý sử dụng

- Giỏ hàng lưu trong Session và có thể mất khi ứng dụng khởi động lại.
- Đơn hàng được lưu vào SQL Server trước khi gửi email.
- Nếu gửi email thất bại, đơn đã lưu vẫn được giữ lại.
- Đây là website phục vụ bài thực hành, không thu tiền trực tuyến.
- Duy trì website public đến khi giảng viên công bố điểm chính thức.