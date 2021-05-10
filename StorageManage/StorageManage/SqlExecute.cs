using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
           
            con.Open();
            MySqlCommand command = new MySqlCommand(sql, con);
            MySqlDataReader reader = command.ExecuteReader();
            return reader;
        }
 
        public void ExecuteWithoutRedaer(string sql)
        {
            con.Open();
            MySqlCommand command = new MySqlCommand(sql, con);
           command.ExecuteNonQuery();
            con.Close();

        }

        public void AddChangeToSimpleTable(string sql, DataTable table)
        {
            MySqlCommand comm = new MySqlCommand(sql, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comm);
            MySqlCommandBuilder comandbuilder = new MySqlCommandBuilder(adapter);
            adapter.Update(table);
        }
        public DataTable retuernTable(string sql) {
            DataTable Table = new DataTable();
            con.Open();
            MySqlDataAdapter Adapter = new MySqlDataAdapter(sql, con);
            Adapter.Fill(Table);
            con.Close();
            return Table;
        }
        public void closeCon()
        {
            con.Close();
        }
    }
}
