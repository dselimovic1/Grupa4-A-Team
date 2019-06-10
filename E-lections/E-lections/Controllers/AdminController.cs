﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_lections.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_lections.Controllers
{
    public class AdminController : Controller
    {

        private ELectionsDbContext context;

        public AdminController(ELectionsDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DodajStranku()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DodajStranku(Stranka s)
        {
            return Content(s.Naziv);
        }

        public IActionResult Stranka()
        {
            return View(context.Stranka.ToList());
        }

        public async Task<IActionResult> AzurirajStrankuAsync(int? id)
        {
            var stranka = await context.Stranka.FirstOrDefaultAsync(s => s.ID == id);
            return View("Index");
        }

        public async Task<IActionResult> ObrisiStranku(int? id)
        {
            var stranka = await context.Stranka.FirstOrDefaultAsync(s => s.ID == id);
            return View("Index");
        }

        public IActionResult DetaljiStranke(int? id)
        {
            var stranka = context.Stranka.FirstOrDefault(s => s.ID == id);
            return View();
        }

        public IActionResult Lista()
        {
            var osobe = context.Osoba.ToList();
            return View(osobe);
        }
    }
}