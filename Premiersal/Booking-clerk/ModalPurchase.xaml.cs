using Booking_clerk.Core;
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
using Newtonsoft.Json;

namespace Booking_clerk
{
    /// <summary>
    /// Interaction logic for ModalPurchase.xaml
    /// </summary>
    public partial class ModalPurchase : Window
    {
        Film film;
        public ModalPurchase(Film _film)
        {
            film = _film;
         
            InitializeComponent(); filmTitle.Content = film.Title;
        }

        private void purchase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var num = int.Parse(textBox.Text.Trim());
                var obj = JsonConvert.SerializeObject(new Purchase { Film = film.Id, Id = 0, Tikets = num });

                var rep = Code.PostRequest("http://localhost:5000/api/Kinozal", obj);
                MessageBox.Show(rep);
                exit_Click(sender,e);
            }
            catch 
            {
                MessageBox.Show("ошибка");
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
