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
    public class LogController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            LogModel model = new LogModel();
            model.Logs = LogTable.Instance.GetLogs();

            return View(model);
        }
	}
}