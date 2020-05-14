using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.Service
{
    class WorkerService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(Worker model)
        {
            if (model.FullNameWorker == null && model.Position == null)
            {
                throw new Exception("Column could not be null");
            }

            db.Workers.Add(
                new Worker()
                {
                    FullNameWorker = model.FullNameWorker,
                    Position = model.Position
                });
            db.SaveChanges();
        }

        public static List<Worker> Read(Worker model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Worker> answer = null;

            if (model.FullNameWorker != null && model.Position != null)
            {
                    answer =
                    db.Workers
                    .Where(rec => rec.FullNameWorker == model.FullNameWorker && rec.Position == model.Position)
                    .Include(rec => rec.Orders) 
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                answer =
                    db.Workers
                    .Include(rec => rec.Orders)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static (string, int) ReadUsedWorker()
        {
            var answer =
                db.Orders
                .Include(rec => rec.Worker)
                .GroupBy(rec => rec.Worker.FullNameWorker)
                .Select(m => new
                {
                    name = m.Key,
                    Count = m.Count()
                })
                .First();

            return (answer.name, answer.Count);
        }

        public static void Update(Worker model)
        {
            Worker worker = db.Workers.FirstOrDefault(rec => rec.FullNameWorker == model.FullNameWorker && rec.Position == model.Position);
            db.SaveChanges();
        }

        public static void Delete(Worker model)
        {
            Worker worker = db.Workers.FirstOrDefault(rec => rec.FullNameWorker == model.FullNameWorker && rec.Position == model.Position);

            db.Workers.Remove(worker);
            db.SaveChanges();
        }
    }
}
