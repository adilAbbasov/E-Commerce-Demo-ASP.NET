using System.ComponentModel.DataAnnotations;

namespace E_CommerceASP.Areas.Admin.Models.ViewModels
{
	public class CreateTagViewModel
	{
		[Required]
		public string Name { get; set; }
	}
}
