using MonolithicSPA.Models.DomainModels.ProductAggregate;

namespace MonolithicSPA.Models.DomainModels.ProductType
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
