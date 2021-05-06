using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterDeliveryApp.Domain;
using WaterDeliveryApp.Models;

namespace WaterDeliveryApp.ViewModelPath
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {

        }
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
