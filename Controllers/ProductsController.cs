using Microsoft.AspNetCore.Mvc;
using SkaftoBageriWMS.Data;
using SkaftoBageriWMS.Models;
using System.Linq;

namespace SkaftoBageriWMS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}
