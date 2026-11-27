using System.Text;
using DatPaper.Extension;
using DatPaper.Data;
using DatPaper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DatPaper.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _config;
    private const string DefaultImage = "/images/no-image.jpg";

    public ProductsController(
    ApplicationDbContext context,
    IWebHostEnvironment env,
    IConfiguration config)
{
    _context = context;
    _env = env;
    _config = config;
}

    private void LoadCategories(int? selected = null)
    {
        ViewData["CategoryId"] = new SelectList(
            _context.Categories.AsNoTracking().OrderBy(c => c.Name),
            "Id", "Name", selected);
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Products.AsNoTracking()
            .Include(p => p.Category).OrderByDescending(p => p.Id).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var product = await _context.Products.AsNoTracking()
            .Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }

    public IActionResult Create()
    {
        LoadCategories();
        return View(new Product { DateAdded = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Name,Description,DateAdded,Price,Stock,CategoryId,ImageUpload")] Product product)
    {
        string newPath = null;
        if (ModelState.IsValid)
        {
            try { newPath = await SaveImageAsync(product.ImageUpload); }
            catch (ArgumentException ex) { ModelState.AddModelError("ImageUpload", ex.Message); }
            catch (IOException) { ModelState.AddModelError("ImageUpload", "Không lưu được ảnh. Vui lòng thử lại."); }
            catch (UnauthorizedAccessException) { ModelState.AddModelError("ImageUpload", "Ứng dụng chưa có quyền ghi thư mục ảnh."); }
        }
        if (!ModelState.IsValid)
        {
            LoadCategories(product.CategoryId);
            return View(product);
        }
        product.ImagePath = newPath ?? DefaultImage;
        _context.Products.Add(product);
        try { await _context.SaveChangesAsync(); }
        catch { DeleteManagedImage(newPath); throw; }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        LoadCategories(product.CategoryId);
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Name,Description,DateAdded,Price,Stock,CategoryId,ImageUpload")] Product input)
    {
        if (id != input.Id) return NotFound();
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        string newPath = null;
        if (ModelState.IsValid)
        {
            try { newPath = await SaveImageAsync(input.ImageUpload); }
            catch (ArgumentException ex) { ModelState.AddModelError("ImageUpload", ex.Message); }
            catch (IOException) { ModelState.AddModelError("ImageUpload", "Không lưu được ảnh. Vui lòng thử lại."); }
            catch (UnauthorizedAccessException) { ModelState.AddModelError("ImageUpload", "Ứng dụng chưa có quyền ghi thư mục ảnh."); }
        }
        if (!ModelState.IsValid)
        {
            input.ImagePath = product.ImagePath;
            LoadCategories(input.CategoryId);
            return View(input);
        }
        var oldPath = product.ImagePath;
        product.Name = input.Name;
        product.Description = input.Description;
        product.DateAdded = input.DateAdded;
        product.Price = input.Price;
        product.Stock = input.Stock;
        product.CategoryId = input.CategoryId;
        // Đọc đường dẫn cũ từ CSDL, không tin giá trị do trình duyệt gửi lên.
        product.ImagePath = newPath ?? (string.IsNullOrWhiteSpace(oldPath) ? DefaultImage : oldPath);
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException)
        {
            DeleteManagedImage(newPath);
            if (!await _context.Products.AnyAsync(p => p.Id == id)) return NotFound();
            throw;
        }
        catch { DeleteManagedImage(newPath); throw; }
        if (newPath != null) DeleteManagedImage(oldPath);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var product = await _context.Products.AsNoTracking()
            .Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        DeleteManagedImage(product.ImagePath);
        return RedirectToAction(nameof(Index));
    }

    private async Task<string> SaveImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0) return null;
        if (file.Length > 2 * 1024 * 1024)
            throw new ArgumentException("Ảnh phải có dung lượng không quá 2 MB.");
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" }.Contains(ext))
            throw new ArgumentException("Chỉ chấp nhận ảnh JPG, JPEG, PNG, GIF hoặc WEBP.");
        if (file.Length < 12)
            throw new ArgumentException("Tệp ảnh không hợp lệ.");

        using var source = file.OpenReadStream();
        var header = new byte[12];
        await source.ReadExactlyAsync(header.AsMemory());
        bool valid = ext switch
        {
            ".jpg" or ".jpeg" => header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF,
            ".png" => header.Take(8).SequenceEqual(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }),
            ".gif" => System.Text.Encoding.ASCII.GetString(header, 0, 6) is "GIF87a" or "GIF89a",
            ".webp" => System.Text.Encoding.ASCII.GetString(header, 0, 4) == "RIFF"
                && System.Text.Encoding.ASCII.GetString(header, 8, 4) == "WEBP",
            _ => false
        };
        if (!valid) throw new ArgumentException("Nội dung tệp không khớp định dạng ảnh.");

        var folder = Path.Combine(_env.WebRootPath, "images", "products");
        Directory.CreateDirectory(folder);
        var fileName = Guid.NewGuid().ToString("N") + ext;
        var fullPath = Path.Combine(folder, fileName);
        try
        {
            using var target = System.IO.File.Create(fullPath);
            await target.WriteAsync(header.AsMemory());
            await source.CopyToAsync(target);
        }
        catch
        {
            if (System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);
            throw;
        }
        return "/images/products/" + fileName;
    }

    private void DeleteManagedImage(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath)
            || !imagePath.StartsWith("/images/products/", StringComparison.Ordinal)) return;
        var fileName = imagePath.Substring("/images/products/".Length);
        if (string.IsNullOrWhiteSpace(fileName) || fileName != Path.GetFileName(fileName)) return;
        try
        {
            var path = Path.Combine(_env.WebRootPath, "images", "products", fileName);
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
        }
        catch (IOException) { /* Không làm mất kết quả lưu CSDL khi dọn ảnh cũ thất bại. */ }
        catch (UnauthorizedAccessException) { }
    }
    // Đọc giỏ hàng và cập nhật thông tin sản phẩm từ CSDL.
