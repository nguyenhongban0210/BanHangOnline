using BanHangOnline.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangOnline.Models
{
	public class ProductModel
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Yêu cầu nhập Tên Sản phẩm")]
		public string Name { get; set; }
		
		[Required(ErrorMessage = "Yêu cầu nhập mô tả Sản phẩm")]
		public string Description { get; set; }

		public string Slug { get; set; }

		[Required(ErrorMessage = "Yêu cầu nhập Giá Sản phẩm")]
		[Range(0.01, double.MaxValue)]
		public decimal Price { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Số lượng")]
        public int Quantity { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn thương hiệu")]
        public int BrandId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoriesId { get; set; }

		public CategoriesModel Categories { get; set; }
		public BrandModel Brand { get; set; }

		public string Image { get; set; } = "noimage.jpg";

		[NotMapped]
		[FileExtension]
        public IFormFile ImageUpload { get; set; }
	}
}
