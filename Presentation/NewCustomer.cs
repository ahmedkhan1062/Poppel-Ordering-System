using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoppelOrder.Entities;
using System.Collections.ObjectModel;

namespace PoppelOrder.Presentation
{
    public partial class NewCustomer : Form
    {
        OrderController DBcontrol;
        public bool regCustClosed = false;
        HomeForm homepage;
        public NewCustomer(OrderController controllerDB)
        {
            InitializeComponent();
            DBcontrol = controllerDB;
            this.Load += NewCustomer_Load;
            this.FormClosed += NewCustomer_FormClosed;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void NewCustomer_Load(object sender, EventArgs e)
        {
            homepage = (HomeForm)this.MdiParent;
        }

        private void checkCustomer()
        {

            if (CustNum.Text == "")
            {
                MessageBox.Show("Please use enter an ID", "Invalid ID");
            }


            else if ((naam.Text.Contains("0")) || (naam.Text.Contains("1")) || (naam.Text.Contains("2")) || (naam.Text.Contains("3")) || (naam.Text.Contains("4")) || (naam.Text.Contains("5")) || (naam.Text.Contains("6")) || (naam.Text.Contains("7")) || (naam.Text.Contains("8")) || (naam.Text.Contains("9")) || naam.Text == "")
            {
                MessageBox.Show("Please use only aplhabetic characters in this field", "Invalid Name");
            }


            else if (van.Text.Contains("0") || van.Text.Contains("1") || van.Text.Contains("2") || van.Text.Contains("3") || van.Text.Contains("4") || van.Text.Contains("5") || van.Text.Contains("6") || van.Text.Contains("7") || van.Text.Contains("8") || van.Text.Contains("9") || van.Text == "")
            {
                MessageBox.Show("Please use only aplhabetic characters in this field", "Invalid Surname");
            }

            else if (street.Text == "")
            {
                MessageBox.Show("Please enter an Address", "Invalid Address");
            }

            else if (TeleNum.Text == "")
            {
                MessageBox.Show("Please enter a telephone number", "Invalid telephone number");
            }

            else if (!mail.Text.Contains("@") || mail.Text == "")
            {
                MessageBox.Show("Please enter a valid email", "Invalid Email");
            }
            else if (limit.Text == "")
            {
                MessageBox.Show("Please enter a valid credit limit", "Invalid Credit Limit");
            }
            else
            {
                Customer client = createCustomer();
                DBcontrol.Add(client);
                MessageBox.Show("A customer has been successfully created", "Success!");
                this.Close();
            }  
        }
        private Customer createCustomer()
        {
            Customer client = new Customer();
            client.ID = CustNum.Text;
            client.Email = mail.Text;
            client.Name = naam.Text;
            client.Surname = van.Text;
            client.Address = street.Text;
            client.Telephone = TeleNum.Text;
            double credit = Convert.ToDouble(limit.Text);
            client.modCredit(0, credit, 0);
            return client;
        }

        private void register_Click(object sender, EventArgs e)
        {

            checkCustomer();
        }

        private void NewCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            regCustClosed = true;
        }

        private void NewCustomer_Activated(object sender, EventArgs e)
        {
            homepage = (HomeForm)this.MdiParent;
        }
    }
}
