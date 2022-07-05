using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using test_admission.Models;

namespace test_admission.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Series([FromBody] Serie serie)
        {
            int x_value = serie.x_value;
            int y_value = serie.y_value;
            int result = -1;
            if (x_value > 0 && x_value <= 255 && y_value > 0 && y_value <= 255)
            {
                result = getSerieNumber(x_value) + getSerieNumber(y_value);

                int getSerieNumber(int value) {
                    int mod_result = value % 2;
                    if (mod_result == 0)
                    {
                        return 6 - (value - 2);
                    }
                    else
                    {
                        return 7 + (value / 2);
                    }
                }
            }

            return Json(new {result});
        }

        public ActionResult WeekDates([FromBody] WeekDate weekDate)
        {
            DateTime date = DateTime.Parse(weekDate.date);
            int day = weekDate.day == 7 ? 0 : weekDate.day;
            string[] dates = new string[] { };

            while(dates.Length < 10)
            {
                date = date.AddDays(1);
                if(day == (int) date.DayOfWeek)
                {
                    dates = dates.Concat(new string[] { date.ToString("yyyy-MM-dd") }).ToArray();
                }
            }

            return Json(new { dates });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}