using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesProductManagement.Models;

namespace SalesProductManagement.Controllers
{
    public class StoresController : Controller
    {
        public SalesProductManagement.Models.InventoryManagement_ord1Entities db = new InventoryManagement_ord1Entities();

        // GET: Stores
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult CreateStores()
        {
            var storesList = db.Stores.ToList().Select(x => new {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            });

            return Json(storesList, JsonRequestBehavior.AllowGet);
        }


        //Add Customer
        [HttpPost]
        public JsonResult Create()
        {
            Store store = new Store();
            store.Name = Request["Name"];
            store.Address = Request["Address"];
            db.Stores.Add(store);
            db.SaveChanges();
            store = db.Stores.ToList().LastOrDefault();
            return Json(store.Id, JsonRequestBehavior.AllowGet);
        }


        //Update Customer
        //[HttpPost]
        public JsonResult GetStoreByid(int id)
        {
            Store store = db.Stores.Where(c => c.Id == id).SingleOrDefault();
            string value = string.Empty;
            return Json(store, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Update(int Id, string Name, string Address)
        {

            bool result = false;
            if (Id != 0)
            {
                var store = db.Stores.Where(c => c.Id == Id).FirstOrDefault();
                store.Name = Request["Name"];
                store.Address = Request["Address"];
                db.Entry(store).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                result = true;
            }
            //else
            //{
            //    return HttpNotFound;
            //}
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public ActionResult Delete(int id)
        {
            bool result = false;
            try
            {
                Store store = db.Stores.Find(id);
                // ProductSold productsold = db.ProductSolds.Where(p => p.CustomerId == id).FirstOrDefault();
                db.Entry(store).State = System.Data.Entity.EntityState.Deleted;
                //context.Entry(customer).State = System.Data.Entity.EntityState.Deleted;

                db.Stores.Remove(store);
                // db.ProductSolds.Remove(productsold);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            { }

            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}