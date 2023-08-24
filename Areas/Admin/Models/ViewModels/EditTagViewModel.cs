using System.ComponentModel.DataAnnotations;

namespace E_CommerceASP.Areas.Admin.Models.ViewModels
{
	public class EditTagViewModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
