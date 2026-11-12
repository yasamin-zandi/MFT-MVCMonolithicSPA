using MonolithicSPA.Models.DomainModels.ProductType;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonolithicSPA.Models.DomainModels.ProductAggregate
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }


        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }


    }
}
