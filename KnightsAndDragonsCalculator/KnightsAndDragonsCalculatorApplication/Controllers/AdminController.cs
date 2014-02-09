using KnightsAndDragonsCalculatorApplication.Calculator;
using KnightsAndDragonsCalculatorApplication.Models;
using System.Web.Mvc;

namespace KnightsAndDragonsCalculatorApplication.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            AdminModel model = new AdminModel();
            model.Elements = StaticLists.GetElementsIncludingAll();
            model.Rarities = StaticLists.GetRarities();

            return View(model);
        }
	}
}