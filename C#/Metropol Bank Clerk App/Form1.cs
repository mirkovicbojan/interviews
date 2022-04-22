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
    public partial class Form1 : Form
    {
        
        List<Client> clients = new List<Client>();
        
        public Form1()
        {
            InitializeComponent();
            LoadClientTable();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        /*Loads up Client List and inserts them
         * as Labels in the table with a dynamically 
         * added button for each
        */
        private void LoadClientTable()
        {
            clientTable.Controls.Clear();
            clients = SqLiteDataAccess.LoadClients();
            clientTable.RowCount = clients.Count;
            clientTable.ColumnCount = 3;


            for (var i = 0; i<clients.Count; i++)
            {
                
                clientTable.Controls.Add(new Label() { Text = clients[i].FullName}, 0, i);
                clientTable.Controls.Add(new Label() { Text = clients[i].DOB}, 1, i);
                Button btn = new Button();
                btn.Name = "btnMore/" + (i+1);
                btn.Text = "More...";
                btn.Click += (sender, e) => moreInfo(sender, e, btn.Name);
                clientTable.Controls.Add(btn, 2, i);
            }
            
            
        }

        private void clientTable_Paint(object sender, PaintEventArgs e)
        {

        }

       private void moreInfo(object sender, EventArgs e, string ID)
        {
            /*takes the ID of the clicked item in table from the button ID
             * thats dynamicallly asigned during creation
             * by using the / in button's ID string as a seperator
             * There is probably a better way to do this.
            */
            string[] ID_split = ID.Split(new char[] { '/' });
            string ID_korisnika = ID_split[1];
            
            Form2 f2 = new Form2(ID_korisnika);
            f2.Show();
            this.Hide();
        }
    }
}
