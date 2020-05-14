using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Lab5.Service
{
    public class CustomerService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(Customer model)
        {
            if (model.FullNameCustomers == null && model.NumberOfMan == null)
            {
                throw new Exception("Column could not be null");
            }

            db.Customers.Add(
                new Customer()
                {
                    FullNameCustomers = model.FullNameCustomers,
                    NumberOfMan = model.NumberOfMan,
                });
            db.SaveChanges();
        }

        public static List<Customer> Read(Customer model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Customer> answer = null;

            if (model.FullNameCustomers != null && model.NumberOfMan != null)
            {
                answer =
                db.Customers
                .Where(rec => rec.NumberOfMan == model.NumberOfMan 
                && rec.FullNameCustomers == model.FullNameCustomers)
                .Include(rec => rec.Orders)
                .Skip(pageSize * currentPage)
                .Take(pageSize)
                .ToList();
            }
            else
            {
                answer =
                    db.Customers
                    .Include(rec => rec.Orders)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Update(Customer model)
        {
            Customer customer = db.Customers.FirstOrDefault(rec => rec.NumberOfMan == model.NumberOfMan
                && rec.FullNameCustomers == model.FullNameCustomers);

            db.SaveChanges();
        }


        public static void Delete(Customer model)
        {
            Customer customer = db.Customers.FirstOrDefault(rec => rec.NumberOfMan == model.NumberOfMan
                && rec.FullNameCustomers == model.FullNameCustomers);

            db.Customers.Remove(customer);
            db.SaveChanges();
        }
    }
}
