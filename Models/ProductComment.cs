using System;
using System.ComponentModel.DataAnnotations;

namespace baiTaplAnNinh.Models
{
    public class ProductComment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductId { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}