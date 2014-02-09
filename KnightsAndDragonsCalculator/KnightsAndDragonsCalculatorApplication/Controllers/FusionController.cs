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
    public class FusionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            FusionModel model = new FusionModel();
            model.FusionArmorNames = ArmorTable.Instance.GetFusionArmorNames();
            model.FusableArmorNames = ArmorTable.Instance.GetFusableArmorNames();

            return View(model);
        }
	}
}