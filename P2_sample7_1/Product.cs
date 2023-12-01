using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_sample7_1
{

    public interface Product
    {

        int getId();

        string getName();

        int getPrice();

    }

    public class ProductObject : Product
    {

        private int id = 0;
        private string name = string.Empty;
        private int price = 0;

        public ProductObject(int id, string name, int price)
        {

            this.id = id;
            this.name = name;
            this.price = price;

        }

        protected ProductObject()
        {

        }

        public int getId()
        {

            return id;

        }

        public string getName()
        {

            return name;

        }

        public int getPrice()
        {

            return price;

        }

    }

    public class NullProduct : ProductObject, NullObject
    {

        private static NullProduct nullProduct = new NullProduct();

        private NullProduct() : base()
        {

        }

        public static NullProduct get()
        {

            return nullProduct;

        }

    }

}
