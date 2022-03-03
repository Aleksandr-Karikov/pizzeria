using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.Models
{
    internal class Order
    {
        internal int Number { get; private set; }

        internal Dictionary<Food, int> Content { get;}

        internal Order(int number,Dictionary<Food,int> content)
        {
            Content = content;
            Number = number;
        }

        internal int GetOrderSize()
        {
            int orderSize = 0;
            foreach (var item in Content)
            {
                orderSize += item.Value;
            }
            return orderSize;
        }
    }
}