private async Task<HoaDon> GetCartAsync()
{
    var cart = HttpContext.Session.Get<HoaDon>("HoaDon");

    var ids = cart.ChiTietHoaDons
        .Select(x => x.MaProduct)
        .Distinct()
        .ToList();

    var products = await _context.Products
        .AsNoTracking()
        .Where(p => ids.Contains(p.Id))
        .ToDictionaryAsync(p => p.Id);

    foreach (var line in cart.ChiTietHoaDons.ToList())
    {
        if (!products.TryGetValue(line.MaProduct, out var product)
            || product.Stock <= 0
            || line.SoLuong <= 0)
        {
            cart.ChiTietHoaDons.Remove(line);
            continue;
        }

        line.ProductObj = product;
        line.DonGia = product.Price;
        line.SoLuong = Math.Min(line.SoLuong, product.Stock);
    }

    SaveCart(cart);
    return cart;
}

// Tính lại tổng tiền trước mỗi lần lưu Session.
private void SaveCart(HoaDon cart)
{
    cart.TongTien = cart.ChiTietHoaDons
        .Sum(x => x.SoLuong * x.DonGia);

    HttpContext.Session.Set("HoaDon", cart);
}

// GET: xem giỏ hàng, không thêm sản phẩm.
[HttpGet]
public async Task<IActionResult> AddToCart()
{
    var cart = await GetCartAsync();
    return View(cart);
}

// POST: thêm một sản phẩm vào giỏ.
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> AddToCart(int id)
{
    var product = await _context.Products
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id);

    if (product == null)
        return NotFound();

    var cart = await GetCartAsync();

    if (product.Stock <= 0)
    {
        TempData["CartMessage"] = "Sản phẩm đã hết hàng.";
        return RedirectToAction(nameof(AddToCart));
    }

    var line = cart.ChiTietHoaDons
        .FirstOrDefault(x => x.MaProduct == id);

    if (line == null)
    {
        cart.ChiTietHoaDons.Add(new ChiTietHoaDon
        {
            MaProduct = product.Id,
            ProductObj = product,
            SoLuong = 1,
            DonGia = product.Price
        });
    }
    else if (line.SoLuong < product.Stock)
    {
        line.SoLuong++;
    }
    else
    {
        TempData["CartMessage"] =
            "Số lượng trong giỏ đã đạt số lượng tồn kho.";
    }

    SaveCart(cart);
    return RedirectToAction(nameof(AddToCart));
}

