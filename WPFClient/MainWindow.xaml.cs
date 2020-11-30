using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using WPFClient.Helpers;
using WPFClient.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace WPFClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ViewModel ChangeCard { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ApplicationViewModel();
            this.new_ViewModel.ItemsSource = new ObservableCollection<ViewModel>();
            this.ChangeCard = new ViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello");
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ChangeCard = e.AddedItems[0] as ViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!(sender as TextBox).Text.Contains("П"))
                    Get((sender as TextBox).Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task Get(string sender)
       {
            try
            {
               
                string operationsjson = JsonConvert.SerializeObject(new QueryParamDTO() { BonusCardNumber = $"{sender}",UserPhoneNumber=$"{sender}"});
                var paramOperations = new SendParams(String.Concat(UrlHelper.Domain, UrlHelper.GetCards),
                    operationsjson, "POST");
               

                var sendOperations = new Helpers.RestClient(paramOperations);


                await sendOperations.Post();


                var ResponseOperations = (ObservableCollection<ViewModel>)JsonConvert.DeserializeObject(sendOperations.Response, typeof(ObservableCollection<ViewModel>));
                this.new_ViewModel.ItemsSource = ResponseOperations;

            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
