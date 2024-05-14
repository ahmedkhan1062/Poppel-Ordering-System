using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoppelOrder.Entities
{
    public enum OrderStatus
    {
        NewOrder,
        OnHold,
        Shipped,
        Delivered,
        Closed
    }
    public class Order
    {
        #region Fields
        private long num;   //order code
        private DateTime orderDate;
        private DateTime shipDate;
        private OrderStatus status;
        private double grandTotal;
        private string shipAddress;
        private Collection<Product> basket;
        #endregion

        #region Constructor
        public Order(long num, DateTime orderDate, DateTime shipDate, OrderStatus status, double grandTotal, string shipAddress)
        {
            this.num = num;
            this.orderDate = orderDate;
            this.shipDate = shipDate;
            this.status = status;
            this.grandTotal = grandTotal;
            this.shipAddress = shipAddress;
            basket = new Collection<Product>();

        }
        public Order()
        {
            num = 0;
            orderDate = new DateTime();
            shipDate = new DateTime();
            status = OrderStatus.Closed;
            grandTotal = 0;
            shipAddress = "";
            basket = new Collection<Product>();
        }
        #endregion

        #region Property methods
        public long Num { get => num; set => num = value; }
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public DateTime ShipDate { get => shipDate; set => shipDate = value; }
        public OrderStatus Status { get => status; set => status = value; }
        public double GrandTotal { get => grandTotal; set => grandTotal = value; }
        public string ShipAddress { get => shipAddress; set => shipAddress = value; }

        public Collection<Product> Basket
        {
            get { return basket; }
        }
        #endregion

        #region mutator Methods
        public void addItem(Product item)
        {
            basket.Add(item);
        }

        public void removeItem(Product item)
        {
            basket.Remove(item);
        }
        #endregion

        public String toString()
        {
            return "\nOrder No: " + num + "\nTotal Cost: "+grandTotal+"\nTo be shipped to: " + shipAddress+" by "+shipDate;
        }


    }
}
