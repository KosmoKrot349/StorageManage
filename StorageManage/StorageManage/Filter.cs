using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StorageManage
{
  public  class Filter
    {
        public CheckBox[] chbxMas;
        public string sql = "";
        public void CreateDetailsFiltr(Grid Grid)
        {
            Grid.Children.Clear();
            Grid.ColumnDefinitions.Clear();
            chbxMas = new CheckBox[3];
            for (int i = 0; i < 3; i++)
            {
                chbxMas[i] = new CheckBox();
                switch (i)
                {
                    case 0: { chbxMas[i].Content = "Заказанные"; chbxMas[i].Name = "FilterDetailsOrdered"; break; }
                    case 1: { chbxMas[i].Content = "Есть в наличии"; chbxMas[i].Name = "FilterDetailsStorage"; break; }
                    case 2: { chbxMas[i].Content = "Закончились"; chbxMas[i].Name = "FilterDetailsEmpty"; break; }
                }
                ColumnDefinition cmd = new ColumnDefinition();
                Grid.ColumnDefinitions.Add(cmd);
                Grid.SetColumn(chbxMas[i], i);
                Grid.Children.Add(chbxMas[i]);

            }
        }
        public void ApplyDetailsFiltr()
        {
            sql = "select * from details where iddetails!=-1";
            if (chbxMas[0].IsChecked == true)
            {
                sql += " and ordered != 0 ";
            }
            if (chbxMas[2].IsChecked == true)
            {
                sql += " and storage = 0 ";
            }
            if (chbxMas[1].IsChecked == true)
            {
                sql += " and storage != 0 ";
            }

        }
    }
}
