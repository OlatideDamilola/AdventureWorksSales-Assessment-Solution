using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorksSales.Web.Models {
    public class ProductCategoryModel {
        public virtual int ProductCategoryID { get; set; } = 0;
        public virtual string Name { get; set; } = string.Empty;
        public virtual DateTime ModifiedDate { get; set; } =DateTime.Now;

    }
}