using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WaterDeliveryApp.ViewModelPath;

namespace WaterDeliveryApp.View
{
    /// <summary>
    /// Логика взаимодействия для WaterTypeList.xaml
    /// </summary>
    public partial class WaterTypeList : Window
    {
        public static ListView WaterTypesListView;
        public WaterTypeList()
        {
            InitializeComponent();
            DataContext = new ViewModel();
            WaterTypesListView = WaterTypeListView;
        }
    }
}
