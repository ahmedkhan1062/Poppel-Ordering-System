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
namespace PoppelOrder.Presentation
{
    public partial class HomeForm : Form
    {
        #region Data fields
        RegistrationForm regForm;
        OrderController DBController;
        #endregion

        #region Constructor
        public HomeForm()
        {
            InitializeComponent();
            DBController = new OrderController();

        }
        #endregion

        #region Events and procedures
        private void HomeForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //This formats the forms so that the child forms are displayed inside the main form
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Show();
        }

        //This creates the customer registration form
        public void CreateNewRegForm()
        {
            regForm = new RegistrationForm(DBController);
            regForm.MdiParent = this;
            regForm.StartPosition = FormStartPosition.CenterParent;
            

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //This opens up the customer registration when the "Create customer order" button is clicked
        
        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void openReg()
        {
            regForm.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (regForm == null)
            {
                CreateNewRegForm();
            }

            if (regForm.regFormClosed)
            {
                CreateNewRegForm();
            }
            regForm.Show();
        }
        #endregion

        public void closeRegForm()
        {
            regForm.Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
