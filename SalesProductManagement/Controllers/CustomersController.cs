using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesProductManagement.Models;

namespace SalesProductManagement.Controllers
{
    public class CustomersController : Controller
    {

        //   public InventoryManagement_ord1Entities1 db = new InventoryManagement_ord1Entities1();
        public SalesProductManagement.Models.InventoryManagement_ord1Entities db = new InventoryManagement_ord1Entities();
        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult CreateCustomer()
        {
            var CustomerList = db.Customers.ToList().Select(x => new {
                Id = x.Id,
                Name=x.Name,
                Address=x.Address
            });
           
            return Json(CustomerList, JsonRequestBehavior.AllowGet);
        }


        //Add Customer
       [HttpPost]
        public JsonResult Create()
        {
            Customer customer = new Customer();
            customer.Name = Request["Name"];
           customer.Address = Request["Address"];
            db.Customers.Add(customer);
            db.SaveChanges();
            customer = db.Customers.ToList().LastOrDefault();
            return Json(customer.Id,JsonRequestBehavior.AllowGet);
        }


        //Update Customer
        //[HttpPost]
        public JsonResult GetCustomerByid(int id)
        {
            Customer customer = db.Customers.Where(c => c.Id == id).SingleOrDefault();
            string value = string.Empty;
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
       

        [HttpPost]
        public JsonResult Update(int Id,string Name,string Address)
        {
           
            bool result = false;
            if (Id != 0)
            {
                var customer = db.Customers.Where(c => c.Id == Id).FirstOrDefault();
                customer.Name = Request["Name"];
                customer.Address = Request["Address"];
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                result = true;
            }
            //else
            //{
            //    return HttpNotFound;
            //}
            return Json(result,JsonRequestBehavior.AllowGet) ;
        }

        [HttpGet]

        public ActionResult Delete(int id)
        { bool result = false;
            try {
                Customer customer = db.Customers.Find(id);
                // ProductSold productsold = db.ProductSolds.Where(p => p.CustomerId == id).FirstOrDefault();
                db.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                //context.Entry(customer).State = System.Data.Entity.EntityState.Deleted;

                db.Customers.Remove(customer);
                // db.ProductSolds.Remove(productsold);
                db.SaveChanges();
                result = true;
            }
            catch(Exception ex)
            {  }

            return Json(result,JsonRequestBehavior.AllowGet);
        }


    }
}
