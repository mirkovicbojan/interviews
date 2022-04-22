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
    public partial class Form3 : Form
    {
        
        public string ID_korisnika { get; set; }
        private List<Account> accounts = new List<Account>();
        private List<Account> clientAccounts = new List<Account>();
        private List<Client> clients = new List<Client>();
        public Form3(string ID)
        {
            InitializeComponent();
            this.ID_korisnika = ID; //redundant?? probably.
            LoadClientAccounts();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        /*
         LoadClientAccount loads entire client and accounts tables into form 3 
         as lists instead of just client accounts at certain ID
         should find a way to send account list as argument to form3 instead of just an ID
         */
        private void LoadClientAccounts()
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(ID_korisnika);
            f2.Show();
            this.Close();
        }

        private void accountList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Apply buttons don't refresh the form so you need to exit out to see changes. Should find a way to fix this.
        private void apply1_Click(object sender, EventArgs e)
        {
            double balance = 0;
            Client current = clients[Int16.Parse(ID_korisnika) - 1];
            for (var i = 0; i<accountList.Items.Count; i++)
            {
                if(accountList.SelectedIndex == i)
                {
                    balance = clientAccounts[i].getAmount();
                    if (current.getType() == 1)
                    {
                        try
                        {
                            balance = balance - (double.Parse(textBoxFrom.Text) + (double.Parse(textBoxFrom.Text) * 0.01));
                        }
                        catch (FormatException)
                        {
                            System.Windows.Forms.MessageBox.Show("Please use numerical values.");
                        }
                    }
                    else if(current.getType() == 2)
                    {
                        try
                        {
                            balance = balance - double.Parse(textBoxFrom.Text);
                        }
                        catch (FormatException)
                        {
                            System.Windows.Forms.MessageBox.Show("Please use numerical values.");
                        }
                    }
                    
                    
                    Console.WriteLine(balance.ToString());
                    Console.WriteLine(clientAccounts[i].getAmount().ToString());
                    clientAccounts[i].setAmount(balance);
                    SqLiteDataAccess.SaveWithdrawal(clientAccounts[i]);
                }
            }

            System.Windows.Forms.MessageBox.Show("Balance updated. Exit out to see changes.");
        }

        private void apply2_Click(object sender, EventArgs e)
        {
            double balance = 0;

            for (var i = 0; i < accountList.Items.Count; i++)
            {
                if (accountList.SelectedIndex == i)
                {
                    balance = clientAccounts[i].getAmount();
                    try
                    {
                        balance = balance + double.Parse(textBoxTo.Text);
                        System.Windows.Forms.MessageBox.Show("Balance updated. Exit out to see changes.");
                    }
                    catch (FormatException)
                    {
                        System.Windows.Forms.MessageBox.Show("Please use numerical values.");
                    }
                    Console.WriteLine(balance.ToString());
                    Console.WriteLine(clientAccounts[i].getAmount().ToString());
                    clientAccounts[i].setAmount(balance);
                    SqLiteDataAccess.SaveWithdrawal(clientAccounts[i]);
                }
            }
            
        }
    }
}
