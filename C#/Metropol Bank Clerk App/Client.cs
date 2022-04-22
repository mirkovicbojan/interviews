using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Metropol_Bank_Clerk_App
{
    
    class Client
    {
        private int ID { get; set; }
        private string name { get; set; }
        private string surname { get; set; }
        private string address { get; set; }
        private int countryNumber { get; set; }
        private int type { get; set; }
        private string dateOfBirth { get; set; }

        public int getID()
        {
            return ID;
        }
        public string getName()
        {
            return name;
        }
        public string getSurname()
        {
            return surname;
        }
        public string getAddress()
        {
            return address;
        }
        public int getCountry()
        {
            return countryNumber;
        }

        public void setAddress(string address)
        {
            this.address = address;
        }
        public int getType()
        {
            return type;
        }

        public void setType(int type) 
        {
            this.type = type;
        }

        public string FullName
        {
            get 
            {
                return $"{name} {surname}";
            }
        }
        public string DOB
        {
            get
            {
                return $"{dateOfBirth}";
            }
        }

        public string getIDstring
        {
            get
            {
                return $"{ID}";
            }
        }
    }
}
