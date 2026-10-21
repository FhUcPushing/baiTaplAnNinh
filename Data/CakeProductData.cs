using baiTaplAnNinh.Models;
using System.Collections.Generic;

namespace baiTaplAnNinh.Data
{
    public static class CakeProductData
    {
        public static List<CakeProduct> GetProducts()
        {
            return new List<CakeProduct>
            {
                new CakeProduct
                {
                    Id = "1",
                    Name = "Bánh Mousse Hawai",
                    Price = 100000,
                    ImageUrl = "images/SanPham/banhMousseHawaii_SP.png",
                    Category = "Bánh Mousse",
                    Description = "Mousse dừa bùi thơm, mịn tan trong miệng."
                },
                new CakeProduct
                {
                    Id = "2",
                    Name = "Bánh Kem Nơ Hồng",
                    Price = 120000,
                    ImageUrl = "images/SanPham/banhkemnohong_SP.png",
                    Category = "Bánh Sinh Nhật",
                    Description = "Bánh kem cốt vani cùng mứt việt quất."
                },
                new CakeProduct
                {
                    Id = "4",
                    Name = "Bánh Cưới 3 tầng",
                    Price = 500000,
                    ImageUrl = "images/SanPham/banh3tang_SP.jpg",
                    Category = "Bánh Sinh Nhật",
                    Description = "Chiếc bánh dành cho ngày đặc biệt."
                },
                new CakeProduct
                {
                    Id = "5",
                    Name = "Bánh Mousse Chanh Leo",
                    Price = 450000,
                    ImageUrl = "images/SanPham/banhchanhleo_TC.jpg",
                    Category = "Bánh Mousse",
                    Description = "Mousse chanh leo chua ngọt, tươi mát."
                },
                new CakeProduct
                {
                    Id = "3",
                    Name = "Bánh Su Kem",
                    Price = 10000,
                    ImageUrl = "images/SanPham/sukem_SP.jpg",
                    Category = "Bánh Tươi",
                    Description = "Bánh su kem mềm thơm."
                },
                new CakeProduct
                {
                    Id = "15",
                    Name = "Bánh Croissant Bơ",
                    Price = 29000,
                    ImageUrl = "images/SanPham/croissant_TC.jpg",
                    Category = "Bánh Tươi",
                    Description = "Vỏ giòn xốp, thơm lừng vị bơ."
                },
                new CakeProduct
                {
                    Id = "16",
                    Name = "Bánh Bông Lan Trứng Muối",
                    Price = 35000,
                    ImageUrl = "images/SanPham/bonglan_SP.jpg",
                    Category = "Bánh Tươi",
                    Description = "Bông lan mềm, sốt trứng muối béo mặn."
                },
                new CakeProduct
                {
                    Id = "17",
                    Name = "Bánh Cupcake Vani",
                    Price = 25000,
                    ImageUrl = "images/SanPham/cupcakevani_SP.jpg",
                    Category = "Bánh Tươi",
                    Description = "Cupcake nhỏ xinh phủ kem vani."
                }
            };
        }
    }
}