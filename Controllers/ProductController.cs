using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ProjectMVC_Ecommerce.Models;

namespace ProjectMVC_Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly dbMarketsContext _context;
        public ProductController(dbMarketsContext dbMarketsContext)
        {
            _context = dbMarketsContext;
        }
        [Route("/shop.html", Name = "ShopProduct")]

        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                var lsProducts = _context.Products.AsNoTracking()
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsProducts, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                return View(models);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        [Route("/{Alias}", Name = "ListProduct")]
        public IActionResult List(string Alias, int page = 1)
        {
            try
            {
                var pageSize = 10;
                var cats = _context.Categories.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
                var lsProducts = _context.Products.AsNoTracking().Where(x => x.CatId == cats.CatId)
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsProducts, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.cats = cats;    
                return View(models);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");

            }
            
        }
        [Route("/{Alias}-{id}.html" ,Name ="ProductDetails")]
        public IActionResult Details(int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");

            }
           
        }
    }
}
