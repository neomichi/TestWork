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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Booking_clerk.Core;
using Newtonsoft.Json;

namespace Booking_clerk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
                InitializeComponent();
  
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var content = Code.GetHtml("http://localhost:5000/api/Kinozal");
            dataGrid.ItemsSource = JsonConvert.DeserializeObject<IEnumerable<Film>>(Encoding.UTF8.GetString(content));
           
        }

        private void DataRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null || !(row.Item is Film)) return;
            var film = row.Item as Film;
            var modalPurchase = new ModalPurchase(film);

            modalPurchase.ShowDialog();

  
          
        }
    }
}
