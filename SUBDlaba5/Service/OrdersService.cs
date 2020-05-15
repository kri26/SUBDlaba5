using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Lab5.Service
{
    public class OrdersService
    {

        private static DatabaseContext db = Program.DB;

        public static void Create(Orders model)
        {
            if (model.DataBegin == null)
            {
                throw new Exception("Column DataBegin could not be null");
            }
            if (model.DataEnd == null)
            {
                throw new Exception("Column DataEnd could not be null");
            }           
            if (!model.CustomersId.HasValue)
            {
                throw new Exception("Column CustomersId could not be null");
            }
            if (!model.MoneyId.HasValue)
            {
                throw new Exception("Column MoneyId could not be null");
            }
            if (!model.NumberId.HasValue)
            {
                throw new Exception("Column NumberId could not be null");
            }
            if (!model.TransportId.HasValue)
            {
                throw new Exception("Column TransportId could not be null");
            }
            if (!model.WorkerId.HasValue)
            {
                throw new Exception("Column WorkerId could not be null");
            }
            Orders orders =
                new Orders()
                {
                    DataBegin = model.DataBegin,
                    DataEnd = model.DataEnd,
                    CustomersId = model.CustomersId,
                    MoneyId = model.MoneyId,
                    NumberId = model.NumberId,
                    TransportId = model.TransportId,
                    WorkerId = model.WorkerId
                };

            db.Orders.Add(orders);
            db.SaveChanges();
        }

        public static List<Orders> Read(Orders model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Orders> answer = null;

            if (model.CustomersId.HasValue && model.CustomersId.HasValue && model.MoneyId.HasValue && model.TransportId.HasValue && model.WorkerId.HasValue)
            {
                answer =
                    db.Orders
                    .Where(rec => rec.CustomersId == model.CustomersId && rec.NumberId == model.NumberId
                     && rec.MoneyId == model.MoneyId && rec.TransportId.HasValue == model.TransportId.HasValue && rec.WorkerId.HasValue == model.WorkerId.HasValue)
                    .Include(rec => rec.Customers)
                    .Include(rec => rec.Money)
                    .Include(rec => rec.Transport)
                    .Include(rec => rec.Worker)
                    .Include(rec => rec.Number)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.Orders
                    .Where(rec => rec.Id == model.Id)
                    .Include(rec => rec.Customers)
                    .Include(rec => rec.Money)
                    .Include(rec => rec.Transport)
                    .Include(rec => rec.Worker)
                    .Include(rec => rec.Number)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Update(Orders model)
        {
            Orders Orders = db.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            Orders.WorkerId = model.WorkerId;
            Orders.NumberId = model.NumberId;
            Orders.MoneyId = model.MoneyId;
            Orders.TransportId = model.TransportId;
            Orders.CustomersId = model.CustomersId;
            Orders.DataBegin = model.DataBegin;
            Orders.DataEnd = model.DataEnd;

            db.SaveChanges();
        }

        public static void Delete(Orders model)
        {
            Orders Orders = db.Orders.FirstOrDefault(rec => rec.Id == model.Id);

            db.Orders.Remove(Orders);
            db.SaveChanges();
        }
    }
}
