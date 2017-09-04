using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRTSekreterya.Models.getModels
{
    public class getPTK
    {      
        public int pkID { get; set; }
        public Nullable<int> pkKisiID { get; set; }
        public Nullable<int> pkPlanID { get; set; }
        public Nullable<bool> pkisSource { get; set; }
        public string pkKisiAdi { get; set; }
    }
}