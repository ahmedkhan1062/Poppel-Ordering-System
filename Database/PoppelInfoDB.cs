using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoppelOrder.Entities;
using System.Diagnostics;

namespace PoppelOrder.Database
{
    public class PoppelInfoDB : DB
    {
        #region Data Fields
        private Collection<Order> orders;
        private Collection<Customer> clients;
        private Collection<Product> items;
        #endregion

        #region Constructor
        public PoppelInfoDB() : base()
        {
            orders = new Collection<Order>();
            clients = new Collection<Customer>();
            items = new Collection<Product>();
            ReadDataFromTable("SELECT * FROM [Order]", "Order");
            ReadDataFromTable("SELECT * FROM [Customer]", "Customer");
            ReadDataFromTable("SELECT * FROM [Credit_Status]", "Credit_Status");
            ReadDataFromTable("SELECT * FROM [Product]", "Product");
        }
        #endregion

        #region Property Methods
        internal Collection<Order> AllOrders { get => orders; set => orders = value; }
        internal Collection<Customer> Clients { get => clients; set => clients = value; }
        internal Collection<Product> Items { get => items; set => items = value; }
        #endregion

        private void fillUpOrders(SqlDataReader reader, string dataTable)
        {
            if (dataTable.Equals("Order"))
            {
                Order order;
                while (reader.Read())
                {
                    order = new Order();
                    order.Num = reader.GetInt64(0);
                    order.OrderDate = reader.GetDateTime(1);
                    order.ShipDate = reader.GetDateTime(2);
                    order.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), reader.GetString(3).Trim());
                    order.GrandTotal = decimal.ToDouble(reader.GetDecimal(4));
                    order.ShipAddress = reader.GetString(5).Trim();
                    orders.Add(order);
                }
            }

            else if (dataTable.Equals("Customer"))
            {
                Customer client;
                while (reader.Read())
                {
                    client = new Customer();
                    client.ID = reader.GetString(0).Trim();
                    client.Name = reader.GetString(1).Trim();
                    client.Surname = reader.GetString(2).Trim();
                    client.Address = reader.GetString(3).Trim();
                    client.Telephone = reader.GetString(4).Trim();
                    client.Email = reader.GetString(5).Trim();
                    //client.Barred = bool.Parse(reader.GetString(6).Trim());
                    clients.Add(client);
                }
            }
            else if (dataTable.Equals("Credit_Status"))
            {
                Customer client;
                while (reader.Read())
                {
                   // client = new Customer();
                    client = findCustomer(reader.GetString(0).Trim());
                    double netTotal = decimal.ToDouble(reader.GetDecimal(2));
                    double limit = decimal.ToDouble(reader.GetDecimal(3));
                    short daysElapsed = reader.GetInt16(4);
                    client.modCredit(netTotal, limit, daysElapsed);
                }
            }
            else if (dataTable.Equals("Product"))
            {
                Product item;
                while (reader.Read())
                {
                    item = new Product();
                    item.ID = reader.GetString(0).Trim();
                    item.Price = reader.GetDecimal(1);
                    item.ItemCategory = reader.GetString(2).Trim();
                    item.ItemSubCategory = reader.GetString(3).Trim();
                    item.ItemName = reader.GetString(4).Trim();
                    item.Description = reader.GetString(5).Trim();
                    item.StockQty = reader.GetInt32(6);
                    items.Add(item); ;
                }
            }
        }
        private string ReadDataFromTable(string selectString, string table)
        {
            SqlDataReader reader; SqlCommand command;
            try
            {
                command = new SqlCommand(selectString, cnMain);
                cnMain.Open();
                command.CommandType = System.Data.CommandType.Text;
                reader = command.ExecuteReader();
                if (reader.HasRows) { fillUpOrders(reader, table); }
                reader.Close();
                cnMain.Close();
                return "Success!";
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                System.Windows.Forms.MessageBox.Show(sqlException.Message);
                return "Error";
            }
        }

        private Customer findCustomer(string id)
        {
            foreach(Customer c in clients){
                if (c.ID.Equals(id)) return c;
            }
            return null;
        }

        private string getCustValStr(Customer c)
        {
             string aStr;
            aStr = "'" + c.ID + "', ' " + c.Name + " ' ," + " ' " + c.Surname + " ' ," +
             " ' " + c.Address + " ' ," + " ' " + c.Telephone +
             " ' , '" + c.Email + "' , '" +c.accessCredit().ToString().Trim() +"'";
             return aStr;
        }

        private string getOrderValStr(Order o)
        {
            string aStr;
            aStr =  o.Num + ",  '" + o.OrderDate + "' , '"  + o.ShipDate + "' ," +
             " '" + o.Status.ToString().Trim() + "' , "  + o.GrandTotal +
             ", '" + o.ShipAddress +"'";
            return aStr;
        }

        public void AddDB(Order or)
        {
            string strSQL = "";
            strSQL = "INSERT INTO [Order](Number, [Order Date], [Shipping Date], [Order_Status], Total, [Ship Address])" +
                " VALUES(" + getOrderValStr(or) + ")";
         UpdateDataSource(new SqlCommand(strSQL, cnMain));
        }

        public void AddDB(Customer cust)
        {
            string strSQL = "";
            strSQL = "INSERT into [Customer](ID, Name, Surname, Address, Phone, [E-Mail], Barred)" +
                "VALUES(" + getCustValStr(cust) + ")" ;
            UpdateDataSource(new SqlCommand(strSQL, cnMain));
        }

        public void EditDB(Customer cl)
        {
            string sqlComm = "Update [Credit_Status] Set Balance = (" + (decimal)cl.getBalance() + ") WHERE (ID = '" + cl.ID + "')";
            UpdateDataSource(new SqlCommand(sqlComm, cnMain));
            sqlComm = "Update [Customer] Set Barred = '" + cl.accessCredit().ToString().Trim() + "' WHERE (ID = '" + cl.ID + "')";
            UpdateDataSource(new SqlCommand(sqlComm, cnMain));
        }
    }
}
