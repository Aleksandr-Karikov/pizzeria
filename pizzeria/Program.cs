using pizzeria.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace pizzeria
{
    internal class Program
    {
        private static Queue<Cook> cooksOrder = new Queue<Cook>();
        //private static Queue<Courier> couriersOrder = new Queue<Courier>();
        private static Queue<Order> ordersOrder = new Queue<Order>();

        private static Queue<Cook> cooksOrderWarehouse = new Queue<Cook>();
        private static Queue<Courier> couriersOrderWarehouse = new Queue<Courier>();

        private static int Count { get; set; }

        private static Warehouse warehouse;

        static void Main(string[] args)
        {
            PizzeriaInit(20, 9, 9);
            Random random = new Random();

            while (true)
            {
                Thread.Sleep(random.Next(3000));
                GenerateOrder();
            }

        }

        private static void PizzeriaInit(int sizeWarehouse, int countCourier, int countCook)
        {
            warehouse = new Warehouse(sizeWarehouse);

            for (int i = 0; i < countCook; i++)
            {
                cooksOrder.Enqueue(new Cook(i+1));
            }
            for (int i = 0; i < countCourier; i++)
            {
                couriersOrderWarehouse.Enqueue(new Courier(i+1));
            }

        }

        private static void GenerateOrder()
        {
            Count++;
            Random random = new Random();
            var content = new Dictionary<Food, int>() { { Food.Pizza, random.Next(1, 5) } };
            var order = new Order(Count, content);
            Console.WriteLine($"пришел новый заказ с номером {Count}");
            if (cooksOrder.Count > 0)
            {
                var cook = cooksOrder.Dequeue();
                cook.MakeOrder(order, OrderCookDone);
            }
            else
            {
                ordersOrder.Enqueue(order);
            }

        }

        private static void OrderCookDone(Cook cook)
        {
            if (warehouse.HasSpace(cook.OrderAtWork))
            {
                if (cooksOrderWarehouse.Contains(cook))
                {
                    cooksOrderWarehouse.Dequeue();
                }
                warehouse.AddOrder(cook.OrderAtWork);
                Console.WriteLine($"Повар {cook.Id} положил заказ {cook.OrderAtWork.Number} на склад");
                if (couriersOrderWarehouse.Count > 0)
                {
                    var courier = couriersOrderWarehouse.Dequeue();
                    courier.TakeOrder(warehouse.TakeOrder(), OrderCourierDone);
                }
                if (ordersOrder.Count > 0 && cooksOrder.Count==0)
                {
                    var order = ordersOrder.Dequeue();
                    cook.MakeOrder(order, OrderCookDone);
                }
                else
                {
                    cooksOrder.Enqueue(cook);
                }
            }
            else
            {
                Console.WriteLine("Место на складе закончилось");
                Console.WriteLine($"Повар {cook.Id} встал в очередь на склад");
                if (!cooksOrderWarehouse.Contains(cook))
                    cooksOrderWarehouse.Enqueue(cook);
            }
        }

        private static void OrderCourierDone(Courier courier)
        {
            Console.WriteLine($"Курьер {courier.Id} завершил заказ {courier.OrderAtWork.Number}");
            if (warehouse.Orders.Count > 0 && couriersOrderWarehouse.Count==0)
            {
                if (cooksOrderWarehouse.Count > 0)
                {
                    OrderCookDone(cooksOrderWarehouse.Peek());
                }
                courier.TakeOrder(warehouse.TakeOrder(), OrderCourierDone);
            }
            else
            {
                couriersOrderWarehouse.Enqueue(courier);
            }
        }
    }
}
