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
            Set();
            //MessageBox.Show("Отправленно в обработку");
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                {
                    ChangeCard = e.AddedItems[0] as ViewModel;
                    textBlock.Text = $"Карта:{ChangeCard.BonusCardNumber}; Баланс:{ChangeCard.BonusCardBalanse}";
                }
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

       async Task Get(string sender)
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
                Summ.State = sender;
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message);
            }
        }
        async Task Set()
        {
            try
            {
                if (ChangeCard.BonusCardID != 0)
                {
                    if (ChangeCard.RemuveSumm > 0)
                    {
                        string operationsjson = JsonConvert.SerializeObject(new QueryParamDTO() { BonusCardNumber = $"{ChangeCard.BonusCardNumber}", UserPhoneNumber = $"{ChangeCard.UserPhoneNumber}", MoneyFromBonusCard = ChangeCard.RemuveSumm });
                        var paramOperations = new SendParams(String.Concat(UrlHelper.Domain, UrlHelper.MoneyFromBonusCard),
                            operationsjson, "POST");
                        var sendOperations = new Helpers.RestClient(paramOperations);
                        await sendOperations.Post();
                        var ResponseOperations = (string)JsonConvert.DeserializeObject(sendOperations.Response, typeof(string));
                        textBlock.Text = $"Карта:{ChangeCard.BonusCardNumber}; Баланс:{ChangeCard.BonusCardBalanse -= (decimal)ChangeCard.RemuveSumm}";
                        ///MessageBox.Show(ResponseOperations);
                    }
                    if (ChangeCard.AddSumm > 0)
                    {
                        string operationsjson = JsonConvert.SerializeObject(new QueryParamDTO() { BonusCardNumber = $"{ChangeCard.BonusCardNumber}", UserPhoneNumber = $"{ChangeCard.UserPhoneNumber}", MoneyFromBonusCard = ChangeCard.AddSumm });
                        var paramOperations = new SendParams(String.Concat(UrlHelper.Domain, UrlHelper.MoneyToBonusCard),
                            operationsjson, "POST");
                        var sendOperations = new Helpers.RestClient(paramOperations);
                        await sendOperations.Post();
                        var ResponseOperations = (string)JsonConvert.DeserializeObject(sendOperations.Response, typeof(string));
                        textBlock.Text = $"Карта:{ChangeCard.BonusCardNumber}; Баланс:{ChangeCard.BonusCardBalanse += (decimal)ChangeCard.AddSumm}";
                        //MessageBox.Show(ResponseOperations);
                    }
                    await Get(Summ.State);
                }
                else
                {
                    MessageBox.Show("Карта не выбрана");
                }
            }
            catch
            {
                MessageBox.Show("Не удалось обработать запрос обратитесь за помощью в службу поддержки");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!(sender as TextBox).Text.Contains("П"))
                {
                    Summ.AddSumm =decimal.Parse((sender as TextBox).Text);
                    if(ChangeCard.BonusCardID != 0)
                    {
                        ChangeCard.AddSumm = Summ.AddSumm>=0? Summ.AddSumm: Summ.AddSumm*-1;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не валидное число");
            }
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!(sender as TextBox).Text.Contains("С"))
                {
                    Summ.RemuveSumm = decimal.Parse((sender as TextBox).Text);
                    if (ChangeCard.BonusCardID != 0)
                    {
                        ChangeCard.RemuveSumm = Summ.RemuveSumm>=0? Summ.RemuveSumm: Summ.RemuveSumm*-1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не валидное число");
            }
        }
    }
}
