namespace E_CommerceASP.Models.Entities
{
	public class Tag : Entity
	{
		public string Name { get; set; }
		public virtual IEnumerable<ProductTag> ProductTags { get; set; }
	}
}
