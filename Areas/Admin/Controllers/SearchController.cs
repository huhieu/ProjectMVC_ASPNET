using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC_Ecommerce.Models;
using System.Xml.Linq;

namespace ProjectMVC_Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class SearchController : Controller
    {
        private readonly dbMarketsContext _context;

        public SearchController(dbMarketsContext context)
        {
            _context = context;
        }
        // GET: Search/FindProduct
        [HttpPost]
        public IActionResult FindProduct(string keyword, int? categoryId)
        {
            List<Product> ls = new List<Product>();
            IQueryable<Product> filteredProducts = _context.Products
                .AsNoTracking()
                .Include(a => a.Cat);

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                filteredProducts = filteredProducts.Where(x => x.CatId == categoryId);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                filteredProducts = filteredProducts.Where(x => x.ProductName.Contains(keyword));
            }

            ls = filteredProducts
                .OrderByDescending(x => x.ProductName)
                .Take(10)
                .ToList();

            return PartialView("ListProductsSearchPartial", ls);
        }

    }
}