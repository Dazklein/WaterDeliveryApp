using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WaterDeliveryApp.Domain;
using WaterDeliveryApp.Models;

namespace WaterDeliveryApp.ViewModelPath
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {

        }

        public static Clients Client { get; set; }
        public static Orders Orders { get; set; }
        public static WaterTypes WaterTypes { get; set; }
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

        public static Orders SelectedOrder { get; set; }
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
        
        // Редактирование
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

        #endregion

        #region Команды действий

        private RelayCommand _addClient;

        public RelayCommand AddClient
        {
            get 
            {
                return _addClient ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    Clients client = Client;
                    string resultStr = "";
                    bool isValid = true;
                    if (Client.LastName == null || Client.LastName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                        isValid = false;
                    }
                    if (Client.Phone == null || Client.Phone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "PriceBlock");
                        isValid = false;
                    }
                    if (Client.Adress == null || Client.Adress.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "PriceBlock");
                        isValid = false;
                    }
                    if (isValid)
                    {

                        resultStr = Data.AddClient(Client);
                        UpdateNomenclatureView();

                        ShowMessage(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }); 
            }
        }


        #endregion

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
            
        }

        private void UpdateNomenclatureView()
        {
            AllNomenclature = DataWorker.Sel_nomenclature();
            MainWindow.AllNomenclatureView.ItemsSource = null;
            MainWindow.AllNomenclatureView.Items.Clear();
            MainWindow.AllNomenclatureView.ItemsSource = AllNomenclature;
            MainWindow.AllNomenclatureView.Items.Refresh();
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
