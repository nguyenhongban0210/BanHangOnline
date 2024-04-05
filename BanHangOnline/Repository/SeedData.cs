using BanHangOnline.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BanHangOnline.Repository
{
	public class SeedData
	{
		public static void SeedingData(DataContext _context)
		{
			//_context.Database.Migrate();
			//if(!_context.Products.Any())
			//{
			//	CategoriesModel phone = new CategoriesModel { Name = "phone", Slug = "phone", Description = "phone is best", Status = 1 };
			//	CategoriesModel tablet = new CategoriesModel { Name = "tablet", Slug = "tablet", Description = "tablet is best", Status = 1 };
			//	BrandModel oppo = new BrandModel { Name = "OPPPO", Slug = "oppo", Description = "Oppo is large  Brand in the would", Status = 1 };
			//	BrandModel samsung = new BrandModel { Name = "SAMSUNG", Slug = "samsung", Description = "SamSung is large  Brand in the would", Status = 1 };
			//	_context.Products.AddRange(
			//		new ProductModel { Name = "Oppo Reno 8 5G", Slug = "phone", Description = "phone is best", Image = "1.jpg", Categories = phone, Brand = oppo, Price = 2000000, Quantity = 49},
			//		new ProductModel { Name = "Galaxy Tab A8", Slug = "tablet", Description = "tablet is best", Image = "2.jpg", Categories = tablet, Brand = samsung, Price = 3000000, Quantity = 49}
			//	);
			//	_context.SaveChanges();
			//}
		}
	}
}
