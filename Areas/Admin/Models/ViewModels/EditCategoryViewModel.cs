using Microsoft.Build.Framework;

namespace E_CommerceASP.Areas.Admin.Models.ViewModels
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
