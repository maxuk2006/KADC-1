using KnightsAndDragonsCalculatorApplication.Calculator;
using KnightsAndDragonsCalculatorApplication.Calculator.Tables;
using KnightsAndDragonsCalculatorApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnightsAndDragonsCalculatorApplication.Controllers
{
    public class ArmorDataController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ArmorDataModel model = new ArmorDataModel();
            model.Armors = ArmorTable.Instance.GetArmors();

            return View(model);
        }
	}
}