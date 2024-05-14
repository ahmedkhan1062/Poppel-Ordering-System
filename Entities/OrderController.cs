using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PoppelOrder.Database;

namespace PoppelOrder.Entities
{
    public class OrderController
    {
        PoppelInfoDB OrderDB;
        private Collection<Order> orders;
        private Collection<Customer> clients;
        private Collection<Product> items;
        #region property methods
        public Collection<Order> AllOrders
        {
            get
            {
                return orders;
            }
        }

        public Collection<Customer> AllClients
        {
            get
            {
                return clients;
            }
        }

        public Collection<Product> AllItems
        {
            get
            {
                return items;
            }
        }
        #endregion

        #region Constructors

        public OrderController()
        {
            OrderDB = new PoppelInfoDB();
            orders = OrderDB.AllOrders;
            items = OrderDB.Items;
            clients = OrderDB.Clients;
        }

        #endregion

        public void Add(Customer client)
        {
            OrderDB.AddDB(client);
            clients.Add(client);
        }

        public void Add(Order order)
        {
            OrderDB.AddDB(order);
            orders.Add(order);
        }

        public void Edit(Customer client)
        {
            OrderDB.EditDB(client);
        }
    }



}
