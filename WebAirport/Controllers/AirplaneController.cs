﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAirport.Data;
using WebAirport.Models;
using System.Data.Entity;

namespace WebAirport.Controllers
{
    public class AirplaneController : Controller
    {
        private AirportContext db = new AirportContext();
        // GET: Airplane
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var TypeAirplanes = db.TypeAirplanes.ToList();

            if (TypeAirplanes.Count != 0)
            {
                ViewBag.TypeAirplanes = TypeAirplanes;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(Airplane airplane)
        {
            db.Airplanes.Add(airplane);
            db.SaveChanges();

            return RedirectToAction("ListAirplane");
        }

        public ActionResult ListAirplane()
        {
            var Airplanes = db.Airplanes.
                Include(c => c.TypeAirplane).
                ToList();

            return View(Airplanes);
        }

        public ActionResult Edit(int id)
        {
            var airplane = db.Airplanes.Find(id);

            ViewBag.TypeAirplanes = db.TypeAirplanes.ToList();

            return View(airplane);
        }

        [HttpPost]
        public ActionResult Edit(Airplane airplane)
        {
            db.Entry(airplane).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListAirplane");
        }
    }
}