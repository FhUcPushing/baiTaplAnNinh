using baiTaplAnNinh.Data;
using baiTaplAnNinh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Text;

namespace baiTaplAnNinh.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Danh sách và tìm kiếm bánh
        public IActionResult Index(string? q, string? loai)
        {
            q = (q ?? "").Trim();

            if (q.Length > 100)
            {
                q = q.Substring(0, 100);
            }

            if (loai != "banh_kem" &&
                loai != "banh_tuoi" &&
                loai != "banh_mi")
            {
                loai = "";
            }

            var products = CakeProductData.GetProducts();

            if (loai == "banh_kem")
            {
                products = products.Where(p =>
                    p.Category == "Bánh Kem" ||
                    p.Category == "Bánh Sinh Nhật" ||
                    p.Category == "Bánh Mousse").ToList();
            }
            else if (loai == "banh_tuoi")
            {
                products = products
                    .Where(p => p.Category == "Bánh Tươi")
                    .ToList();
            }
            else if (loai == "banh_mi")
            {
                products = products
                    .Where(p => p.Category == "Bánh Mì")
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                string keyword = BoDau(q);

                products = products
                    .Where(p => BoDau(p.Name).Contains(keyword))
                    .ToList();
            }

            ViewData["Query"] = q;
            ViewData["Category"] = loai;

            return View(products);
        }

        // Chi tiết bánh
        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var products = CakeProductData.GetProducts();

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductDetailsViewModel
            {
                Product = product,

                RecommendedProducts = products
                    .Where(p => p.Id != product.Id)
                    .Take(3)
                    .ToList(),

                Comments = await _context.ProductComments
                    .Where(c => c.ProductId == id)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync()
            };

            return View(model);
        }

        // Nhận và lưu bình luận
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(
            string? productId,
            string? userName,
            string? content)
        {
            productId = (productId ?? "").Trim();
            userName = (userName ?? "").Trim();
            content = (content ?? "").Trim();

            bool productExists = CakeProductData.GetProducts()
                .Any(p => p.Id == productId);

            if (!productExists)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(userName) ||
                userName.Length > 50 ||
                string.IsNullOrWhiteSpace(content) ||
                content.Length > 1000)
            {
                TempData["CommentError"] =
                    "Tên phải có tối đa 50 ký tự và nội dung phải có từ 1 đến 1000 ký tự.";

                return RedirectToAction(
                    nameof(Details), new { id = productId });
            }

            var comment = new ProductComment
            {
                ProductId = productId,
                UserName = userName,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            _context.ProductComments.Add(comment);
            await _context.SaveChangesAsync();

            TempData["CommentSuccess"] = "Gửi bình luận thành công!";

            return RedirectToAction(
                nameof(Details), new { id = productId });
        }

        // Bỏ dấu tiếng Việt để tìm kiếm thuận tiện
        private static string BoDau(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            string normalized = text
                .ToLowerInvariant()
                .Replace('đ', 'd')
                .Normalize(NormalizationForm.FormD);

            var result = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c)
                    != UnicodeCategory.NonSpacingMark)
                {
                    result.Append(c);
                }
            }

            return result.ToString()
                .Normalize(NormalizationForm.FormC);
        }
    }
}
