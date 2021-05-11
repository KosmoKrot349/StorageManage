using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class AddChangeClients:IButtonClick
    {
        MainWindow window;

        public AddChangeClients(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataTable table = new DataTable();
            table.Columns.Add("idclients", System.Type.GetType("System.Int32"));
            table.Columns.Add("name", System.Type.GetType("System.String"));
            table.Columns.Add("phone", System.Type.GetType("System.String"));

            ArrayList list = new ArrayList();
            for (int i = 0; i < window.ClientsDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = window.ClientsDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if (rMas[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано имя клиента"); return; }
                if (rMas[2].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указан телефон клиента"); return; }
                if (rMas[2].ToString().Length > 12) { MessageBox.Show("В " + (i + 1) + " строке не верный телефон клиента"); return; }
                if (list.IndexOf(rMas[2]) != -1) { MessageBox.Show("Повторяется телефон клиента " + rMas[2]); return; }
                list.Add(rMas[2]);
                table.ImportRow(row);
            }

            window.ex.AddChangeToSimpleTable("select * from clients", table);
            DataGridUpdater.ClientsDataGridUpdate(window);
        }
    }
}
