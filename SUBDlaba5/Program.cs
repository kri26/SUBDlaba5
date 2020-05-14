using Lab5.Models;
using Lab5.Service;
using System;
using System.Linq;

namespace Lab5
{
    class Program
    {
        public static readonly DatabaseContext DB = new DatabaseContext("localhost", "5432", "krist", "hotel");

        static void Main(string[] args)
        {
            int[,] summaryTable = new int[11, 15];

            for (int i = 0; i < 10; i++)
            {
                int[] times = test();
                for (int j = 0; j < 15; j++)
                {
                    summaryTable[i, j] = times[j];
                }
            }

            for (int i = 0; i < 15; i++)
            {
                int min = Int32.MaxValue;
                for (int j = 0; j < 10; j++)
                {
                    if (summaryTable[j, i] < min)
                    {
                        min = summaryTable[j, i];
                    }
                }

                summaryTable[10, i] = min;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("ScriptInsert0: " + summaryTable[10, 0]);
            Console.WriteLine("ScriptInsert1: " + summaryTable[10, 1]);
            Console.WriteLine("ScriptInsert2: " + summaryTable[10, 2]);
            Console.WriteLine("ScriptRead0: " + summaryTable[10, 3]);
            Console.WriteLine("ScriptRead1: " + summaryTable[10, 4]);
            Console.WriteLine("ScriptRead2: " + summaryTable[10, 5]);
            Console.WriteLine("ScriptUpdate0: " + summaryTable[10, 6]);
            Console.WriteLine("ScriptUpdate1: " + summaryTable[10, 7]);
            Console.WriteLine("ScriptUpdate2: " + summaryTable[10, 8]);
            Console.WriteLine("ScriptCustom0: " + summaryTable[10, 9]);
            Console.WriteLine("ScriptCustom1: " + summaryTable[10, 10]);
            Console.WriteLine("ScriptCustom2: " + summaryTable[10, 11]);
            Console.WriteLine("ScriptDelete0: " + summaryTable[10, 14]);
            Console.WriteLine("ScriptDelete1: " + summaryTable[10, 13]);
            Console.WriteLine("ScriptDelete2: " + summaryTable[10, 12]);
        }

        static int[] test()
        {
            int[] times = new int[17];


            times[0] = ScriptInsert0();
            times[1] = ScriptInsert1();
            times[2] = ScriptInsert2();
            times[3] = ScriptInsert3();
            times[4] = ScriptInsert4();
            times[5] = ScriptInsert5();
            times[6] = ScriptRead0();
            times[7] = ScriptRead1();
            times[8] = ScriptRead2();
            times[9] = ScriptUpdate0();
            times[10] = ScriptUpdate1();
            times[11] = ScriptUpdate2();
            //times[12] = ScriptCustom0();
            times[13] = ScriptCustom1();
            times[14] = ScriptCustom2();
           // times[15] = ScriptDelete2();
            //times[16] = ScriptDelete1();
            //times[17] = ScriptDelete0();

            return times;
        }

        static int ScriptInsert0()
        {
            Number model = new Number() { Size = 2, TypeOfNumber = "Standart" };

            DateTime startTime = DateTime.Now;
            NumberService.Create(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptInsert1()
        {
            Customer model = new Customer() { FullNameCustomers = "Ivanov II", NumberOfMan = 1 };

            DateTime startTime = DateTime.Now;
            CustomerService.Create(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptInsert2()
        {
            Money model = new Money() { Currency = "RUB", Amount = 1000, WorkerMoney = 1};

            DateTime startTime = DateTime.Now;
            MoneyService.Create(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptInsert3()
        {
            Transport model = new Transport() { NumborOfTransport = "A336KK", TypeOfTransport = "standart" };

            DateTime startTime = DateTime.Now;
            TransportService.Create(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptInsert4()
        {
            Worker model = new Worker() { FullNameWorker = "Ivanov II", Position = "manager" };

            DateTime startTime = DateTime.Now;
            WorkerService.Create(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptInsert5()
        {
            var worker = WorkerService.Read(new Worker() { FullNameWorker = "Ivanov II", Position = "manager" }, 1, 0).First();
            var customer = CustomerService.Read(new Customer() { FullNameCustomers = "Ivanov II", NumberOfMan = 1 }, 1, 0).First();
            Orders[] models = new Orders[30];
            for (int i = 0; i < models.Length; i++)
            {
                models[i] = new Orders()
                {
                    DataBegin = DateTime.Now,
                    DataEnd = DateTime.Now.AddDays(i),
                    MoneyId = 1,
                    CustomersId = customer.Id,
                    NumberId = 1,
                    TransportId = 1,
                    WorkerId = worker.Id

                };
            }

            DateTime startTime = DateTime.Now;
            foreach(var model in models)
            {
                OrdersService.Create(model);
            }
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead0()
        {
            Customer model = CustomerService.Read(new Customer() { FullNameCustomers = "Ivanov II", NumberOfMan = 1 }, 1, 0).First();

            DateTime startTime = DateTime.Now;
            Customer customer = CustomerService.Read(model, 1, 0).First();
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("{0}: {1} - {2}", customer.Id, customer.FullNameCustomers, customer.NumberOfMan);

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead1()
        {
            Money model = MoneyService.Read(new Money() { Currency = "RUB", Amount = 1000 }, 1, 0).First();

            DateTime startTime = DateTime.Now;
            Money models = MoneyService.Read(model, 1, 0).First();
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("{0}: {1} - {2}", models.Id, models.Currency, models.Amount);

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead2()
        {
            Number model = NumberService.Read(new Number() { Size = 2, TypeOfNumber = "Standart" }, 1, 0).First();

            DateTime startTime = DateTime.Now;
            Number models = NumberService.Read(model, 1, 0).First();
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("{0}: {1} - {2}", models.Id, models.Size, models.TypeOfNumber);

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead3()
        {
            Transport model = TransportService.Read(new Transport() { NumborOfTransport = "A336KK", TypeOfTransport = "standart" }, 1, 0).First();

            DateTime startTime = DateTime.Now;
            Transport models = TransportService.Read(model, 1, 0).First();
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("{0}: {1} - {2}", models.Id, models.NumborOfTransport, models.TypeOfTransport);

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead4()
        {
            var worker = WorkerService.Read(new Worker() { FullNameWorker = "Ivanov II", Position = "manager" }, 1, 0).First();
            var customer = CustomerService.Read(new Customer() { FullNameCustomers = "Ivanov II", NumberOfMan = 1 }, 1, 0).First();
            Orders model = OrdersService.Read(new Orders() { WorkerId = worker.Id , CustomersId = customer.Id }, 1, 0).First();

            DateTime startTime = DateTime.Now;
            var models = OrdersService.Read(model);
            DateTime finishTime = DateTime.Now;

            foreach(var orders in models)
            {
                Console.WriteLine("{0} {1}", orders.DataBegin, orders.DataEnd);
            }

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptRead5()
        {
            Worker model = WorkerService.Read(new Worker() { FullNameWorker = "Ivanov II", Position = "manager" }, 1, 0).First();

            DateTime startTime = DateTime.Now;
            Worker models = WorkerService.Read(model, 1, 0).First();
            DateTime finishTime = DateTime.Now;

            Console.WriteLine("{0}: {1} - {2}", models.Id, models.FullNameWorker, models.Position);

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptUpdate0()
        {
            Worker model = WorkerService.Read(new Worker() { FullNameWorker = "Ivanov II", Position = "manager" }, 1, 0).First();
            DateTime startTime = DateTime.Now;
            WorkerService.Update(model);
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptUpdate1()
        {
            Customer model = CustomerService.Read(new Customer() { FullNameCustomers = "Ivanov II", NumberOfMan = 1 }, 1, 0).First();
            DateTime startTime = DateTime.Now;
            CustomerService.Update(model);
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptUpdate2()
        {
            Money model = MoneyService.Read(new Money() { Currency = "RUB", Amount = 1000 }, 1, 0).First();
            DateTime startTime = DateTime.Now;
            MoneyService.Update(model);
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptDelete0()
        {
            Transport transport = TransportService.Read(new Transport() { NumborOfTransport = "A336KK", TypeOfTransport = "standart" }, 1, 0).First();
            Transport model = new Transport() { Id = transport.Id };
            DateTime startTime = DateTime.Now;
            TransportService.Delete(model);
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptDelete1()
        {
            Orders order = OrdersService.Read(new Orders() { DataEnd = DateTime.Now.Date }, 1, 0).First();
            Customer customer = CustomerService.Read(new Customer() { Id = order.Id }, 1, 0).First();
            Customer model = new Customer() { Id = customer.Id };
            DateTime startTime = DateTime.Now;
            CustomerService.Delete(model);
            DateTime finishTime = DateTime.Now;

            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptDelete2()
        {
            Worker order = WorkerService.Read(new Worker() { FullNameWorker = "Ivanov II", Position = "manager" }, 1, 0).First();
            Worker model = new Worker() { Id = order.Id };
            DateTime startTime = DateTime.Now;
            WorkerService.Delete(model);
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptCustom0()
        {
            var worker = WorkerService.Read(new Worker() { FullNameWorker = "Ivanov II", Position = "manager" }, 1, 0).First();
            var customer = CustomerService.Read(new Customer() { FullNameCustomers = "Ivanov II", NumberOfMan = 1 }, 1, 0).First();
            DateTime startTime = DateTime.Now;
            Orders model = OrdersService.Read(new Orders() { WorkerId = worker.Id, CustomersId = customer.Id }, 1, 0).First(); 
            var models = OrdersService.Read(model);
            foreach (var orders in models)
            {
                Console.WriteLine("{0} {1}", orders.DataBegin, orders.DataEnd);
            }
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptCustom1()
        {
            DateTime startTime = DateTime.Now;
            Customer model = CustomerService.Read(new Customer() { FullNameCustomers = "Ivanov II", NumberOfMan = 1 }, 1, 0).First();   
            Customer customer = CustomerService.Read(model, 1, 0).First();
            Console.WriteLine("{0}: {1} - {2}", customer.Id, customer.FullNameCustomers, customer.NumberOfMan);
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }

        static int ScriptCustom2()
        {
            DateTime startTime = DateTime.Now;
            var models = WorkerService.ReadUsedWorker();
            Console.WriteLine("{0} {1} ", models.Item1, models.Item2);
            DateTime finishTime = DateTime.Now;
            return (int)(finishTime - startTime).TotalMilliseconds;
        }
    }
}
