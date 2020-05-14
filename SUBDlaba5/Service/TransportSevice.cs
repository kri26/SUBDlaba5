using Lab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab5.Service
{
    class TransportService
    {
        private static DatabaseContext db = Program.DB;

        public static void Create(Transport model)
        {
            if (model.NumborOfTransport == null && model.TypeOfTransport == null)
            {
                throw new Exception("Column could not be null");
            }

            db.Transports.Add(
                new Transport()
                {
                    NumborOfTransport = model.NumborOfTransport,
                    TypeOfTransport = model.TypeOfTransport
                });
            db.SaveChanges();
        }

        public static List<Transport> Read(Transport model, int pageSize = Int32.MaxValue, int currentPage = 0)
        {
            List<Transport> answer = null;

            if (model.NumborOfTransport != null && model.TypeOfTransport != null)
            {
                answer =
                db.Transports
                .Where(rec => rec.NumborOfTransport == model.NumborOfTransport && rec.TypeOfTransport == model.TypeOfTransport)
                .Include(rec => rec.Orders)
                .Skip(pageSize * currentPage)
                .Take(pageSize)
                .ToList();
            }
            else
            {
                answer =
                    db.Transports
                    .Include(rec => rec.Orders)
                    .Skip(pageSize * currentPage)
                    .Take(pageSize)
                    .ToList();
            }

            return answer;
        }

        public static void Update(Transport model)
        {
            Transport transport = db.Transports.FirstOrDefault(rec => rec.NumborOfTransport == model.NumborOfTransport && rec.TypeOfTransport == model.TypeOfTransport);
            db.SaveChanges();
        }

        public static void Delete(Transport model)
        {
            Transport Transport = db.Transports.FirstOrDefault(rec => rec.NumborOfTransport == model.NumborOfTransport && rec.TypeOfTransport == model.TypeOfTransport);

            db.Transports.Remove(Transport);
            db.SaveChanges();
        }
    }
}
