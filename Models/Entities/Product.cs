using System.ComponentModel.DataAnnotations;

namespace E_CommerceASP.Models.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual IEnumerable<ProductTag> ProductTags { get; set; }
    }
}
