using System.ComponentModel.DataAnnotations;

namespace E_CommerceASP.Models.ViewModels
{
	public class ProductViewModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public double Price { get; set; }
		[Required]
		public int Stock { get; set; }
		[Required]
		public string ImageUrl { get; set; }
		[Required]
		public string CategoryName { get; set; }
	}
}
