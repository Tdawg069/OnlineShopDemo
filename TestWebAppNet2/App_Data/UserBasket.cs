using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebAppNet2.Data
{
    public class UserBasket
    {
        public UserBasket()
        {

        }

        public int UserBasketId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public Users User { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }

    }
}
