using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;

namespace API.Controllers
{
    public class CustController : ApiController
    {      
        CustomerEntities customerEntities = new CustomerEntities();
      
        // GET: api/Cust/5
        /// <summary>
        /// Gets the customer details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return JSON</returns>
        public string Get(string id)
        {
            try
            {                
                var cust = (from c in customerEntities.Customers where c.ID_Number == id select c).ToList().FirstOrDefault();
                return new JavaScriptSerializer().Serialize(new { IdNumber = cust.ID_Number, Firstname = cust.First_Name, Lastname = cust.Last_Name,Mobile = cust.Mobile_Number, Email=cust.Email_Address });
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        // POST: api/Cust
        /// <summary>
        /// Posts the data that it receives from the Web App
        /// </summary>
        /// <param name="inputIDnumber"></param>
        /// <param name="inputFirstname"></param>
        /// <param name="inputlastname"></param>
        /// <param name="inputCell"></param>
        /// <param name="inputEmail"></param>
        /// <returns>Returns message</returns>
        public string Post(string inputIDnumber, string inputFirstname, string inputlastname, string inputCell, string inputEmail)
        {
            var cust = "";
            CustomerEntities customerEntities = new CustomerEntities();
            try
            {
               cust = (from c in customerEntities.Customers where c.ID_Number == inputIDnumber select c.ID_Number).FirstOrDefault();
            }
            catch(Exception e)
            {
                return e.Message;
            }
            if (String.IsNullOrEmpty(cust))
            {
                Customer customer = new Customer();
                customer.ID_Number = inputIDnumber;
                customer.First_Name = inputFirstname;
                customer.Last_Name = inputlastname;
                customer.Mobile_Number = inputCell;
                customer.Email_Address = inputEmail;
                customerEntities.Customers.Add(customer);
                customerEntities.SaveChanges();
                return new JavaScriptSerializer().Serialize(new
                {
                    Message = "Customer Added Successfully"
                });
            }
            else
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    Message = "Customer Already Exists"
                });
            }
        }

        // PUT: api/Cust/5
        /// <summary>
        /// Updates the existing cutomer
        /// </summary>
        /// <param name="inputIDnumber"></param>
        /// <param name="inputFirstname"></param>
        /// <param name="inputlastname"></param>
        /// <param name="inputCell"></param>
        /// <param name="inputEmail"></param>
        /// <returns>Returns message</returns>
        public string Put(string inputIDnumber, string inputFirstname, string inputlastname, string inputCell, string inputEmail)
        {          
            var cust = (from c in customerEntities.Customers where c.ID_Number == inputIDnumber select c).ToList().FirstOrDefault();
            cust.Email_Address = inputEmail;
            cust.First_Name = inputFirstname;
            cust.Last_Name = inputlastname;
            cust.Mobile_Number = inputCell;
            customerEntities.Customers.AddOrUpdate(cust);
            customerEntities.SaveChanges();
            return new JavaScriptSerializer().Serialize(new
            {
                Message = "Customer Updated Successfully"
            });
        }

        // DELETE: api/Cust/5
        public void Delete(int id)
        {
        }
    }
}
