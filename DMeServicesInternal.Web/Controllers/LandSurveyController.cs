using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMeServicesInternal.Web.Controllers
{
    public class LandSurveyController : Controller
    {
        // GET: LandSurvey
        public ActionResult Index()
        {
            return View();
        }

        // GET: LandSurvey/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LandSurvey/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LandSurvey/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LandSurvey/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LandSurvey/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: LandSurvey/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LandSurvey/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
