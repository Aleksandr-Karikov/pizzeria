using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.Models
{
    internal class Warehouse
    {
        internal int Size { get; private set; }
        internal Queue<Order> Orders { get; private set; } = new Queue<Order>();

        internal Warehouse(int size)
        {
            Size = size;
        }

        internal int Occupancy { get; set; }
        internal bool HasSpace(Order order)
        {
            int orderSize = order.GetOrderSize();
            if (orderSize > Size - Occupancy)
            {

                return false;
                
            }
            return true;
        }
        internal void AddOrder(Order order)
        {
            int orderSize = order.GetOrderSize();
            if (orderSize > Size - Occupancy) throw new Exception("not enough storage space");
            Orders.Enqueue(order);
            Occupancy += orderSize;

        }

        internal Order TakeOrder()
        {
            if (Orders.Count == 0) throw new Exception("not enough order");

            Order order = Orders.Dequeue();
            Occupancy -= order.GetOrderSize();
            return order;
        }
    }
}
