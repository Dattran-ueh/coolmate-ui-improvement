using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using DatPaper.Validation;

namespace DatPaper.Models;

public class Product
{
    public int Id { get; set; }

    [Display(Name = "Tên sản phẩm")]
    [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm.")]
    [StringLength(200, MinimumLength = 3,
        ErrorMessage = "Tên sản phẩm phải có từ 3 đến 200 ký tự.")]
    public string Name { get; set; }

    [Display(Name = "Mô tả")]
    [StringLength(2000,
        ErrorMessage = "Mô tả không được vượt quá 2000 ký tự.")]
    public string Description { get; set; }

    [Display(Name = "Ngày nhập hàng")]
    [DataType(DataType.Date)]
    [NotFutureDate]
    public DateTime DateAdded { get; set; } = DateTime.Today;

    [Display(Name = "Giá bán")]
    [Range(1000, 100000000,
        ErrorMessage = "Giá bán phải từ 1.000 đến 100.000.000 đồng.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Display(Name = "Số lượng tồn")]
    [Range(0, 100000,
        ErrorMessage = "Số lượng tồn phải từ 0 đến 100.000.")]
    public int Stock { get; set; }

    [Display(Name = "Thể loại")]
    [ExistingCategory]
    public int CategoryId { get; set; }

    [ValidateNever]
    public Category Category { get; set; }

    [Display(Name = "Đường dẫn ảnh")]
    [StringLength(500)]
    public string ImagePath { get; set; }

    [NotMapped]
    [Display(Name = "Ảnh sản phẩm")]
    public IFormFile ImageUpload { get; set; }
}