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
    public partial class RegistrationForm : Form
    {
        #region Data fields
        HomeForm homepage;
        public bool regFormClosed = false;
        SelectionForm selForm;
        NewCustomer regCust;
        OrderController Controller;
        Customer CurrentClient;
        #endregion

        #region Constructor
        public RegistrationForm(OrderController aController)
        {
            InitializeComponent();
            this.Load += RegForm_Load;
            this.FormClosed += Form_regFormClosed;
            this.Text = "New Order";
            Controller = aController;
            CurrentClient = null;
        }
        #endregion

        #region procedures

        //This is part of the code that ensures that only one window can be open at a time for this form
        private void Form_regFormClosed(object sender, EventArgs e)
        {
            regFormClosed = true;
        }

        //This loads the form or something
        private void RegForm_Load(object sender, EventArgs e)
        {
            homepage = (HomeForm)this.MdiParent;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //This is the code that creates the product selection form
        private void CreateNewSelForm()
        {
            if (selForm == null || selForm.selFormClosed){
                selForm = new SelectionForm(Controller, CurrentClient);
                selForm.MdiParent = homepage;
                selForm.StartPosition = FormStartPosition.CenterParent;
            }
        }

        //This is the register customer button which will move the page to the product selection form
        private void button3_Click(object sender, EventArgs e)
        {
            if (selForm == null)
            {
                CreateNewSelForm();
            }

            if (selForm.selFormClosed)
            {
                CreateNewSelForm();
            }
            selForm.Show();

        }
        private void SearchCustomerID()
        {
            Collection<Customer> Clients;
            Clients = Controller.AllClients;
            //richTextBox2.AppendText("bot");
            /*if (Orders != null)
            {
                richTextBox2.Text = Orders.ToString();
            }*/
            foreach (Customer client in Clients)
            {
                if (textBox1.Text == client.ID)
                {
                    richTextBox1.Text = client.toString();
                    CurrentClient = client;
                    if (!CurrentClient.Barred)
                    {
                        richTextBox1.AppendText("Customer Credit is valid");
                        button3.Enabled = true;
                    }
                    else
                    {
                        richTextBox1.AppendText("Outstanding Credit of R" + client.getBalance());
                        button3.Enabled = false;
                    }
                    return;
                }
            }
            DialogResult message = MessageBox.Show("Would you like to create a new Customer", "Invalid Customer Code!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (message == DialogResult.Yes) {
                if (regCust == null || regCust.regCustClosed){
                    regCust = new NewCustomer(Controller);
                    regCust.MdiParent = homepage;
                    regCust.StartPosition = FormStartPosition.CenterParent;
                    regCust.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchCustomerID();
        }

        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            if (regCust == null || regCust.regCustClosed)
            {
                regCust = new NewCustomer(Controller);
                regCust.MdiParent = homepage;
                regCust.StartPosition = FormStartPosition.CenterParent;
                regCust.Show();
            }
        }
    }
}
