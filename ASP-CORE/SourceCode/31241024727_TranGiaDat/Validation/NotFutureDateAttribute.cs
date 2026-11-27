using System.ComponentModel.DataAnnotations;

namespace DatPaper.Validation;

public class NotFutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        if (value is DateTime date && date.Date <= DateTime.Today)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(
            "Ngày nhập hàng không được lớn hơn ngày hiện tại.");
    }
}