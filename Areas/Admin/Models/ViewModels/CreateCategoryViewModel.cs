using Microsoft.Build.Framework;

namespace E_CommerceASP.Areas.Admin.Models.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
