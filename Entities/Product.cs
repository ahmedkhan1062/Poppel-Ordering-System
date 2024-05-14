using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoppelOrder.Entities
{
    public class Product
    {
        #region Fields
        private string code;
        private decimal productPrice;
        private string itemCategory;
        private string itemSubCategory;
        private string itemName;
        private int stockQty;
        private string description;
        #endregion

        #region Property methods
        public string ID
        {
            get { return code; }
            set { code = value; }
        }

        public decimal Price
        {
            get { return productPrice; }
            set { productPrice = value; }
        }
        public string ItemCategory
        {
            get => itemCategory;
            set => itemCategory = value;
        }
        public string ItemSubCategory
        {
            get => itemSubCategory;
            set => itemSubCategory = value;
        }
        public string ItemName
        {
            get => itemName;
            set => itemName = value;
        }


        public int StockQty
        {
            get => stockQty;
            set => stockQty = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }
        #endregion

        #region Constructors
        public Product()
        {
            int code = 0;
            decimal productPrice = 0;
            string itemCategory = "";
            string itemSubCategory = "";
            string itemName = "";
            int stockQty = 0;
            string description = "";
        }

        public Product(int ID, decimal price, string category, string subCategory, string name, int quantity, string des)
        {
            int code = ID;
            decimal productPrice = price;
            string itemCategory = category;
            string itemSubCategory = subCategory;
            string itemName = name;
            int stockQty = quantity;
            string description = des;
        }

        #endregion

        public string toString()
        {
            return "Product ID: " + ID + "\nProduct Name: " + itemName + "\nDescription: " + Description + "\nPrice: " + productPrice;
        }

        public string cartView()
        {
            return  "Item: " + itemName + "\nCode: " + ID + "\nCost: " + productPrice;
        }

        public string pickString()
        {
            return "Product ID: " + ID + "\nProduct Name: " + itemName + "\nCategory: " + ItemCategory + "\nSub-category: " + itemSubCategory+"\n\n";
        }

        public bool reserveInv()
        {
            if (this.stockQty > 0)
            {
                this.stockQty -= 1;
                return true;
            }
            else return false;
        }

        public void incInv()
        {
            this.stockQty += 1;
        }

    }
}
