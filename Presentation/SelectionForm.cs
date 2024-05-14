using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoppelOrder.Entities;
using System.IO;
namespace PoppelOrder.Presentation
{
    public partial class SelectionForm : Form
    {
        #region Fields
        HomeForm Homepage;
        public bool selFormClosed = false;
        OrderController controller;
        Order customerOrder;
        Product currentItem;
        Customer Client;
        bool confirmed = false;
        #endregion

        #region Constructor
        public SelectionForm(OrderController aController, Customer client)
        {
            InitializeComponent();
            this.Load += SelectionForm_Load;
            this.FormClosed += SelectionForm_FormClosed;
            controller = aController;
            Client = client;
            setUpOrder();
            label3.Text = "Place an Order for Mr/Mrs " + Client.Surname;
        }
        #endregion

        #region methods and procedures

        private void setUpOrder()
        {
            var rand = new Random();
            long orderNum = rand.Next(1000, 9999);
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;

            DateTime shipDate = new DateTime();
            shipDate = currentDate.AddDays(7);
            customerOrder = new Order(orderNum, currentDate, shipDate, OrderStatus.NewOrder, 0, Client.Address);
        }

        public void setupCartView()
        {
            decimal total = 0;
            richTextBox1.Clear();

            foreach (Product item in customerOrder.Basket)
            {
                richTextBox1.AppendText(item.cartView());
                richTextBox1.AppendText("\n\n");
                total += item.Price;
            }
            label5.Text = "R"+total.ToString();
            customerOrder.GrandTotal = (double)total;
            if (customerOrder.Basket.Count > 0)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

       /* public void ShowCart()
        {
            
            
            foreach (Product item in customerOrder.Basket)
            {
                cartDetails = new ListViewItem();
            }
        }*/

        private void addToCart(int qty)
        {
            for (int i = 0; i < qty; i++)
            {
                if (!Client.purchaseItem((double)currentItem.Price))
                {
                    var message = MessageBox.Show("Customer has outstanding credit of R" + Client.getBalance() + ".\nCould not process request for " + currentItem.ItemName, "Error");
                    // Client.Credit += currentItem.Price;
                    break;
                }

                if (!currentItem.reserveInv())
                {
                    MessageBox.Show("There is not enough stock available to meet your requested number of " + currentItem.ItemName + "(s)", "Error");
                    break;
                }

                else

                {
                    customerOrder.addItem(currentItem);

                    setupCartView();
                    label7.Text = Client.getBalance().ToString();

                }
            }
        }

        private void removeItem()
        {
            bool check = false;
            foreach (Product item in customerOrder.Basket)
            {
                if (textBox3.Text == item.ID)
                {
                    check = true;
                    customerOrder.removeItem(item);
                    Client.returnItem((double)item.Price);
                    item.incInv();
                    break;
                }
            }
            if (check == false)
            {
                MessageBox.Show("Please enter a valid product ID", "Invalid Product Code");
            }
            setupCartView();
            label7.Text = Client.getBalance().ToString();
        }

        private void SearchProductID()
        {
            Collection<Product> Items;
            Items = controller.AllItems;
            bool check = false;
           
                foreach (Product item in Items)
                {
                    if (textBox4.Text == item.ID)
                    {
                        check = true;
                        richTextBox2.Text = item.toString();
                        currentItem = item;
                        if (item.StockQty > 0)
                        {
                            textBox1.Text="In Stock";
                            button1.Enabled = true;
                        }
                        else
                        {
                            textBox1.Text="Out of Stock";
                            button1.Enabled = false;
                        }

                    }


                }
            if (check == false)
            {
                MessageBox.Show("Please enter a valid product code", "Product ID Invalid");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void confirmOrder()
        {            
            DialogResult message = MessageBox.Show("Would you like to confirm the following order?" + customerOrder.toString(), "Confirm Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (message == DialogResult.Yes)
            {
                //controller.Add(customerOrder);
                confirmed = true;
                Client.History.Add(customerOrder);
                controller.Add(customerOrder);
                controller.Edit(Client);
                generatePickList(customerOrder.Num);
                Homepage.closeRegForm();
                Homepage.CreateNewRegForm();
                Homepage.openReg();
                this.Close();
            }
        }

        //Loads the form or something like that
        private void SelectionForm_Load(object sender, EventArgs e)
        {
            Homepage = (HomeForm)this.MdiParent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            confirmOrder();
            

        }


        private void generatePickList(long orderNum)
        {
            string path = System.IO.Path.GetDirectoryName(Application.StartupPath)+@"\PickLists\Order" + orderNum + ".txt";
            if (!File.Exists(path))
            { 
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Order number " + orderNum + " picking list \n\n");
                    foreach (Product item in customerOrder.Basket)
                    {
                        sw.WriteLine(item.pickString());
                        //sw.WriteLine(System.IO.Path.GetDirectoryName(Application.CommonAppDataPath));
                    }
                    MessageBox.Show("Thank you for your order, a picking list has been generated", "Picklist created");
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            SearchProductID();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            addToCart(int.Parse(textBox2.Text));
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult message = MessageBox.Show("Are you sure you would like to remove this item?" , "Remove Item", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (message == DialogResult.Yes)
            {
                removeItem();
            }
            
        }
        #endregion

        private void label6_Click(object sender, EventArgs e)
        {

        }

        //This helps make sure only one can be open at a time
        private void SelectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!confirmed)
            {
                foreach (Product item in customerOrder.Basket)
                {
                    Client.returnItem((double)item.Price);
                }
            }
            selFormClosed = true;
        }
    }
}
