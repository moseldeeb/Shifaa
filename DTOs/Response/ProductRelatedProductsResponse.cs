using Shifaa.Models;

namespace Shifaa.DTOs.Response
{
    public class ProductRelatedProductsResponse
    {
        public Product Product { get; set; } = new Product();
        public IEnumerable<Product> RelatedProducts { get; set; } = new List<Product>();
    }
}
