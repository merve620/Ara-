using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Araç.Models
{
    public class Marka
    {
        public int MarkaId { get; set; }
        public string MarkaAd { get; set; }
        public List<Model> Models { get; set; }
    }
}