using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.Service
{
    class MoneyService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(Money model)
        {
            if (model.Currency == null && model.Amount == null && model.WorkerMoney == null)
            {
                throw new Exception("Column could not be null");
            }

            db.Moneys.Add(
                new Money()
                {
                    Amount = model.Amount,
                    Currency = model.Currency,
                    WorkerMoney = model.WorkerMoney,
                });
            db.SaveChanges();
        }

        public static List<Money> Read(Money model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Money> answer = null;

            if (model.Currency != null && model.Amount != null && model.WorkerMoney != null)
            {
                answer =
                    answer =
                    db.Moneys
                    .Where(rec => rec.Currency == model.Currency 
                        && rec.Amount == model.Amount && rec.WorkerMoney == model.WorkerMoney)
                    .Include(rec => rec.Orders)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.Moneys
                    .Include(rec => rec.Orders)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Update(Money model)
        {
            Money materialsType = db.Moneys.FirstOrDefault(rec => rec.Currency == model.Currency
                        && rec.Amount == model.Amount && rec.WorkerMoney == model.WorkerMoney);

            db.SaveChanges();
        }

        public static void Delete(Money model)
        {
            Money money = db.Moneys.FirstOrDefault(rec => rec.Currency == model.Currency
                        && rec.Amount == model.Amount && rec.WorkerMoney == model.WorkerMoney);
            db.Moneys.Remove(money);
            db.SaveChanges();
        }
    }
}
