namespace E_CommerceASP.Models.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
	}
}
