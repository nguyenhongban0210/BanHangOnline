using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.Repository.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg" };
                bool result = extensions.Any(x => extension.EndsWith(x));

                if (!result)
                {
                    return new ValidationResult("Hãy chọn ảnh có đuôi mở rộng là jpg!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
