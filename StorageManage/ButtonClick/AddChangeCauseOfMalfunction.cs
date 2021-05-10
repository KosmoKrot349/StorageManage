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
    class AddChangeCauseOfMalfunction : IButtonClick
    {
        MainWindow window;

        public AddChangeCauseOfMalfunction(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataTable table = new DataTable();
            table.Columns.Add("idcauses", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));

            ArrayList list = new ArrayList();
            for (int i = 0; i < window.CausesOfMalfunctionsDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = window.CausesOfMalfunctionsDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if (rMas[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указана причина поломки"); return; }
                if (list.IndexOf(rMas[1]) != -1) { MessageBox.Show("Повторяется причина поломки " + rMas[1]); return; }
                list.Add(rMas[1]);
                table.ImportRow(row);
            }

            window.ex.AddChangeToSimpleTable("select * from causesofmalfunction", table);
            DataGridUpdater.CausesOfMalfunctionsDataGridUpdate(window);
        }
    }
}
