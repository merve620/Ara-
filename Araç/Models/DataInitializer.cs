using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Araç.Models
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var sehirler = new List<Sehir>()
            {
                new Sehir() {SehirAd="İSTANBUL"},
                new Sehir() {SehirAd="ANKARA"},
                new Sehir() {SehirAd="İZMİR"}
            };
            foreach (var item in sehirler)
            {
                context.Sehirs.Add(item);
            }
            context.SaveChanges();
            var durum = new List<Durum>()
            {
                new Durum() {DurumAd="KİRALIK"},
                new Durum() {DurumAd="SATILIK"}
            };
            foreach (var item in durum)
            {
                context.Durums.Add(item);
            }
            context.SaveChanges();
            var marka = new List<Marka>()
            {
                new Marka() {MarkaAd="BMW"},
                new Marka() {MarkaAd="MERCEDES"},
                new Marka() {MarkaAd="AUDİ"}
            };
            foreach (var item in marka)
            {
                context.Markas.Add(item);
            }
            context.SaveChanges();
            var model = new List<Model>()
            {
                new Model() { ModelAd ="520",MarkaId=1},
                new Model() { ModelAd ="X5",MarkaId=1},
                new Model() { ModelAd ="A180",MarkaId=2},
                new Model() { ModelAd ="Q7",MarkaId=3}
            };
            foreach (var item in model)
            {
                context.Models.Add(item);
            }
            context.SaveChanges();
            var ilan = new List<Ilan>()
            { 
                new Ilan() {MarkaId=1,Aciklama="Araç Temiz",IlanNo="A125",Fiyat=500000,Tarih="01/01/2023",Kilometre=10000,ModelYili=2020,YakitTuru="Benzin",VitesTuru="Otomatik",DurumId=1,ModelId=1,Username="mervesagdic",SehirId=1,Telefon="123456789"},
                new Ilan() {MarkaId=3, Aciklama="Araç Temiz Kazasız",IlanNo="A150",Fiyat=500000,Tarih="01/02/2023",Kilometre=50000,ModelYili=2015,YakitTuru="LPG",VitesTuru="Düz Vites",DurumId=2,ModelId=4,Username="meryem",SehirId=2,Telefon="123456789"}
            };
            foreach (var item in ilan)
            {
                context.Ilans.Add(item);
            }
            context.SaveChanges();
            var resim = new List<Resim>()
            {
                new Resim() {ResimAd="a1.jpg",IlanId=1},
                new Resim() {ResimAd="a2.jpg",IlanId=1},
                new Resim() {ResimAd="a3.jpg",IlanId=2}
            };
            foreach (var item in resim) 
            { 
                context.Resims.Add(item);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}