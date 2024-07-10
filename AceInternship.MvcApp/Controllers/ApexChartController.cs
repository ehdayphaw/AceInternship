using Microsoft.AspNetCore.Mvc;

namespace AceInternship.MvcApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
