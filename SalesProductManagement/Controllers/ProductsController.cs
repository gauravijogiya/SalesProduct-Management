using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesProductManagement.Models;

namespace SalesProductManagement.Controllers
{
    public class ProductsController : Controller
    {
        //   public InventoryManagement_ord1Entities1 db = new InventoryManagement_ord1Entities1();
        public SalesProductManagement.Models.InventoryManagement_ord1Entities db = new InventoryManagement_ord1Entities();
        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult CreateProducts()
        {
            var productList = db.Products.ToList().Select(x => new {
                Id = x.Id,
                ProductName = x.ProductName,
                Price = x.Price
            });

            return Json(productList, JsonRequestBehavior.AllowGet);
        }


        //Add Customer
        [HttpPost]
        public JsonResult Create()
        {
            Product product = new Product();
            product.ProductName = Request["ProductName"];
            product.Price =Convert.ToDecimal( Request["Price"]);
            db.Products.Add(product);
            db.SaveChanges();
            product = db.Products.ToList().LastOrDefault();
            return Json(product.Id, JsonRequestBehavior.AllowGet);
        }

        
        //Update Customer
        //[HttpPost]
        public JsonResult GetProductByid(int id)
        {
            Product product = db.Products.Where(c => c.Id == id).SingleOrDefault();
            string value = string.Empty;
            return Json(product, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Update(int Id, string ProductName, string Price)
        {

            bool result = false;
            if (Id != 0)
            {
                var product = db.Products.Where(c => c.Id == Id).FirstOrDefault();
                product.ProductName = Request["ProductName"];
                product.Price = Convert.ToDecimal( Request["Price"]);
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
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
                Product product = db.Products.Find(id);
                // ProductSold productsold = db.ProductSolds.Where(p => p.CustomerId == id).FirstOrDefault();
                db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                //context.Entry(customer).State = System.Data.Entity.EntityState.Deleted;

                db.Products.Remove(product);
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