using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ProjectMVC_Ecommerce.Models;

namespace ProjectMVC_Ecommerce.Controllers
{
    public class PageController : Controller
    {
        private readonly dbMarketsContext _context;
        public INotyfService _notyfService { get; set; }

        public PageController   (dbMarketsContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminTinDangs
        

        [Route("/page/{Alias}", Name = "PageDetails")]
        public IActionResult Details(string Alias)
        {
            if(string.IsNullOrEmpty(Alias))
            {
                return RedirectToAction("Index","Home");
            }
            var page = _context.Pages.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
            if (page == null)
            {
                return RedirectToAction("Index","Home");
            }
            
            return View(page);
        }
    }
}
