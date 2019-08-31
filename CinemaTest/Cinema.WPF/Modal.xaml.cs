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
using Cinema.WFP;
using Newtonsoft.Json;

namespace Cinema.WPF
{
    /// <summary>
    /// Interaction logic for Modal.xaml
    /// </summary>
    public partial class Modal : Window
    {       
        SeansesView seansesView;
        public Modal(SeansesView _seansesView)
        {
            seansesView = _seansesView;
            InitializeComponent();

            Freeplaces.Content = seansesView.FreePlaces;
            Start.Content = seansesView.Start;
            Title.Content = seansesView.Title;
        }

        private void purchase_Click(object sender, RoutedEventArgs e)
        {
            
                var row = textBox.Text;
                var result = int.Parse(row);
                var resp = WebHelper.SetPutData(seansesView.Id, result);
                switch (resp.StatusCode)
                {
                    case 200: MessageBox.Show("Успех"); break;
                    case 400: MessageBox.Show(resp.Response); break;
                    default:
                        {
                            MessageBox.Show("Ошибка"); break;

                        }
                }
            
              

        
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
