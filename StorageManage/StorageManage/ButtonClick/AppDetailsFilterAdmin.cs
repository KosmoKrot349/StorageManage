using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManage.ButtonClick
{
    class AppDetailsFilterAdmin:IButtonClick
    {
        MainWindow window;

        public AppDetailsFilterAdmin(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.filter.ApplyDetailsFiltr();
            DataGridUpdater.DetailsDataGridUpdate(window, window.filter.sql);
        }
    }
}
