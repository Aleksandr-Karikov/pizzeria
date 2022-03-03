using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.Models
{
    internal class Courier
    {
        internal int Id { get; private set; }
        internal Courier(int id)
        {
            Id = id;
        }
        internal Order OrderAtWork { get; private set; }
        internal async void TakeOrder(Order order, Action<Courier> onMaked)
        {
            Console.WriteLine($"Курьер {Id} взял заказ {order.Number}");
            Random random = new Random();
            OrderAtWork = order;
            await Task.Delay(random.Next(20000, 30000));
            onMaked(this);
        }
    }
}
