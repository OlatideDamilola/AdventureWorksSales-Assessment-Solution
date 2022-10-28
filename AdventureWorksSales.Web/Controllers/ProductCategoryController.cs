using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using AdventureWorksSales.Web.Models;

namespace AdventureWorksSales.Web.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        dbObj conObj = new dbObj();
        ProductCategoryModel ProdCatModel;
        List<ProductCategoryModel> AllProductCategory;
        public ActionResult Index() {
           AllProductCategory = new List<ProductCategoryModel>();

            try {
                conObj.cmdObj = new SqlCommand("SELECT* FROM ProductCategory", new SqlConnection(dbObj.conStr));
                conObj.cmdObj.Connection.Open();
                conObj.drObj = conObj.cmdObj.ExecuteReader();
                while (conObj.drObj.Read()) {
                    ProdCatModel = new ProductCategoryModel {
                        Name = conObj.drObj["Name"].ToString(),
                        ProductCategoryID = Convert.ToInt16(conObj.drObj["ProductCategoryID"]),
                        ModifiedDate = Convert.ToDateTime(conObj.drObj["ModifiedDate"])
                    };
                    AllProductCategory.Add(ProdCatModel);   
                }
            } catch { } finally {
                if (conObj.cmdObj != null && conObj.cmdObj.Connection != null) conObj.cmdObj.Connection.Close();
                conObj = null;
            }
            ViewBag.vData = AllProductCategory;
            return View();
        }

        [HttpPost]
        public ActionResult Addnew(string proCatName) {
            if (!string.IsNullOrEmpty(proCatName)) {
                try {
                    conObj.cmdObj = new SqlCommand("INSERT INTO ProductCategory(Name,rowguid,ModifiedDate)VALUES(@Name,@rowguid,@ModifiedDate)", new SqlConnection(dbObj.conStr));
                    conObj.cmdObj.Parameters.AddWithValue("@Name", proCatName.Trim());
                    conObj.cmdObj.Parameters.AddWithValue("@rowguid",Guid.NewGuid());
                    conObj.cmdObj.Parameters.AddWithValue("@ModifiedDate",DateTime.Now);
                    conObj.cmdObj.Connection.Open();
                    conObj.cmdObj.ExecuteNonQuery();                    
                } catch { } finally {
                    if (conObj.cmdObj != null && conObj.cmdObj.Connection != null) conObj.cmdObj.Connection.Close();
                    conObj = null;
                }
            }
            return RedirectToAction("Index");
        }

        
        public ActionResult Edit(string id) {
            if (!string.IsNullOrEmpty(id)) {
                try {
                    conObj.cmdObj = new SqlCommand("SELECT Name FROM ProductCategory WHERE ProductCategoryID=@ProductCategoryID", new SqlConnection(dbObj.conStr));
                    conObj.cmdObj.Parameters.AddWithValue("@ProductCategoryID", id.Trim());
                    conObj.cmdObj.Connection.Open();
                    conObj.drObj = conObj.cmdObj.ExecuteReader();
                    if (conObj.drObj.Read()) ViewBag.vData = conObj.drObj["Name"].ToString()+"-"+id;
                } catch { } finally {
                    if (conObj.cmdObj != null && conObj.cmdObj.Connection != null) conObj.cmdObj.Connection.Close();
                    conObj = null;
                }
                return View();                
            }else return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Update( string proCatName,string editId) {
            if (!string.IsNullOrEmpty(proCatName)) {
                try {
                    conObj.cmdObj = new SqlCommand("UPDATE ProductCategory SET Name = @Name, ModifiedDate=@ModifiedDate WHERE ProductCategoryID=@ProductCategoryID; ", new SqlConnection(dbObj.conStr));
                    conObj.cmdObj.Parameters.AddWithValue("@Name", proCatName.Trim());
                    conObj.cmdObj.Parameters.AddWithValue("@ProductCategoryID",editId);
                    conObj.cmdObj.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    conObj.cmdObj.Connection.Open();
                    conObj.cmdObj.ExecuteNonQuery();
                } catch { } finally {
                    if (conObj.cmdObj != null && conObj.cmdObj.Connection != null) conObj.cmdObj.Connection.Close();
                    conObj = null;
                }
            }
            return RedirectToAction("Index");
        }
    }
}