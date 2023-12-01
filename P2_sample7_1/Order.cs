using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_sample7_1
{

    public interface Order
    {

        Order addItem(OrderItem item);

        int amount();

    }

    public interface OrderItem
    {

        int getId();

        int getProductId();

        string getProductName();

        int getProductPrice();

        int getQuantity();

        int amount();

    }

    public class OrderObject : Order
    {

        private int id = 0;
        private List<OrderItem> items = new List<OrderItem>();

        public OrderObject(int id)
        {

            this.id = id;

        }

        public int getId()
        {

            return id;

        }

        public Order addItem(OrderItem item)
        {

            items.Add(item);

            return this;

        }

        public int amount()
        {

            int amount = 0;

            items.ForEach(item =>
            {

                amount += item.amount();

            });

            return amount;

        }

    }

    public class OrderItemObject : OrderItem
    {

        private int id = 0;

        private Product product = NullProduct.get();

        private int quantity = 0;

        public OrderItemObject(int id, Product product, int quantity)
        {
            
            this.id = id;
            this.product = product;
            this.quantity = quantity;

        }

        public int getId()
        {

            return id;

        }

        public int getProductId()
        {

            return product.getId();

        }

        public string getProductName()
        {

            return product.getName();

        }

        public int getProductPrice()
        {

            return product.getPrice();

        }

        public int getQuantity()
        {

            return quantity;

        }

        public int amount()
        {

            return getProductPrice() * getQuantity();

        }

    }

}
