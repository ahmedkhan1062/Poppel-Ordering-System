using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoppelOrder.Entities
{
    public class Customer
    {
        #region Fields
        private string id;
        private string name;
        private string surname;
        private string address;
        private string telephone;
        private string email;
        private bool blackListed;
        private Collection<Order> history;
        private Payment balance;
        #endregion

        #region Constructor
        public Customer(string id, string name, string surname, string address, string telephone, string email, double net, double limit, short dayPast)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.address = address;
            this.telephone = telephone;
            this.email = email;
            this.balance = new Payment(net, limit, dayPast);
            this.History = new Collection<Order>();
            this.blackListed = balance.isBarred();
        }
        public Customer()
        {
            id = "";
            name = "";
            surname = "";
            address = "";
            telephone = "";
            email = "";
            blackListed = false;
            balance = new Payment();
            History = new Collection<Order>();
        }
        #endregion

        #region Property methods
        public string ID { get => id; set => id = value; }
        public string Address { get => address; set => address = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
        public bool Barred { get => blackListed; set => blackListed = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public Collection<Order> History { get => history; set => history = value; }
        #endregion

        #region InnerClass Payment
        class Payment
        {
            #region Attributes
            private double balance, creditLimit;
            private short daysPast;
            #endregion
            #region Constructor
            public Payment()
            {
                balance = 0;
                creditLimit = 0;
                daysPast = 0;
            }
            public Payment(double balance, double creditLimit, short daysPast)
            {
                this.balance = balance;
                this.creditLimit = creditLimit;
                this.daysPast = daysPast;
                isBarred();
            }
            #endregion
            #region Property Methods
            public double Balance { get => balance; set => balance = value; }
            public double CreditLimit { get => creditLimit; set => creditLimit = value; }
            public short DaysPast { get => daysPast; set => daysPast = value; }
            #endregion
            public bool isBarred()
            {
                if (Balance < 0)
                {
                    double temp = Math.Abs(Balance);
                    if (temp > CreditLimit && DaysPast > 60)
                        return true;
                    else return false;
                }
                else return false;
            }
        }
        #endregion

        public string toString()
        {
            return "Customer ID: " + id + "\nSurname: " + Surname + "\nName: " + Name + "\nAddress: " + address + "\nBarred: " + blackListed + ".\n";
        }
        public void modCredit(double net, double creditLimit, short daysPast)
        {
            balance.Balance = net;
            balance.CreditLimit = creditLimit;
            balance.DaysPast = daysPast;
            blackListed = balance.isBarred();
        }
        public bool accessCredit() { return blackListed; }

        public bool purchaseItem(double price)
        {
            if (balance.Balance - price > balance.CreditLimit*(-1))
            {
                balance.Balance -= price;
                return true;
            }
            else return false;
        }
        public void returnItem(double price)
        {
            balance.Balance += price;
        }

        public double getBalance() { return balance.Balance; }
    }
}
