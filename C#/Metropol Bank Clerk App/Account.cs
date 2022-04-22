using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metropol_Bank_Clerk_App
{
    class Account
    {
        private int accountID { get; set; }
        private int ID { get; set; }
        private string accNumber { get; set; }
        private string currency { get; set; }
        private int typeOfAccount { get; set; }
        private double depositedAmount { get; set; }
        private double cardLimit { get; set; }
        private int typeOfPayment { get; set; }

        public int getID()
        {
            return ID;
        }

        public string getNumber()
        {
            return accNumber;
        }
        public int returnType()
        {
            return typeOfAccount;
        }
        public double getAmount()
        {
            return depositedAmount;
        }

        public string getCurrency()
        {
            return currency;
        }
        public int getAccountID()
        {
            return accountID;
        }
        public void setAmount(double newAmount)
        {
            this.depositedAmount = newAmount;
        }
        public int getType()
        {
            return typeOfAccount;
        }
        public int getTypeofPayment() 
        {
            return typeOfPayment;
        }
    }
}
