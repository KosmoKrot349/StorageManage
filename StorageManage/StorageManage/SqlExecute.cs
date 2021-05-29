using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace StorageManage
{
    public class SqlExecute
    {
        public string connectionString;
        MySqlConnection con;
        public SqlExecute(string connectionString) {
             this.connectionString= connectionString;
            con = new MySqlConnection(connectionString);
        }
        public MySqlDataReader returnResult(string sql) {
            try
            {
                con.Open();
                MySqlCommand command = new MySqlCommand(sql, con);
                MySqlDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch(MySqlException exception) { MessageBox.Show(exception.Message); return null; }
        }
 
        public void ExecuteWithoutRedaer(string sql)
        {
            try
            {
                con.Open();
            MySqlCommand command = new MySqlCommand(sql, con);
           command.ExecuteNonQuery();
            con.Close();
            }
            catch (MySqlException exception) { MessageBox.Show(exception.Message); }

        }

        public void AddChangeToSimpleTable(string sql, DataTable table)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand(sql, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comm);
            MySqlCommandBuilder comandbuilder = new MySqlCommandBuilder(adapter);
            adapter.Update(table);
            }
            catch (MySqlException exception) { MessageBox.Show(exception.Message); }
        }
        public DataTable retuernTable(string sql) {
            try
            {
            DataTable Table = new DataTable();
            con.Open();
            MySqlDataAdapter Adapter = new MySqlDataAdapter(sql, con);
            Adapter.Fill(Table);
            con.Close();
            return Table;
            }
            catch (MySqlException exception) { MessageBox.Show(exception.Message); DataTable Table = new DataTable(); return Table; }
        }
        public void closeCon()
        {
            con.Close();
        }
    }
}
