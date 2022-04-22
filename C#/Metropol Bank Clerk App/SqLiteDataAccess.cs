using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace Metropol_Bank_Clerk_App
{
    class SqLiteDataAccess
    {
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        /*LoadAccounts and LoadClients gets used in almost every form 
         * because I don't know how to write a select that returns 
         * only one table. See in Form4.cs button1_click().
         * */
        public static List<Client> LoadClients()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Client>("select * from Client", new DynamicParameters());
                return output.ToList();

            }
        }

        public static List<Account> LoadAccounts()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                var output = cnn.Query<Account>("select * from Account", new DynamicParameters());
                return output.ToList();

            }
        }

        //Updates database address to be the same as class object address.
        public static void SaveNewAddress(Client client)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                using (SQLiteCommand command = new SQLiteCommand(cnn))
                {
                    command.CommandText = "update Client set address = :address where ID = :ID";
                    command.Parameters.Add("address", DbType.String).Value = client.getAddress();
                    command.Parameters.Add("ID", DbType.String).Value = client.getID();

                    command.ExecuteNonQuery();
                }
                cnn.Close();
            }
        }

        //Updates database balance to be the same as class object balance.
        public static void SaveWithdrawal(Account account)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                using (SQLiteCommand command = new SQLiteCommand(cnn))
                {
                    command.CommandText = "update Account set depositedAmount = :amount where accountID = :accountID";
                    command.Parameters.Add("amount", DbType.String).Value = account.getAmount();
                    command.Parameters.Add("accountID", DbType.String).Value = account.getAccountID();

                    command.ExecuteNonQuery();
                }
                cnn.Close();
            }
        }

        public static string returnCountry(int countryNumber)
        {
            string name = null;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                using (SQLiteCommand command = new SQLiteCommand(cnn))
                {
                    command.CommandText = "select name from Country where countryNumber = :countryNumber";
                    command.Parameters.Add("countryNumber", DbType.String).Value = countryNumber;
                    name = (string)command.ExecuteScalar();
                }
                cnn.Close();
            }
            return name;
        }

        public static void changeClientType(Client client)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                cnn.Open();
                using (SQLiteCommand command = new SQLiteCommand(cnn))
                {
                    command.CommandText = "update Client set type = :type where ID = :ID";
                    command.Parameters.Add("type", DbType.String).Value = client.getType();
                    command.Parameters.Add("ID", DbType.String).Value = client.getID();

                    command.ExecuteNonQuery();
                }
                cnn.Close();
            }
        }
    }
}
