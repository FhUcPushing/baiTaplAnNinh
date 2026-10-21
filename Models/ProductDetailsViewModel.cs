using baiTaplAnNinh.Models;
using System.Collections.Generic;

namespace baiTaplAnNinh.Models
{
    public class ProductDetailsViewModel
    {
        public CakeProduct Product { get; set; } = new();

        public List<CakeProduct> RecommendedProducts { get; set; }
            = new List<CakeProduct>();

        public List<ProductComment> Comments { get; set; }
            = new List<ProductComment>();
    }
}