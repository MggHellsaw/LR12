using LR12.Models;
using Microsoft.AspNetCore.Mvc;

namespace lr12.Controllers
{
    public class CompanyController : Controller
    {
        private CompanyContext _context;

        public CompanyController(CompanyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var companies = _context.Companies.ToList();
            return View(companies);
        }

    }
}
