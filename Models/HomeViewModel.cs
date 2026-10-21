using System.Collections.Generic;

namespace baiTaplAnNinh.Models
{
    public class HomeViewModel
    {
        public List<CakeProduct> FeaturedProducts { get; set; }
            = new List<CakeProduct>();

        public List<CakeProduct> FreshCakes { get; set; }
            = new List<CakeProduct>();
    }
}