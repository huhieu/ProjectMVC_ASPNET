using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ProjectMVC_Ecommerce.Models;

namespace ProjectMVC_Ecommerce.Controllers
{
    public class BlogController : Controller
    {
        private readonly dbMarketsContext _context;
        public INotyfService _notyfService { get; set; }

        public BlogController(dbMarketsContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminTinDangs
        [Route("blogs.html", Name = "Blog")]
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsTinDangs = _context.TinDangs.AsNoTracking()
                .OrderByDescending(x => x.PostId);  
            PagedList<TinDang> models = new PagedList<TinDang>(lsTinDangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(models);
        }

        [Route("/tin-tuc/{Alias}-{id}.html",Name ="TinDetails")]
        public IActionResult Details(int id)
        {
            var tindang  = _context.TinDangs.AsNoTracking().SingleOrDefault(x => x.PostId == id);
            if (tindang == null)
            {
                return RedirectToAction("Index");
            }
            var newRalated = _context.TinDangs.AsNoTracking().Where(x => x.Published == true && x.PostId != id).Take(3).OrderByDescending(x => x.CreatedDate).ToList();
            ViewBag.newRalated = newRalated;
            return View(tindang);
        }
    }
}
