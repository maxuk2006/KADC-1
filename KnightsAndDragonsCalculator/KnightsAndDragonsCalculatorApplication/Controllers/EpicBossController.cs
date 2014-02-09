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
    public class EpicBossController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            EpicBossModel model = new EpicBossModel();
            model.Elements = StaticLists.GetElements();
            model.GuildRanks = StaticLists.GetGuildRanks();
            model.ArmorNames = ArmorTable.Instance.GetEpicBossArmorNames();

            return View(model);
        }
	}
}