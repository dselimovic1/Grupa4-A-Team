﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_lections.Models;

namespace E_lections.Controllers
{
    public class KandidatController : Controller
    {

        private ELectionsDbContext _context;

        public KandidatController(ELectionsDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Index", HomeController.currentlyLogged);
        }

        public IActionResult Izbori()
        {
            return View(_context.Izbor.Include(i => i.GlasackiListici).Where(i => i.Status == StatusIzbora.Aktivan).ToList());
        }

        public IActionResult Stranka()
        {
            return View(_context.Stranka.ToList());
        }

        public IActionResult NapustiStranku()
        {
            if (HomeController.currentlyLogged.StrankaId == null)
                ViewBag.Message = "Niste član niti jedne stranke!";
            else
            {
                ViewBag.Message = "Napustili ste stranku!";
                var stranka = _context.Stranka.Include(c => c.UpisiUStranku).FirstOrDefault(s => s.ID == HomeController.currentlyLogged.StrankaId);
                var osoba = stranka.UpisiUStranku.First(o => o.StrankaId == HomeController.currentlyLogged.StrankaId);
                stranka.UpisiUStranku.Remove(osoba);
                _context.SaveChanges();
                HomeController.currentlyLogged.StrankaId = null;
                HomeController.currentlyLogged.Stranka = null;
            }
            return View("Index");
        }

        public IActionResult UclaniSe(int id)
        {
            if (HomeController.currentlyLogged.StrankaId != null)
            {
                ViewBag.Message = "Već ste član stranke!";
                return View("Stranka", _context.Stranka.ToList());
            }
            var stranka = _context.Stranka.Include(c => c.UpisiUStranku).FirstOrDefault(s => s.ID == id);
            stranka.UpisiUStranku.Add(HomeController.currentlyLogged);
            _context.SaveChanges();
            return View("Index");
        }

        public IActionResult Detalji(int? id)
        {
            ViewBag.Listic = _context.Kandidat.FirstOrDefault(k => k.ID == HomeController.currentlyLogged.ID).GlasackiListicId;
            return View(_context.GlasackiListic.Include(k => k.Kandidati).Where(i => i.IzborId == id).ToList());
        }

        public IActionResult DetaljiKandidata(int? id)
        {
            return View(_context.Kandidat.Where(k => k.GlasackiListicId == id).ToList());
        }

        public IActionResult Prijava(int? id)
        {
            Kandidat k = _context.Kandidat.Include(ka => ka.GlasackiListic).Where(ka => ka.ID == HomeController.currentlyLogged.ID).FirstOrDefault();
            if(k.GlasackiListic != null)
            {
                ViewBag.Message = "Već ste prijavljeni na izbore!";
                return View("Izbori", _context.Izbor.Include(c => c.GlasackiListici).ToList());
            }
            ViewBag.Message = "Uspješno ste se prijavili na izbore!";
            var glasackiListic = _context.GlasackiListic.Include(g => g.Kandidati).FirstOrDefault(g => g.ID == id);
            glasackiListic.Kandidati.Add(k);
            _context.SaveChanges();
            return View("Izbori", _context.Izbor.Include(c => c.GlasackiListici).ToList());
        }

        public IActionResult Odjava(int? id)
        {
            Kandidat k = _context.Kandidat.Include(ka => ka.GlasackiListic).Where(ka => ka.ID == HomeController.currentlyLogged.ID).FirstOrDefault();
            var glasackiListic = _context.GlasackiListic.Include(g => g.Kandidati).FirstOrDefault(g => g.ID == id);
            glasackiListic.Kandidati.Remove(k);
            _context.SaveChanges();
            ViewBag.Message = "Odjavili ste sa izbora!";
            return View("izbori", _context.Izbor.Include(i => i.GlasackiListici).ToList());
        }

        public IActionResult LogOut()
        {
            HomeController.currentlyLogged = null;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Glasaj(int? id)
        {
            var opcije = _context.GlasackiListic.Include(g => g.Kandidati).Where(g => g.ID == id).FirstOrDefault();
            return View(opcije);
        }

        [HttpPost]
        public IActionResult Glasaj(string[] glasovi)
        {
            List<int> lista = new List<int>();
            foreach (string s in glasovi)
            {
                lista.Add(Int32.Parse(s));
            }
            foreach (int id in lista)
            {
                var osoba = _context.Kandidat.FirstOrDefault(k => k.ID == id);
                osoba.brojGlasova++;
            }
            _context.SaveChanges();
            ViewBag.PorukaGlasanje = "Hvala što ste glasali!";
            return View("Index");
        }

        public IActionResult Profil()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Profil(string profil)
        {
            var kandidat = _context.Kandidat.Include(k => k.Profil).Where(k => k.ID == HomeController.currentlyLogged.ID).FirstOrDefault();
            kandidat.Profil.Opis = profil;
            _context.SaveChanges();
            return View("Index");
        }

        public IActionResult ProfilKandidata(int? id)
        {
            var kandidat = _context.Kandidat.Include(k => k.Profil).Where(k => k.ID == id).FirstOrDefault();
            return View(kandidat.Profil);
        }

    }
}