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
    public class LevelDataController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            LevelDataModel model = new LevelDataModel();
            model.Levels = LevelTable.Instance.GetLevels();

            return View(model);
        }
	}
}