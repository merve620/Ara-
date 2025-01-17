﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Araç.Models;

namespace Araç.Controllers
{
    public class IlanController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Ilan
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var ilans = db.Ilans.Where(i=>i.Username==username).Include(i => i.Durum).Include(i => i.Model).Include(i => i.Sehir);
            return View(ilans.ToList());
        }
        public List<Marka> MarkaGetir()
        {
            List<Marka> markalar=db.Markas.ToList();
            return markalar;
        }
        public ActionResult ModelGetir(int MarkaId) 
        { 
            List<Model> modellist=db.Models.Where(x=>x.MarkaId==MarkaId).ToList();
            ViewBag.modellistesi = new SelectList(modellist, "ModelId", "ModelAd");
            return PartialView("ModelPartial"); 
        } 
        // GET: Ilan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        // GET: Ilan/Create
        public ActionResult Create()
        {
            ViewBag.markalist=new SelectList(MarkaGetir(),"MarkaId","MarkaAd");
            ViewBag.DurumId = new SelectList(db.Durums, "DurumId", "DurumAd");        
            ViewBag.SehirId = new SelectList(db.Sehirs, "SehirId", "SehirAd");
            return View();
        }
        public ActionResult Images(int id)
        {
            var ilan=db.Ilans.Where(i=>i.IlanId==id).ToList();
            var rsm = db.Resims.Where(i => i.IlanId == id).ToList();
            ViewBag.rsm = rsm;
            ViewBag.ilan=ilan;
            return View();
        }
        [HttpPost]
        public ActionResult Images(int id,HttpPostedFileBase file)
        {
            string path = Path.Combine("/Content/image/" + file.FileName);
            file.SaveAs(Server.MapPath(path));
            Resim rsm=new Resim();
            rsm.ResimAd=file.FileName.ToString();
            rsm.IlanId=id;
            db.Resims.Add(rsm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // POST: Ilan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IlanId,IlanNo,Aciklama,Fiyat,Tarih,Kilometre,ModelYili,YakitTuru,VitesTuru,Username,Telefon,DurumId,MarkaId,ModelId,SehirId")] Ilan ilan)
        {
            if (ModelState.IsValid)
            {
                ilan.Username=User.Identity.Name;
                db.Ilans.Add(ilan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DurumId = new SelectList(db.Durums, "DurumId", "DurumAd", ilan.DurumId);
            ViewBag.markalist = new SelectList(MarkaGetir(), "MarkaId", "MarkaAd");
            ViewBag.SehirId = new SelectList(db.Sehirs, "SehirId", "SehirAd", ilan.SehirId);
            return View(ilan);
        }

        // GET: Ilan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            ViewBag.markalist = new SelectList(MarkaGetir(), "MarkaId", "MarkaAd");
            ViewBag.DurumId = new SelectList(db.Durums, "DurumId", "DurumAd", ilan.DurumId);
            ViewBag.ModelId = new SelectList(db.Models, "ModelId", "ModelAd", ilan.ModelId);
            ViewBag.SehirId = new SelectList(db.Sehirs, "SehirId", "SehirAd", ilan.SehirId);
            return View(ilan);
        }

        // POST: Ilan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IlanId,IlanNo,Aciklama,Fiyat,Tarih,Kilometre,ModelYili,YakitTuru,VitesTuru,Username,Telefon,DurumId,MarkaId,ModelId,SehirId")] Ilan ilan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ilan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DurumId = new SelectList(db.Durums, "DurumId", "DurumAd", ilan.DurumId);
            ViewBag.markalist = new SelectList(MarkaGetir(), "MarkaId", "MarkaAd");
            ViewBag.SehirId = new SelectList(db.Sehirs, "SehirId", "SehirAd", ilan.SehirId);
            return View(ilan);
        }

        // GET: Ilan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        // POST: Ilan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ilan ilan = db.Ilans.Find(id);
            db.Ilans.Remove(ilan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