// POST: cập nhật số lượng.
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> UpdateCart(int id, int quantity)
{
    var cart = await GetCartAsync();

    var line = cart.ChiTietHoaDons
        .FirstOrDefault(x => x.MaProduct == id);

    if (line == null)
        return RedirectToAction(nameof(AddToCart));

    if (!ModelState.IsValid
        || quantity < 1
        || quantity > line.ProductObj.Stock)
    {
        TempData["CartMessage"] =
            $"Số lượng phải từ 1 đến {line.ProductObj.Stock}.";
    }
    else
    {
        line.SoLuong = quantity;
        SaveCart(cart);
    }

    return RedirectToAction(nameof(AddToCart));
}

// POST: xóa một sản phẩm khỏi giỏ.
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> RemoveFromCart(int id)
{
    var cart = await GetCartAsync();

    var line = cart.ChiTietHoaDons
        .FirstOrDefault(x => x.MaProduct == id);

    if (line != null)
        cart.ChiTietHoaDons.Remove(line);

    SaveCart(cart);
    return RedirectToAction(nameof(AddToCart));
}
[HttpGet]
public async Task<IActionResult> Checkout()
{
    var cart = await GetCartAsync();

    if (!cart.ChiTietHoaDons.Any())
    {
        TempData["CartMessage"] =
            "Hãy chọn sản phẩm trước khi đặt hàng.";

        return RedirectToAction(nameof(AddToCart));
    }

    ViewBag.CartTotal = cart.TongTien;
    return View(new ShippingDetail());
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Checkout(
    [Bind("Name,Address,Email,Mobile,ReleaseDate")]
    ShippingDetail detail)
{
    var cart = HttpContext.Session.Get<HoaDon>("HoaDon");
    ViewBag.CartTotal = cart.TongTien;

    if (!cart.ChiTietHoaDons.Any())
    {
        ModelState.AddModelError(
            "", "Giỏ hàng đang trống. Vui lòng chọn sản phẩm.");
    }

    if (!ModelState.IsValid)
        return View(detail);

    // Kiểm tra lại giá và tồn kho từ CSDL.
    var ids = cart.ChiTietHoaDons
        .Select(x => x.MaProduct)
        .Distinct()
        .ToList();

    var products = await _context.Products
        .AsNoTracking()
        .Where(p => ids.Contains(p.Id))
        .ToDictionaryAsync(p => p.Id);

    foreach (var line in cart.ChiTietHoaDons)
    {
        if (!products.TryGetValue(line.MaProduct, out var product)
            || line.SoLuong < 1
            || line.SoLuong > product.Stock
            || line.DonGia != product.Price)
        {
            var refreshedCart = await GetCartAsync();
            ViewBag.CartTotal = refreshedCart.TongTien;

            ModelState.AddModelError(
                "",
                "Giá hoặc tồn kho đã thay đổi. "
                + "Vui lòng quay lại kiểm tra giỏ hàng.");

            return View(detail);
        }
    }

    var order = new HoaDon
    {
        NgayLap = DateTimeOffset.UtcNow
            .ToOffset(TimeSpan.FromHours(7)).DateTime,
        TongTien = cart.ChiTietHoaDons
            .Sum(x => x.SoLuong * products[x.MaProduct].Price)
    };

    foreach (var line in cart.ChiTietHoaDons)
    {
        order.ChiTietHoaDons.Add(new ChiTietHoaDon
        {
            MaProduct = line.MaProduct,
            SoLuong = line.SoLuong,
            DonGia = products[line.MaProduct].Price
        });
    }

    // Lưu hóa đơn, chi tiết, địa chỉ và trừ tồn cùng giao dịch.
    await using (var transaction =
        await _context.Database.BeginTransactionAsync())
    {
        try
        {
            foreach (var line in order.ChiTietHoaDons)
            {
                var changed = await _context.Products
                    .Where(p => p.Id == line.MaProduct
                        && p.Stock >= line.SoLuong
                        && p.Price == line.DonGia)
                    .ExecuteUpdateAsync(update => update
                        .SetProperty(
                            p => p.Stock,
                            p => p.Stock - line.SoLuong));

                if (changed != 1)
                {
                    throw new InvalidOperationException(
                        "Sản phẩm không còn đủ số lượng hoặc đã đổi giá.");
                }
            }

            detail.HoaDonObj = order;

            _context.HoaDons.Add(order);
            _context.ShippingDetails.Add(detail);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex) when (
            ex is DbUpdateException
            || ex is InvalidOperationException)
        {
            await transaction.RollbackAsync();
            _context.ChangeTracker.Clear();

            ModelState.AddModelError(
                "",
                "Chưa lưu được đơn hàng. "
                + "Vui lòng kiểm tra lại giỏ hàng rồi thử lại.");

            return View(detail);
        }
    }

    // Đơn đã lưu: xóa giỏ trước khi thực hiện gửi email.
    HttpContext.Session.Remove("HoaDon");

    var vietnamese =
        System.Globalization.CultureInfo.GetCultureInfo("vi-VN");

    string Enc(string value) =>
        System.Net.WebUtility.HtmlEncode(value ?? "");

    var body = new StringBuilder();

    body.Append("<h2>Đạt Paper xác nhận đơn hàng</h2>");
    body.Append($"<p>Mã đơn: <strong>#{order.MaHoaDon}</strong></p>");
    body.Append($"<p>Xin chào {Enc(detail.Name)},</p>");
    body.Append("<p>Cảm ơn bạn đã đặt hàng tại Đạt Paper.</p><ul>");

    foreach (var line in order.ChiTietHoaDons)
    {
        var product = products[line.MaProduct];
        var subtotal = line.SoLuong * line.DonGia;

        body.Append(
            $"<li>{Enc(product.Name)} — "
            + $"{line.SoLuong} × "
            + $"{line.DonGia.ToString("N0", vietnamese)} đ"
            + $" = {subtotal.ToString("N0", vietnamese)} đ</li>");
    }

    body.Append("</ul>");
    body.Append(
        $"<p><strong>Tổng tiền: "
        + $"{order.TongTien.ToString("N0", vietnamese)} đ"
        + "</strong></p>");

    body.Append($"<p>Người nhận: {Enc(detail.Name)}</p>");
    body.Append($"<p>Địa chỉ: {Enc(detail.Address)}</p>");
    body.Append($"<p>Điện thoại: {Enc(detail.Mobile)}</p>");
    body.Append(
        $"<p>Ngày giao mong muốn: "
        + $"{detail.ReleaseDate:dd/MM/yyyy}</p>");

    var emailSent = await EmailServiceNew.SendMailGoogleSmtp(
        _config["Email:Address"],
        _config["Email:AppPassword"],
        detail.Email,
        $"Đạt Paper — Xác nhận đơn hàng #{order.MaHoaDon}",
        body.ToString());

    TempData["OrderId"] = order.MaHoaDon;
    TempData["OrderTotal"] =
        order.TongTien.ToString("N0", vietnamese);
    TempData["EmailSent"] = emailSent;

    return RedirectToAction(nameof(CheckoutCompleted));
}

[HttpGet]
public IActionResult CheckoutCompleted()
{
    var orderId = TempData["OrderId"];

    if (orderId == null)
        return RedirectToAction(nameof(Index));

    ViewBag.OrderId = orderId;
    ViewBag.OrderTotal = TempData["OrderTotal"];
    ViewBag.EmailSent = TempData["EmailSent"] is true;

    return View();
}
}


