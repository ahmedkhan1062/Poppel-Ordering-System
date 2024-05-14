using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoppelOrder.Entities
{
    class Clerk
    {
        #region Field Members
        private long emplID;
        private string name;
        private double salary;
        #endregion

        #region Constructor
        public Clerk(long emplID, string name, double salary)
        {
            this.EmplID = emplID;
            this.Name = name;
            this.Salary = salary;
        }
        public Clerk()
        {
            emplID = 0;
            name = "";
            salary = 0;
        }
        #endregion

        #region Property Methods
        public long EmplID { get => emplID; set => emplID = value; }
        public string Name { get => name; set => name = value; }
        public double Salary { get => salary; set => salary = value; }
        #endregion

       bool getCreditStatus(Customer client) { return client.Barred; }
        //void capture() { }
    }
}
