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
    public class EnhancementController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            EnhancementModel model = new EnhancementModel();
            model.TargetArmorNames = ArmorTable.Instance.GetTargetArmorNames();
            model.FeederArmorNames = ArmorTable.Instance.GetFeederArmorNames();
            model.TargetArmorMaxLevels = StaticLists.GetTargetArmorMaxLevels();
            model.BaseFeedCosts = StaticLists.GetBaseFeedCosts();

            return View(model);
        }
    }
}
