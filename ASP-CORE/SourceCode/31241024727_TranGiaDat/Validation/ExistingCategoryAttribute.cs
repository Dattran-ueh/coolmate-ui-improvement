using System.ComponentModel.DataAnnotations;
using DatPaper.Data;

namespace DatPaper.Validation;

public class ExistingCategoryAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        if (value is not int categoryId || categoryId <= 0)
        {
            return new ValidationResult("Vui lòng chọn thể loại.");
        }

        var context = validationContext.GetService(
            typeof(ApplicationDbContext)) as ApplicationDbContext;

        if (context == null)
        {
            return new ValidationResult(
                "Không thể kiểm tra thể loại lúc này.");
        }

        if (!context.Categories.Any(c => c.Id == categoryId))
        {
            return new ValidationResult("Thể loại không tồn tại.");
        }

        return ValidationResult.Success;
    }
}