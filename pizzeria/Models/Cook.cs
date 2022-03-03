using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.Models
{
    internal class Cook
    {
        internal int Id { get; private set; }
        internal Cook(int id)
        {
            Id = id;
        }
        internal Order OrderAtWork { get; private set; }
        internal async void MakeOrder(Order order, Action<Cook> onMaked)
        {
            Random random = new Random();
            OrderAtWork = order;
            Console.WriteLine($"Повар с номером {Id} взял заказ {order.Number}");
            await Task.Delay(random.Next(4000));
            onMaked(this);

        }
    }
}
