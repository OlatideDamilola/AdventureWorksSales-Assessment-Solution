using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksSales.Web.Models {
    public class SalesAnalysisModel {
        public virtual UInt32 TotalOrders{ get; set; } =0;
        public virtual  decimal HighestLineTotal { get; set; } = 0;
        public virtual  decimal FrontBrakesSalesTotal { get; set; } = 0;
    }
}