using AceInternship.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AceInternship.MvcApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            ApexChartPieChartResponseModel model = new ApexChartPieChartResponseModel()
            {
                Series = new List<int> { 44, 55, 13, 43, 22 },

                Lables = new List<string> { "Team A", "Team B", "Team C", "Team D", "Team E" }
            };
            return View(model);
        }
        public IActionResult DashedLine()
        {
            return View();
        }
    }
}
