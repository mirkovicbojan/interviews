using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metropol_Bank_Clerk_App
{
    public partial class Form4 : Form
    {
        public string ID_korisnika { get; set; }
        private List<Account> accounts = new List<Account>();
        private List<Account> clientAccounts = new List<Account>();
        private List<Client> clients = new List<Client>();
        

        public Form4(string ID)
        {
            InitializeComponent();
            this.ID_korisnika = ID; //redundant?? probably.
            LoadAccounts();
        }

        /*Transfer button doesn't refresh the form 
         *so you need to exit out to see changes. 
         *Should find a way to fix this. 
         */
        private void button1_Click(object sender, EventArgs e)
        {
         
            double balance = 0;
            double recieverBalance = 0;
            string accountCurrency = null;
            Account senderAccount = null;
            Account recieverAccount = null;

            //Assigns balance, currency and account object from selected item in ListBox
            for (var i = 0; i < accountList.Items.Count; i++) 
            {
                if (accountList.SelectedIndex == i)
                {
                    balance = clientAccounts[i].getAmount();
                    accountCurrency = clientAccounts[i].getCurrency();
                    senderAccount = clientAccounts[i];
                }
                else { continue; }
            }
            /* Ideally I'd write an SQL statement that returns only the account 
             * with the same account number. However, I don't know how to do that in SQLite :(
             * So in this case the program must go through all accounts in the database and find one whose
             * account number matches what the user has entered. That's extremely slow.
             */
            for (var i = 0; i<accounts.Count; i++) 
            {
                if (recieverNumber.Text.Equals(accounts[i].getNumber()) && accountCurrency.Equals(accounts[i].getCurrency())) 
                {
                    recieverAccount = accounts[i];
                    recieverBalance = accounts[i].getAmount();
                    try 
                    {
                        balance -= double.Parse(tbAmount.Text);
                        recieverBalance += double.Parse(tbAmount.Text);
                    }
                    catch (FormatException)
                    {
                        System.Windows.Forms.MessageBox.Show("Please enter only numerical values.");
                        return;
                    }
                    //Updates class objects with new balances and then inserts changes into database
                    senderAccount.setAmount(balance);
                    recieverAccount.setAmount(recieverBalance);
                    SqLiteDataAccess.SaveWithdrawal(senderAccount);
                    SqLiteDataAccess.SaveWithdrawal(recieverAccount);
                    System.Windows.Forms.MessageBox.Show("Transfer successful, please exit out to see changes.");
                }
            }
        }

        private void LoadAccounts()
        {
            accounts = SqLiteDataAccess.LoadAccounts();
            clients = SqLiteDataAccess.LoadClients();
            Client current = clients[Int16.Parse(ID_korisnika) - 1];

            for (var i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].getID() == (Int16.Parse(ID_korisnika)))
                {
                    clientAccounts.Add(accounts[i]);

                }
            }


            for (var i = 0; i < clientAccounts.Count; i++)
            {

                accountList.Items.Add(
                    Text = clientAccounts[i].getNumber() + " "
                    + clientAccounts[i].getAmount() + " "
                    + clientAccounts[i].getCurrency()
                );

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(ID_korisnika);
            f2.Show();
            this.Close();
        }

        
    }
}
