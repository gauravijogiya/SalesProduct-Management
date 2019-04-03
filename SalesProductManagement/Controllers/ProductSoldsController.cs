using SalesProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SalesProductManagement.Controllers
{
    public class ProductSoldsController : Controller
    {
        public SalesProductManagement.Models.InventoryManagement_ord1Entities db = new InventoryManagement_ord1Entities();
        // GET: ProductSolds
        public ActionResult Index()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName");
            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name");
            return View();

        }


        public JsonResult CreateSalestList()
        {
            var productSolds = db.ProductSolds.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Store).ToList().Select(x => new
            {
                id = x.Id,
                customername = x.Customer.Name,
                productname = x.Product.ProductName,
                storename = x.Store.Name,
                datesold = x.DateSold.ToString()
            }); ;
            return Json(productSolds.ToList(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult Create(ProductSold productSold)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                db.ProductSolds.Add(productSold);
                db.SaveChanges();
                result = true;

            }
            var productId = db.ProductSolds.ToList().LastOrDefault();


            //ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", productSold.CustomerId);
            // ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", productSold.ProductId);
            //ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name", productSold.StoreId);
            return Json(productId, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetsaleByid(int id)
        {
            var productid = db.ProductSolds.Where(c => c.Id == id).SingleOrDefault();
            
            return Json(productid.Id, JsonRequestBehavior.AllowGet);
        }


      //  [HttpPost]
        public JsonResult Update(ProductSold productsold)
        {

            bool result = false;
                var product = db.ProductSolds.Where(p => p.Id == productsold.Id).FirstOrDefault();
                //customer.Name = Request["Name"];
                //customer.Address = Request["Address"];
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                result = true;
            
            //else
            //{
            //    return HttpNotFound;
            //}
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Deleting saless
        [HttpGet]
        public JsonResult DeleteSales(int? id)
        {
            bool result = false;
            {
                if (id != null || id != 0)
                {
                    ProductSold productSold = db.ProductSolds.Find(id);
                    db.ProductSolds.Remove(productSold);
                    db.SaveChanges();
                    result = true; ;
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
    }
}