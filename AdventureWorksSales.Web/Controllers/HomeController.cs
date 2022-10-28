using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using AdventureWorksSales.Web.Models;

namespace AdventureWorksSales.Web.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            dbObj conObj = new dbObj();
            SalesAnalysisModel salesAnalysisModel=new SalesAnalysisModel();
            try { 
                conObj.cmdObj=new SqlCommand("SELECT SUM(OrderQty) AS Qtot FROM SalesOrder",new SqlConnection(dbObj.conStr)); 
                conObj.cmdObj.Connection.Open();
                conObj.drObj=conObj.cmdObj.ExecuteReader();
                if (conObj.drObj.Read()) salesAnalysisModel.TotalOrders = Convert.ToUInt32(conObj.drObj["Qtot"]);
                conObj.cmdObj.Dispose();
                conObj.cmdObj=new SqlCommand("SELECT MAX(LineTotal) AS Linetot FROM SalesOrder",new SqlConnection(dbObj.conStr));
                conObj.cmdObj.Connection.Open();
                conObj.drObj = conObj.cmdObj.ExecuteReader();
                if (conObj.drObj.Read()) salesAnalysisModel.HighestLineTotal = Convert.ToDecimal(conObj.drObj["Linetot"]);
                conObj.cmdObj.Dispose();
                conObj.cmdObj = new SqlCommand("SELECT ProductID  FROM Product WHERE Name=@name", new SqlConnection(dbObj.conStr));
                conObj.cmdObj.Parameters.AddWithValue("@name", "Front Brakes");
                conObj.cmdObj.Connection.Open();
                conObj.drObj = conObj.cmdObj.ExecuteReader();
                if (conObj.drObj.Read()) {
                    string itemId = conObj.drObj["ProductID"]?.ToString() ?? default;
                    if (itemId != null) {
                        conObj.cmdObj.Dispose();
                        conObj.cmdObj = new SqlCommand("SELECT SUM(LineTotal) AS BrakeTot FROM SalesOrder WHERE ProductID=@pID ", new SqlConnection(dbObj.conStr));
                        conObj.cmdObj.Parameters.AddWithValue("@pID",itemId);
                        conObj.cmdObj.Connection.Open();
                        conObj.drObj = conObj.cmdObj.ExecuteReader();
                        if(conObj.drObj.Read()) salesAnalysisModel.FrontBrakesSalesTotal= Convert.ToDecimal(conObj.drObj["BrakeTot"]);
                    }
                }
            } catch { } finally {
                if (conObj.cmdObj != null && conObj.cmdObj.Connection != null) conObj.cmdObj.Connection.Close();
                conObj = null;
            }
            ViewBag.vData = salesAnalysisModel;
            return View();
        }

    }
}