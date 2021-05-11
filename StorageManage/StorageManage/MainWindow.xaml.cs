using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StorageManage.ButtonClick;
using StorageManage.MenuClick;
using StorageManage.SelectionChanged;
using System.IO;

namespace StorageManage
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string connectionstring = "";
        public string currentUserLogin;
        public SqlExecute ex;
        IButtonClick actionReactButton;
        IMenuClick actionReactMenuItem;
        ISelectionChanged actionReactComboBox;
        public HideAllGrids hd;
        public Filter filter = new Filter();
        //для изменения пользователей
        public string unChangeLogin;
        public int userIdForChange;
        //для изменения запчастей
        public string unChangeDetailTitle;
        public int detailIdForChange;
        //для изменения заказа
        public int orderQuantity;
        public int orderIdForChange;
        //для изменения устройства
        public int deviceIdForChange;
        public string unChangeDeviceTitle;
        //максв чекбоксов для добавления деталей
        public CheckBox[] detailsCheckBoxMas;
        //для изменения неисправности
        public int malfunctionIdForChange;
        public string unChangeMalfunctionTitle;
        // id клиента
        public int clientID;
        //для изменения заказа
        public int repairOrderForChange;
        //максв чекбоксов для добавления деталей
        public CheckBox[] causesForRepairOrderCheckBoxMas;
        public MainWindow()
        {
            InitializeComponent();
            StreamReader sReader = new StreamReader(@"settings.txt");
            string connString = "";
            while (!sReader.EndOfStream)
            {
                connString += sReader.ReadLine() + ";";
            }
            sReader.Close();
            connectionstring = "server="+ connString.Split(';')[0].Split(':')[1] + ";database="+ connString.Split(';')[1].Split(':')[1] + ";uid="+ connString.Split(';')[2].Split(':')[1] + ";password="+ connString.Split(';')[3].Split(':')[1] + ";";
            ex = new SqlExecute(connectionstring);
            hd =new HideAllGrids(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                //авторизация
                case "AuthorAuthor": { actionReactButton = new Authorization(this); break; }
                    //переход к регистрации
                case "AuthorRegister": { actionReactButton = new GoToRegistration(this); break; }
                    //регистрация
                case "RegRegister": { actionReactButton = new Registration(this); break; }
                    //удаление пользователя
                case "DelUser": { actionReactButton = new DeleteUser(this); break; }
                    //подтверждение пользователя/снятие подтверждения
                case "Confirmed": { actionReactButton = new ConfirmUnconfirmUser(this); break; }
                    //переход к изменению пользователя
                case "GoToChangeUser": { actionReactButton = new GoToChangeUser(this); break; }
                    //изменение пользователя
                case "ChangeUser": { actionReactButton = new ChangeUser(this); break; }
                //переход к добавлению детали
                case "GoToAddDetail": { actionReactButton = new GoToAddDetail(this); break; }
                    //добавление детали
                case "AddDetail": { actionReactButton = new AddDetail(this); break; }
                //переход к редактированию детали
                case "GoToChangeDetail": { actionReactButton = new GoToChangeDetail(this); break; }
                //изменение детали
                case "ChangeDetail": { actionReactButton = new ChangeDetail(this); break; }
                //удаление детали
                case "DelDetail": { actionReactButton = new DeleteDetail(this); break; }
                //изменение важносит детали
                case "ImportantDetail": { actionReactButton = new ImportantUnimportantDetail(this); break; }
                //Применение фильтра по деталям
                case "AppDetailsFilterAdmin": { actionReactButton = new AppDetailsFilterAdmin(this); break; }
                //Переход к добавлению заказа на деталь
                case "GoToAddOrder": { actionReactButton = new GoToAddOrder(this); break; }
                //Добавление заказа на деталь
                case "AddOrder": { actionReactButton = new AddOrder(this); break; }
                //Удаление заказа на деталь
                case "DelOrder": { actionReactButton = new DeleteOrder(this); break; }
                //переход к изменению заказа на деталь
                case "GoToChangeOrder": { actionReactButton = new GoToChangeOrder(this); break; }
                //изменение заказа на деталь
                case "ChangeOrder": { actionReactButton = new ChangeOrder(this); break; }
                //Получение заказа на деталь
                case "CompleetOrder": { actionReactButton = new CompleetOrder(this); break; }
                //Добавление заказа от менеджера из деталей
                case "GoToAddOrderManagerFromDetails": { actionReactButton = new GoToAddOrder(this); break; }
                //Добавление заказа от админа из деталей
                case "GoToAddOrderAdmin": { actionReactButton = new GoToAddOrder(this); break; }
                //Переход к добавлению менеджер
                case "GoToAddOrderManager": { actionReactButton = new GoToAddOrder(this); break; }
                //Переход к редактированию менеджер
                case "GoToChangeOrderManager": { actionReactButton = new GoToChangeOrder(this); break; }
                //Добавление изменение типов устройств
                case "AddChangeTypeOfDevice": { actionReactButton = new AddChangeTypeOfDevice(this); break; }
                //Удаление типа устройства
                case "DelTypeOfDevice": { actionReactButton = new DeleteTypeOfDevice(this); break; }
                //переход к добавлению устройства
                case "GoToAddDevice": { actionReactButton = new GoToAddDevice(this); break; }
                //добавление устройства
                case "AddDevice": { actionReactButton = new AddDevice(this); break; }
                //удаление устройства
                case "DelDevice": { actionReactButton = new DeleteDevice(this); break; }
                //переход к изменению устройства
                case "GoToChangeDevice": { actionReactButton = new GoToChangeDevice(this); break; }
                //изменение устройства
                case "ChangeDevice": { actionReactButton = new ChangeDevice(this); break; }
                //переход к добавлению деталей для устройства
                case "GoToDetailsOfThisDevice": { actionReactButton = new GoToDetailsForDevice(this); break; }
                //добавление деталей для устройства
                case "AppDetilsForDevice": { actionReactButton = new AppDetilsForDevice(this); break; }
                //Добавление изменение типов неисправностей
                case "AddChangeCauseOfMalfunction": { actionReactButton = new AddChangeCauseOfMalfunction(this); break; }
                //Удаление типа неисправности
                case "DelCauseOfMalfunction": { actionReactButton = new DeleteCauseOfMalfunction(this); break; }
                //переход к добавлению неиспраности
                case "GoToAddMalfunction": { actionReactButton = new GoToAddMalfunction(this); break; }
                //добавление неисправности
                case "AddMalfunction": { actionReactButton = new AddMalfunction(this); break; }
                //удаление неисправности
                case "DelMalfunction": { actionReactButton = new DeleteMalfunction(this); break; }
                //переход к изменению неисправности 
                case "GoToChangeMalfunction": { actionReactButton = new GoToChangeMalfunction(this); break; }
                //изменение неисправности
                case "ChangeMalfunction": { actionReactButton = new ChangeMalfunction(this); break; }

                //переход к добавлению деталей для устранения неисправности
                case "GoToDetailsOfThisMalfunction": { actionReactButton = new GoToDetailsOfThisMalfunction(this); break; }
                //добавление деталей для устранения неисправности
                case "AppDetilsForMalfunction": { actionReactButton = new AppDetilsForMalfunction(this); break; }

                //переход к добавлению причин неисправности
                case "GoToCausesOfThisMalfunction": { actionReactButton = new GoToCausesOfThisMalfunction(this); break; }
                //добавление причин для неисправности
                case "AppCausesForMalfunction": { actionReactButton = new AppCausesForMalfunction(this); break; }
                //добавление изменение клиента
                case "AddChangeClients": { actionReactButton = new AddChangeClients(this); break; }
                //добавление изменение клиента
                case "DelClient": { actionReactButton = new DeleteClient(this); break; }
                //переход к добавлению устройств клиента
                case "GoToDevicesOfThisClient": { actionReactButton = new GoToDevicesOfThisClient(this); break; }
                //добавление устройств к клиенту
                case "AppDevicesForClients": { actionReactButton = new AppDevicesForClients(this); break; }
                //переход к добавлению заказа на починку 
                case "GoToAddRepairOrder": { actionReactButton = new GoToAddRepairOrder(this); break; }
                //добавление заказа на починку 
                case "AddRepairOrder": { actionReactButton = new AddRepairOrder(this); break; }

                //переход к изменению заказа на починку
                case "GoToChangeRepairOrder": { actionReactButton = new GoToChangeRepairOrder(this); break; }
                //изменение заказа на починку 
                case "ChangeRepairOrder": { actionReactButton = new ChangeRepairOrder(this); break; }
                //Удаление заказа на починку  
                case "DelRepairOrder": { actionReactButton = new DeleteRepairOrder(this); break; }

                //добавление неисправностей к заказу на починку
                case "GoToMalFunctionsOfThisRepairOrder": { actionReactButton = new GoToMalFunctionsOfThisRepairOrder(this); break; }
                //Расчёт вероятностей поломок
                case "CountProbaility": { actionReactButton = new CountProbaility(this); break; }
                //добавление поломок к заказу на починку
                case "AppMalfunctionsForRepairOrder": { actionReactButton = new AppMalfunctionsForRepairOrder(this); break; }
                //попытка ремонта тезники из заказа
                case "RepairDevice": { actionReactButton = new RepairDevice(this); break; }
                //Переход к настройкам
                case "GoToSettings": { actionReactButton = new GoToSettings(this); break; }
                //Выход из настроек
                case "LeaveSettings": { actionReactButton = new LeaveSettings(this); break; }
                //Сохранение настроек
                case "SaveSettings":{ actionReactButton = new SaveSettings(this); break; }
            }
            actionReactButton.ButtonClick();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mn = sender as MenuItem;
            switch (mn.Name)
            {
                //выйти, меню админа
                case "LeaveAdm": { actionReactMenuItem = new Leave(this); break; }
                //выйти меню менеджера
                case "LeaveMan": { actionReactMenuItem = new Leave(this); break; }
                //переход к пользователям
                case "UsersMenu": { actionReactMenuItem = new GoToUsers(this); break; }
                //детали, меню админа
                case "AdminDetailsMenu": { actionReactMenuItem = new GoToAdminDetails(this); break; }
                //детали, меню менеджера
                case "ManagerDetailsMenu": {actionReactMenuItem = new GoToAdminDetails(this); break; }
                //Заказы деталей, меню админа
                case "AdminOrdersMenu": { actionReactMenuItem = new GoToOrders(this); break; }
                //Заказы деталей, меню менеджера
                case "ManagerOrdersMenu": { actionReactMenuItem = new GoToOrders(this); break; }
                //Типы устройств
                case "TypesOfDevicesMenu": { actionReactMenuItem = new GoToTypesOfDevices(this); break; }
                    //Устройства
                case "DevicesMenu": { actionReactMenuItem = new GoToDevices(this); break; }
                //причины неисправностей
                case "CausesOfMalfunctionsMenu": { actionReactMenuItem = new GoToCausesOfMalfunctions(this); break; }
                    //неисправности
                case "MalfunctionsMenu": { actionReactMenuItem = new GoToMalfunctions(this); break; }
                //клиенты админ
                case "ClientsMenu": { actionReactMenuItem = new GoToClients(this); break; }
                //заказы на починку
                case "RepairOrdersMenu": { actionReactMenuItem = new GoToRepairOrders(this); break; }
                //заказы на починку менеджер
                case "RepairOrdersManagerMenu": { actionReactMenuItem = new GoToRepairOrders(this); break; }

            }
            actionReactMenuItem.MenuClick();
        }
        //ввод числе только с заяптой
        private void Label_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if ((!Char.IsDigit(e.Text, 0)) && (e.Text != ","))
            {
                e.Handled = true;
            }
            else
                if ((e.Text == ",") && ((tbne.Text.IndexOf(",") != -1) || (String.IsNullOrEmpty(tbne.Text))))
            {  e.Handled = true; }
        }
        //ввод простых чисел
        private void AddDetStorage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CMBX= sender as ComboBox;
            switch (CMBX.Name)
            {
                //Выбор клиента для выбора техники
                case "AddClientOfRepairOrder": { actionReactComboBox = new SelectClientFromAddRepairOrder(this); break; }
                    
            }
            actionReactComboBox.SelectionChanged();
        }
    }
}
