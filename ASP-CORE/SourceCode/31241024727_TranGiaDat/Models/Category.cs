using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DatPaper.Models;

public class Category
{
    public int Id { get; set; }

    [Display(Name = "Tên thể loại")]
    [Required(ErrorMessage = "Vui lòng nhập tên thể loại.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Tên thể loại phải có từ 2 đến 100 ký tự.")]
    public string Name { get; set; }

    [ValidateNever]
    public ICollection<Product> Products { get; set; }
        = new List<Product>();
}