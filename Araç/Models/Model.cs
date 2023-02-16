using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Araç.Models
{
    public class Model
    {
        public int ModelId { get; set; }
        public string ModelAd { get; set; }
        public int MarkaId { get; set; }
        public virtual Marka Marka { get; set; }
    }
}