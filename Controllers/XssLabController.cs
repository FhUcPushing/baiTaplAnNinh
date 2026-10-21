using baiTaplAnNinh.Data;
using baiTaplAnNinh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SweetCakeMvc.Controllers
{
    public class XssLabController : Controller
    {
        private readonly ApplicationDbContext _context;

        public XssLabController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Trang tổng hợp các bài thực hành
        public IActionResult Index()
        {
            return View();
        }

        // Reflected XSS: dữ liệu được phản hồi từ query string
        [HttpGet]
        public IActionResult Reflected(string? query)
        {
            query ??= string.Empty;

            if (query.Length > 1000)
            {
                query = query.Substring(0, 1000);
            }

            ViewData["Query"] = query;

            return View();
        }

        // DOM-based XSS: xử lý dữ liệu ở trình duyệt
        [HttpGet]
        public IActionResult Dom()
        {
            return View();
        }

        // Stored XSS: đọc bình luận đã lưu
        [HttpGet]
        public async Task<IActionResult> Stored(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = "1";
            }

            var product = CakeProductData.GetProducts()
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductDetailsViewModel
            {
                Product = product,

                Comments = await _context.ProductComments
                    .Where(c => c.ProductId == id)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync(),

                RecommendedProducts = new List<CakeProduct>()
            };

            return View(model);
        }

        // Lưu bình luận phục vụ bài thực hành Stored XSS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStoredComment(
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
                TempData["LabError"] =
                    "Tên phải có tối đa 50 ký tự và nội dung từ 1 đến 1000 ký tự.";

                return RedirectToAction(
                    nameof(Stored), new { id = productId });
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

            TempData["LabMessage"] =
                "Đã lưu bình luận vào SQL Server.";

            return RedirectToAction(
                nameof(Stored), new { id = productId });
        }
    }
}
