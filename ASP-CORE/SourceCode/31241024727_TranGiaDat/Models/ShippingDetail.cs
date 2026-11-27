using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DatPaper.Models;

public class ShippingDetail : IValidatableObject
{
    public int ID { get; set; }

    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Họ và tên phải có từ 2 đến 100 ký tự.")]
    public string Name { get; set; }

    [Display(Name = "Địa chỉ nhận hàng")]
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận hàng.")]
    [StringLength(300, MinimumLength = 10,
        ErrorMessage = "Địa chỉ phải có từ 10 đến 300 ký tự.")]
    public string Address { get; set; }

    [Display(Name = "Email nhận xác nhận")]
    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    [StringLength(254)]
    public string Email { get; set; }

    [Display(Name = "Số điện thoại")]
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [RegularExpression(@"^0[0-9]{9}$",
        ErrorMessage = "Nhập số điện thoại gồm 10 chữ số, bắt đầu bằng 0.")]
    public string Mobile { get; set; }

    [Display(Name = "Ngày giao hàng mong muốn")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
        = DateTime.Today.AddDays(3);

    public int MaHoaDon { get; set; }

    [ValidateNever]
    public HoaDon HoaDonObj { get; set; }

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (ReleaseDate.Date < DateTime.Today)
        {
            yield return new ValidationResult(
                "Ngày giao hàng không được trước ngày hiện tại.",
                new[] { nameof(ReleaseDate) });
        }
    }
}