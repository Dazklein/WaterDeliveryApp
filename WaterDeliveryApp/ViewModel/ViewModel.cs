using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WaterDeliveryApp.Domain;
using WaterDeliveryApp.Models;
using WaterDeliveryApp.View;

namespace WaterDeliveryApp.ViewModelPath
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {

        }

        public static List<WaterList>  WaterList { get; set; }

        public static Clients Client { get; set; }

        /*private Clients _client;
        public Clients Client 
        {
            get 
            {
                return _client;
            }
            set
            {
                _client = value;
                NotifyPropertyChanged("Client");
            }
        }*/
        public static Orders Order { get; set; }
        public static WaterTypes WaterType { get; set; }
        public static Clients SelectedClient { get; set; }
        private List<Clients> _clients = Data.GetClients();

        public List<Clients> Clients
        {
            get { return _clients; }
            set 
            { 
                _clients = value;
                NotifyPropertyChanged("Clients");
            }
        }

        public static WaterTypes SelectedWater { get; set; }
        private List<WaterTypes> _waterTypes = Data.GetWaterTypes();

        public List<WaterTypes> WaterTypes
        {
            get { return _waterTypes; }
            set 
            { 
                _waterTypes = value;
                NotifyPropertyChanged("WaterTypes");
            }
        }

        public static ViewOrders SelectedOrder { get; set; }
        private List<ViewOrders> _viewOrders = Data.GetViewOrders();

        public List<ViewOrders> ViewOrders
        {
            get { return _viewOrders; }
            set 
            {
                _viewOrders = value;
                NotifyPropertyChanged("ViewOrders");
            }
        }

        #region Команды открытия окон

        // Добавление
        private RelayCommand _addClientWnd;

        public RelayCommand AddClientWnd
        {
            get 
            {
                return _addClientWnd ?? new RelayCommand(obj =>
                {
                    AddClientWndMethod();
                });
            }
        }

        private RelayCommand _addWaterTypeWnd;

        public RelayCommand AddWaterTypeWnd
        {
            get 
            { 
                return _addWaterTypeWnd ?? new RelayCommand(obj =>
                {
                    AddWaterTypeWndMethod();
                }); 
            }
        }

        private RelayCommand _addOrderWnd;

        public RelayCommand AddOrderWnd
        {
            get 
            { 
                return _addOrderWnd ?? new RelayCommand(obj =>
                {
                    AddOrderWndMethod();
                });
            }
        }
        
        private RelayCommand _editClientWnd;

        public RelayCommand EditClientWnd
        {
            get
            {
                return _editClientWnd ?? new RelayCommand(obj =>
                {
                    EditClientWndMethod();
                });
            }
        }

        private RelayCommand _editWaterTypeWnd;

        public RelayCommand EditWaterTypeWnd
        {
            get
            {
                return _editWaterTypeWnd ?? new RelayCommand(obj =>
                {
                    EditWaterTypeWndMethod();
                });
            }
        }

        private RelayCommand _editOrderWnd;

        public RelayCommand EditOrderWnd
        {
            get
            {
                return _editOrderWnd ?? new RelayCommand(obj =>
                {
                    EditOrderWndMethod();
                });
            }
        }

        private void AddClientWndMethod()
        {
            CreateNewClient createNewClient = new CreateNewClient();
            SetCenterPositionAndOpen(createNewClient);
        }

        private void AddOrderWndMethod()
        {
            CreateNewOrder createNewOrder = new CreateNewOrder();
            SetCenterPositionAndOpen(createNewOrder);
        }

        private void AddWaterTypeWndMethod()
        {
            CreateNewTypeWater createNewTypeWater = new CreateNewTypeWater();
            SetCenterPositionAndOpen(createNewTypeWater);
        }

        private void EditClientWndMethod()
        {
            EditClient editClient = new EditClient();
            SetCenterPositionAndOpen(editClient);
        }

        private void EditOrderWndMethod()
        {
            EditOrder editOrder = new EditOrder();
            SetCenterPositionAndOpen(editOrder);
        }

        private void EditWaterTypeWndMethod()
        {
            EditTypeWater editTypeWater = new EditTypeWater();
            SetCenterPositionAndOpen(editTypeWater);
        }

        #endregion

        #region Команды действий

        // Создание клиента
        private RelayCommand _addClient;

        public RelayCommand AddClient
        {
            get 
            {
                return _addClient ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    bool isValid = true;
                    if (Client.LastName == null || Client.LastName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "LastName");
                        isValid = false;
                    }
                    if (Client.FirstName == null || Client.FirstName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "FirstName");
                        isValid = false;
                    }
                    if (Client.Patronymic == null || Client.Patronymic.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "Patronymic");
                        isValid = false;
                    }
                    if (Client.Phone == null || Client.Phone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "Phone");
                        isValid = false;
                    }
                    if (Client.Adress == null || Client.Adress.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "Adress");
                        isValid = false;
                    }
                    if (isValid)
                    {

                        resultStr = Data.AddClient(Client);
                        UpdateView();

                        ShowMessage(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }); 
            }
        }

        //Создание заказа
        private RelayCommand _addOrder;

        public RelayCommand AddOrder
        {
            get {
                return _addOrder ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    int resultStr;
                    bool isValid = true;
                    ///////////////////
                    if (isValid)
                    {

                        resultStr = Data.AddOrder(Order);
                        UpdateView();

                        ShowMessage(resultStr.ToString());
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }); 
            }
        }

        //Создание товара
        private RelayCommand _addWaterType;

        public RelayCommand AddWaterType
        {
            get
            {
                return _addWaterType ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    bool isValid = true;
                    if (WaterType.Name == null || WaterType.Name.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "Name");
                        isValid = false;
                    }
                    /*if (WaterType.Cost == 0 || WaterType.Cost.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "FirstName");
                        isValid = false;
                    }*/
                    if (WaterType.Description == null || WaterType.Description.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "Description");
                        isValid = false;
                    }
                    if (isValid)
                    {

                        resultStr = Data.AddWaterType(WaterType);
                        UpdateView();

                        ShowMessage(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        //Редактирование клиента
        private RelayCommand _editClient;

        public RelayCommand EditClient
        {
            get
            {
                return _editClient ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    bool isValid = true;
                    if (Client.LastName == null || Client.LastName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "LastName");
                        isValid = false;
                    }
                    if (Client.FirstName == null || Client.FirstName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "FirstName");
                        isValid = false;
                    }
                    if (Client.Patronymic == null || Client.Patronymic.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "Patronymic");
                        isValid = false;
                    }
                    if (Client.Phone == null || Client.Phone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "Phone");
                        isValid = false;
                    }
                    if (Client.Adress == null || Client.Adress.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "Adress");
                        isValid = false;
                    }
                    if (isValid)
                    {

                        resultStr = Data.EditClient(Client);
                        UpdateView();

                        ShowMessage(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        //Редактирование заказа
        private RelayCommand _editOrder;

        public RelayCommand EditOrder
        {
            get
            {
                return _editOrder ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    bool isValid = true;
                    ///////////////////
                    if (isValid)
                    {

                        resultStr = Data.EditOrder(Order);
                        UpdateView();

                        ShowMessage(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        //Редактирование товара
        private RelayCommand _editWaterType;

        public RelayCommand EditWaterType
        {
            get
            {
                return _editWaterType ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    bool isValid = true;
                    if (WaterType.Name == null || WaterType.Name.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "Name");
                        isValid = false;
                    }
                    /*if (WaterType.Cost == 0 || WaterType.Cost.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "FirstName");
                        isValid = false;
                    }*/
                    if (WaterType.Description == null || WaterType.Description.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "Description");
                        isValid = false;
                    }
                    if (isValid)
                    {
                        resultStr = Data.EditWaterType(WaterType);
                        UpdateView();

                        ShowMessage(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        #endregion

        private RelayCommand _closeWindow;

        public RelayCommand CloseWindow
        {
            get 
            {
                return _closeWindow ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    wnd.Close();
                }); 
            }
        }

        private void SetNullValuesToProperties()
        {
            Client.ActiveOrdersCount = 0;
            Client.Adress = "";
            Client.ClientId = 0;
            Client.FirstName = "";
            Client.LastName = "";
            Client.OrdersCount = 0;
            Client.Patronymic = "";
            Client.Phone = "";

            Order.ClientId = 0;
            Order.DeliveryTime = DateTime.Now;
            Order.IsDelivered = false;
            Order.OrderId = 0;

            WaterType.Cost = 0;
            WaterType.Description = "";
            WaterType.Name = "";
            WaterType.WaterTypeId = 0;
        }

        private void UpdateView()
        {
            ViewOrders = Data.GetViewOrders();
            MainWindow.OrdersListView.ItemsSource = null;
            MainWindow.OrdersListView.Items.Clear();
            MainWindow.OrdersListView.ItemsSource = ViewOrders;
            MainWindow.OrdersListView.Items.Refresh();

            Clients = Data.GetClients();
            MainWindow.ClientsListView.ItemsSource = null;
            MainWindow.ClientsListView.Items.Clear();
            MainWindow.ClientsListView.ItemsSource = Clients;
            MainWindow.ClientsListView.Items.Refresh();

            WaterTypes = Data.GetWaterTypes();
            MainWindow.WaterTypesListView.ItemsSource = null;
            MainWindow.WaterTypesListView.Items.Clear();
            MainWindow.WaterTypesListView.ItemsSource = WaterTypes;
            MainWindow.WaterTypesListView.Items.Refresh();
        }

        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        private void ShowMessage(string message)
        {
            MessageWindow messageView = new MessageWindow(message);
            SetCenterPositionAndOpen(messageView);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
