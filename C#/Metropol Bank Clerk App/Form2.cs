using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Metropol_Bank_Clerk_App
{
    public partial class Form2 : Form
    {
        public string ID_korisnika { get; set; }
        private List<Client> clients = new List<Client>();
        private List<Account> accounts = new List<Account>();
        private List<Account> clientAccounts = new List<Account>();

        //Form2 constructor recieves the ID of the client selected in Form1
        public Form2(string ID)
        {
            InitializeComponent();
            pnlNav.Hide();
            this.ID_korisnika = ID;
            LoadCurrentClient();
            
        }
        //Loads info of current client
        private void LoadCurrentClient()
        {
            clients = SqLiteDataAccess.LoadClients();
            Client current = clients[Int16.Parse(ID_korisnika) - 1];
            labelClient.Text = current.FullName;

            /*Creates new button and assigns it an event handler
              that can take the current client as a parameter 
             * This is done so the changes to a certain client can be first changed
             * to instance of client class and then to database so there is no need to reload the client table
             * to get new address.
             */
            Button btnSave = new Button()
            {
                Text = "Save",
                FlatStyle = FlatStyle.Flat,
                Font = new Font(Button.DefaultFont.FontFamily, 8)
            };
            Button btnVIP = new Button()
            {
                Text = "Change",
                FlatStyle = FlatStyle.Flat,
                Font = new Font(Button.DefaultFont.FontFamily, 8)
            };
            btnVIP.Click += (sender, e) => changeClientType(sender, e, current);
            btnSave.Click += (sender, e) => saveAddress(sender, e, current);

            tableClientInfo.RowCount = 6;
            tableClientInfo.ColumnCount = 2;

            tableClientInfo.Controls.Add(new Label()
            {
                Text = current.getName(),
                AutoSize = true
            }, 0, 0);
            tableClientInfo.Controls.Add(new Label()
            {
                Text = current.getSurname(),
                AutoSize = true
            }, 0, 1);
            tableClientInfo.Controls.Add(new Label()
            {
                Text = current.DOB,
                AutoSize = true
            }, 0, 2);
            tableClientInfo.Controls.Add(btnSave, 1, 3);
            tableClientInfo.Controls.Add(btnVIP, 1, 5);
            /*Should find a way to print out the country instead of the country code,
             * could be done with an API or possibly
             * by creating a database table with countries the bank is located in
            */

            tableClientInfo.Controls.Add(new Label()
            {
                Text = SqLiteDataAccess.returnCountry(current.getCountry()),
                AutoSize = true
            }, 0, 4);

            //Checks client type (integer value) and sets type of client label text
            if (current.getType() == 1)
            {
                tableClientInfo.Controls.Add(new Label()
                {
                    Text = "Regular client",
                    AutoSize = true
                }, 0, 5);
            }
            else if (current.getType() == 2)
            {
                tableClientInfo.Controls.Add(new Label()
                {
                    Text = "Premium client",
                    AutoSize = true
                }, 0, 5);
            }

            addressBox.Text = current.getAddress();
            addressBox.Width = 300;

            LoadClientAccounts();
        }

        /*Loads the current client's accounts by going through 
         * all existing bank accounts and prints ones that match the 
         * client ID to a table. Slow!
         */
        private void LoadClientAccounts()
        {
            accounts = SqLiteDataAccess.LoadAccounts();


            for (var i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].getID() == (Int16.Parse(ID_korisnika)))
                {
                    clientAccounts.Add(accounts[i]);
                    Console.WriteLine("Added an account.");
                }
            }
            accountTable.RowCount = clientAccounts.Count;

            for (var i = 0; i < clientAccounts.Count; i++)
            {
                Console.WriteLine("Added an account row.");

                accountTable.Controls.Add(new Label() { Text = clientAccounts[i].getNumber(), AutoSize = true }, 0, i);
                if (clientAccounts[i].returnType() == 1)
                {
                    accountTable.Controls.Add(new Label() { Text = "DEBIT", AutoSize = true }, 1, i);
                }
                else
                {
                    accountTable.Controls.Add(new Label() { Text = "CREDIT", AutoSize = true }, 1, i);
                }
                accountTable.Controls.Add(new Label() { Text = clientAccounts[i].getAmount().ToString(), AutoSize = true }, 2, i);
                accountTable.Controls.Add(new Label() { Text = clientAccounts[i].getCurrency(), AutoSize = true, TextAlign = ContentAlignment.MiddleCenter }, 3, i);
                if (clientAccounts[i].getTypeofPayment() == 1) 
                {
                    accountTable.Controls.Add(new Label() { Text = "Monthly" }, 4, i);

                }
                else if (clientAccounts[i].getTypeofPayment() == 2) 
                {
                    accountTable.Controls.Add(new Label() { Text = "Quarterly" }, 4, i);
                }
                else 
                {
                    accountTable.Controls.Add(new Label() { Text = "Yearly" }, 4, i);
                }
            }
        }

        //saves client address upon clicking the dynamically added button in LoadCurrentClient()
        private void saveAddress(object sender, EventArgs e, Client client)
        {
            string new_address = addressBox.Text;
            client.setAddress(new_address);
            SqLiteDataAccess.SaveNewAddress(client);
            tableClientInfo.Refresh();
            System.Windows.Forms.MessageBox.Show("Clients Address has been updated.");
        }

        private void changeClientType(object sender, EventArgs e, Client client) 
        {
            int clientType = client.getType();
            if (clientType == 1)
            {
                client.setType(2);
            }
            else 
            {
                client.setType(1);
            }
            SqLiteDataAccess.changeClientType(client);
            tableClientInfo.Refresh();
            System.Windows.Forms.MessageBox.Show("Clients type has been changed re-enter to see changes.");
        }

        //Goes back to client list
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            pnlNav.Show();
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);
            btnWithdraw.BackColor = Color.FromArgb(24, 30, 54);
            btnTransfer.BackColor = Color.FromArgb(24, 30, 54);
            btnSettings.BackColor = Color.FromArgb(24, 30, 54);
            Form f1 = new Form1();
            f1.Show();
            this.Close();
        }

        //Opens withdrawal form
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            pnlNav.Show();
            pnlNav.Height = btnWithdraw.Height;
            pnlNav.Top = btnWithdraw.Top;
            pnlNav.Left = btnWithdraw.Left;
            btnWithdraw.BackColor = Color.FromArgb(46, 51, 73);
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
            btnTransfer.BackColor = Color.FromArgb(24, 30, 54);
            btnSettings.BackColor = Color.FromArgb(24, 30, 54);
            //Opens withdrawal form
            Form3 f3 = new Form3(ID_korisnika);
            f3.Show();
            this.Hide();
        }

        //Opens transfer form
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            pnlNav.Show();
            pnlNav.Height = btnTransfer.Height;
            pnlNav.Top = btnTransfer.Top;
            pnlNav.Left = btnTransfer.Left;
            btnTransfer.BackColor = Color.FromArgb(46, 51, 73);
            btnWithdraw.BackColor = Color.FromArgb(24, 30, 54);
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
            btnSettings.BackColor = Color.FromArgb(24, 30, 54);
            Form4 f4 = new Form4(ID_korisnika);
            f4.Show();
            this.Hide();
        }

        //Placeholder, doesn't do anything yet.
        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlNav.Show();
            pnlNav.Height = btnSettings.Height;
            pnlNav.Top = btnSettings.Top;
            pnlNav.Left = btnSettings.Left;
            btnSettings.BackColor = Color.FromArgb(46, 51, 73);
            btnWithdraw.BackColor = Color.FromArgb(24, 30, 54);
            btnTransfer.BackColor = Color.FromArgb(24, 30, 54);
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        //Exit button
        private void button1_Click(object sender, EventArgs e)
        {
            Form f1 = new Form1();
            f1.Show();
            this.Close();
        }

       
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
         
        }
    }
}
