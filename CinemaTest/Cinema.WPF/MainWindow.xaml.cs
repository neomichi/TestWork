using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Cinema.WFP;
using Newtonsoft.Json;
using RestSharp;

namespace Cinema.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       

        public MainWindow()
        {
            InitializeComponent();
            var timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 15);
            timer.Start();
            UpdateGrid();

            Binding binding = new Binding();

            binding.ElementName = "myTextBox";
            binding.Path = new PropertyPath("Text");
            CommandBinding bind = new CommandBinding(ApplicationCommands.New);


        }

        private  void timer_Tick(object sender, EventArgs e)
        {
            UpdateGrid();
        }

    

        public async void UpdateGrid()
        {
            var collection = await Task.Run(() =>
            {
                return WebHelper.GetData().Response;
            });
            dataGrid.ItemsSource = JsonConvert.DeserializeObject<IEnumerable<SeansesView>>(collection);
            dataGrid.Items.Refresh();
            
            
            // Button b = new Button();
            




        }
      

        private void  ShowHideDetails(object sender, RoutedEventArgs e)
        {
            var seansesView = (sender as Button).Tag as SeansesView;           
            ShowModal(seansesView);
        }

        private void DataRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            var row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row == null || !(row.Item is SeansesView)) return;          
            ShowModal(row.Item as SeansesView);
        }

        private void ShowModal(SeansesView seansesView)
        {
            var modalBuy = new Modal(seansesView).ShowDialog();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

       

    }
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
