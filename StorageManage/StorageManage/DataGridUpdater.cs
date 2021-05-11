using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManage
{
    class DataGridUpdater
    {
        public static void UserDataGridUpdate(MainWindow window)
        {
            DataTable table = new DataTable();
            object[] sqlMass = new object[4];
            table.Columns.Add("id", System.Type.GetType("System.Int32"));
            table.Columns.Add("login", System.Type.GetType("System.String"));
            table.Columns.Add("password", System.Type.GetType("System.String"));
            table.Columns.Add("isconfirmed", System.Type.GetType("System.String"));
            MySqlDataReader reader = window.ex.returnResult("select * from users where id!=1");
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    sqlMass[0] = reader.GetInt32(0);
                    sqlMass[1] = reader.GetString(1);
                    sqlMass[2] = reader.GetString(2);
                    if (reader.GetBoolean(3) == true) { sqlMass[3] = "Одобрен"; } else { sqlMass[3] = "Не одобрен"; }
                    DataRow row;
                    row = table.NewRow();
                    row.ItemArray = sqlMass;
                    table.Rows.Add(row);
                }

            }
            window.ex.closeCon();
            window.UserDataGrid.ItemsSource = table.DefaultView;

        }

        public static void DetailsDataGridUpdate(MainWindow window, string sql)
        {
            DataTable table = new DataTable();
            object[] sqlMass = new object[7];
            table.Columns.Add("iddetails", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));
            table.Columns.Add("storage", System.Type.GetType("System.Int32"));
            table.Columns.Add("ordered", System.Type.GetType("System.Int32"));
            table.Columns.Add("saled", System.Type.GetType("System.Int32"));
            table.Columns.Add("price", System.Type.GetType("System.Double"));
            table.Columns.Add("isimportant", System.Type.GetType("System.String"));
            MySqlDataReader reader = window.ex.returnResult(sql);
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    sqlMass[0] = reader.GetInt32(0);
                    sqlMass[1] = reader.GetString(1);
                    sqlMass[2] = reader.GetInt32(2);
                    sqlMass[3] = reader.GetInt32(3);
                    sqlMass[4] = reader.GetInt32(4);
                    sqlMass[5] = reader.GetDouble(5);
                    if (reader.GetBoolean(6) == true) { sqlMass[6] = "Важная"; } else { sqlMass[6] = "Не важная"; }
                    DataRow row;
                    row = table.NewRow();
                    row.ItemArray = sqlMass;
                    table.Rows.Add(row);
                }

            }
            window.ex.closeCon();
            window.DetailsDataGrid.ItemsSource = table.DefaultView;

        }

        public static void OrdersDataGridUpdate(MainWindow window)
        {
            DataTable table = new DataTable();
            object[] sqlMass = new object[7];
            table.Columns.Add("idorders", System.Type.GetType("System.Int32"));
            table.Columns.Add("detTitle", System.Type.GetType("System.String"));
            table.Columns.Add("person", System.Type.GetType("System.String"));
            table.Columns.Add("orderdate", System.Type.GetType("System.String"));
            table.Columns.Add("orderprice", System.Type.GetType("System.Double"));
            table.Columns.Add("quantity", System.Type.GetType("System.Int32"));
            table.Columns.Add("iscompleet", System.Type.GetType("System.String"));
            MySqlDataReader reader = window.ex.returnResult("select orders.idorders,details.title,users.login,orders.orderdate,orders.orderprice,orders.quantity,orders.iscompleet from orders inner join users using(id) inner join details using(iddetails)");
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    sqlMass[0] = reader.GetInt32(0);
                    sqlMass[1] = reader.GetString(1);
                    sqlMass[2] = reader.GetString(2);
                    sqlMass[3] = reader.GetDateTime(3).ToShortDateString();
                    sqlMass[4] = reader.GetDouble(4);
                    sqlMass[5] = reader.GetInt32(5);
                    if (reader.GetBoolean(6) == true) { sqlMass[6] = "Получен"; } else { sqlMass[6] = "Ожидается"; }
                    DataRow row;
                    row = table.NewRow();
                    row.ItemArray = sqlMass;
                    table.Rows.Add(row);
                }

            }
            window.ex.closeCon();
            window.OrdersDataGrid.ItemsSource = table.DefaultView;

        }

        public static void TypesOfDevicesDataGridUpdate(MainWindow window)
        {
            window.TypesOfDevicesDataGrid.ItemsSource = window.ex.retuernTable("SELECT * from typeofdevices").DefaultView;
        }

        public static void DevicesDataGridUpdate(MainWindow window)
        {
            DataTable table = new DataTable();
            object[] sqlMass = new object[3];
            table.Columns.Add("iddevices", System.Type.GetType("System.Int32"));
            table.Columns.Add("titledevice", System.Type.GetType("System.String"));
            table.Columns.Add("titletype", System.Type.GetType("System.String"));
            MySqlDataReader reader = window.ex.returnResult("select devices.iddevices,devices.title,typeofdevices.title from devices inner join typeofdevices using(idtypes)");
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    sqlMass[0] = reader.GetInt32(0);
                    sqlMass[1] = reader.GetString(1);
                    sqlMass[2] = reader.GetString(2);
                    DataRow row;
                    row = table.NewRow();
                    row.ItemArray = sqlMass;
                    table.Rows.Add(row);
                }

            }
            window.ex.closeCon();
            window.DevicesDataGrid.ItemsSource = table.DefaultView;

        }
        public static void CausesOfMalfunctionsDataGridUpdate(MainWindow window)
        {
            window.CausesOfMalfunctionsDataGrid.ItemsSource = window.ex.retuernTable("SELECT * from causesofmalfunction").DefaultView;
        }

        public static void MalfunctionsDataGridUpdate(MainWindow window)
        {
            DataTable table = new DataTable();
            object[] sqlMass = new object[3];
            table.Columns.Add("idmalfunctions", System.Type.GetType("System.Int32"));
            table.Columns.Add("titlemalfunction", System.Type.GetType("System.String"));
            table.Columns.Add("titletype", System.Type.GetType("System.String"));
            MySqlDataReader reader = window.ex.returnResult("select malfunctions.idmalfunctions,malfunctions.title,typeofdevices.title from malfunctions inner join typeofdevices using(idtypes)");
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    sqlMass[0] = reader.GetInt32(0);
                    sqlMass[1] = reader.GetString(1);
                    sqlMass[2] = reader.GetString(2);
                    DataRow row;
                    row = table.NewRow();
                    row.ItemArray = sqlMass;
                    table.Rows.Add(row);
                }

            }
            window.ex.closeCon();
            window.MalfunctionsDataGrid.ItemsSource = table.DefaultView;

        }

        public static void ClientsDataGridUpdate(MainWindow window)
        {
            window.ClientsDataGrid.ItemsSource = window.ex.retuernTable("SELECT * from clients").DefaultView;
        }


        public static void RepairOrdersDataGridUpdate(MainWindow window)
        {
            DataTable table = new DataTable();
            object[] sqlMass = new object[7];
            table.Columns.Add("idrepairorders", System.Type.GetType("System.Int32"));
            table.Columns.Add("clientname", System.Type.GetType("System.String"));
            table.Columns.Add("devicetitle", System.Type.GetType("System.String"));
            table.Columns.Add("datestart", System.Type.GetType("System.String"));
            table.Columns.Add("dateend", System.Type.GetType("System.String"));
            table.Columns.Add("state", System.Type.GetType("System.String"));
            table.Columns.Add("desc", System.Type.GetType("System.String"));
            MySqlDataReader reader = window.ex.returnResult("select repairorders.idrepairorders,clients.name,devices.title,repairorders.datestart,repairorders.dateend,repairorders.state,repairorders.desc from repairorders inner join clients using(idclients) inner join devices using(iddevices)");
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    sqlMass[0] = reader.GetInt32(0);
                    sqlMass[1] = reader.GetString(1);
                    sqlMass[2] = reader.GetString(2);
                    sqlMass[3] = reader.GetDateTime(3).ToShortDateString();
                    sqlMass[4] = reader.GetDateTime(4).ToShortDateString();
                    sqlMass[5] = reader.GetString(5);
                    sqlMass[6] = reader.GetString(6);
                    DataRow row;
                    row = table.NewRow();
                    row.ItemArray = sqlMass;
                    table.Rows.Add(row);
                }

            }
            window.ex.closeCon();
            window.RepairOrdersDataGrid.ItemsSource = table.DefaultView;

        }
    }
}
