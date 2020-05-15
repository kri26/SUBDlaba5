using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.Service
{
    public class NumberService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(Number model)
        {
            if (model.Size == null && model.TypeOfNumber == null)
            {
                throw new Exception("Column could not be null");
            }

            db.Numbers.Add(
                new Number()
                {
                    Size = model.Size,
                    TypeOfNumber = model.TypeOfNumber,
                });
            db.SaveChanges();
        }

        public static List<Number> Read(Number model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Number> answer = null;

            if (model.Size != null && model.TypeOfNumber != null)
            {
                answer =
                db.Numbers
                .Where(rec => rec.Size == model.Size && rec.TypeOfNumber == model.TypeOfNumber)
                .Include(rec => rec.Orders)
                .Skip(pageSize * currentPage)
                .Take(pageSize)
                .ToList();
            }
            else
            {
                answer =
                    db.Numbers
                    .Include(rec => rec.Orders)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static (string, int) ReadUsedNumber()
        {
            var answer =
                db.Orders
                .Include(rec => rec.Number)
                .GroupBy(rec => rec.Number.TypeOfNumber)
                .Select(m => new
                {
                    name = m.Key,
                    Count = m.Count()
                })
                .First();

            return (answer.name, answer.Count);
        }

        public static void Update(Number model)
        {
            Number number = db.Numbers.FirstOrDefault(rec => rec.Size == model.Size && rec.TypeOfNumber == model.TypeOfNumber);
            db.SaveChanges();
        }

        public static void Delete(Number model)
        {
            Number number = db.Numbers.FirstOrDefault(rec => rec.Size == model.Size && rec.TypeOfNumber == model.TypeOfNumber);

            db.Numbers.Remove(number);
            db.SaveChanges();
        }
    }
}
