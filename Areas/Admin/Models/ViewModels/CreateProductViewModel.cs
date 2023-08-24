using Microsoft.Build.Framework;

namespace E_CommerceASP.Areas.Admin.Models.ViewModels
{
	public class CreateProductViewModel
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public double Price { get; set; }
		[Required]
		public IFormFile ImageUrl { get; set; }
		[Required]
		public int CategoryId { get; set; }
		[Required]
		public IEnumerable<int> TagIds { get; set; }
	}
}
