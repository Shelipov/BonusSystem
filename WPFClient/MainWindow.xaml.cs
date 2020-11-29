using System;
using System.Collections.Generic;
using System.Linq;
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
using WPFClient.Models;

namespace WPFClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello");
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
            Get(sender);
        }

        public async Task Get(object sender)
        {
            try
            {
                var client = new RestClient("http://localhost:5000/bonus-system/get-cards");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cookie", "FedAuth=77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48U2VjdXJpdHlDb250ZXh0VG9rZW4gcDE6SWQ9Il80YzgyZmYxNS02NDFkLTQ5MjUtODg2OC05OGU3MTgzNzY0MjMtQzcxMkQ3QjRFMjVDRTIxNEUzN0I4NEJCOEYwNDZFNjQiIHhtbG5zOnAxPSJodHRwOi8vZG9jcy5vYXNpcy1vcGVuLm9yZy93c3MvMjAwNC8wMS9vYXNpcy0yMDA0MDEtd3NzLXdzc2VjdXJpdHktdXRpbGl0eS0xLjAueHNkIiB4bWxucz0iaHR0cDovL2RvY3Mub2FzaXMtb3Blbi5vcmcvd3Mtc3gvd3Mtc2VjdXJlY29udmVyc2F0aW9uLzIwMDUxMiI+PElkZW50aWZpZXI+dXJuOnV1aWQ6NjE5ZWMxOWUtZWMzMi00MjBmLWEzMDQtYmI4ODI5ZThhY2Y3PC9JZGVudGlmaWVyPjxDb29raWUgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwNi8wNS9zZWN1cml0eSI+VzJtemF3b3dVWnJwSnU4TXdRdlVSTUNUTHd5QUN1dVhlQWtNQ1VLYk1lRWx3NlNNcnJ0WlJxbHFFa0FVaUkyZ0ZqYTZIMStHWmxpQWFIaENMbnNqcnBEL1hXcEpmYnRDbU80YWxsbTdMY1JSd1RHL3pXTU95OHFHN3hrNE5YQlQ0MXl1cUdDdE5TaTZEcGZ6dHRkNjFaZjE1VUlXcnptTVN6RDA0a1g5ZnpNZUhGTXRnNTlTeCtiOEQ2aFlxbzNkTXBwTmFLc2Z6d292SWJkYmZHaUpDVlA5dWQzVnJvY0NQRFRxZnY3ZUtGRGRhNWZlaldKSFhzZ3oySGpYdi9LSkhKSjczdm1CVFRxc2RsWHg2OWNWY2IxQjl1TzFpMy81dnIwL09WckNzNHFMMGlmeWxlOSs5TUhjV09rb09zZUdmN2Zyc1g4ZkVIcVhkTzVpcTlEK2NYZVNNNlVFZjJWWmMyNnJpQm5iTUovNHNIaGFPakhlb0tiUWc2aFpZMHV3TWEvT3Nvc3EvcGk0cG04ZEpQb28wUXZ1amI4ZW5GcEErUk9FdkZIZFI5eTRkcjI3NjZGNlQ0ell0OXh4bjNSaVFaakJ0bTJHUXovdWdSZmo0dFRhdFU0cktESzRIeFg3QXhKdGFsbUZYM1picTByVXdTc296eHRHeDlyZ01kR1FMMllkQm1WZjlZUkRGUThzSlFDOUZPS0U5V1g3MjNGMnpsMGs0dkxpd3ZydCt5b1BBbWsyeDhZR1dTaTJYQkRTK0REUXBEcEdaSzdGUG5QKzl2VzVPcnVlNWRnQ2FmeEx1Y3htTGUxdWl6WWx3RUV3YWNKbW9iYlRhVHhaU0F3SkFKaGFhUHgvQ2tra0wyNUpjdTNVQ3IwcnhZVzNOWEpia3FBZ0tZYzB4MnBFMDVnQmdxRUFCaktmVlBkT2NQOHhQU0l2N3JBS2lYSk1OdTFrL0VFcVZOUFg4Nk41a2RrTWxxWUwvMmlSS2EzMjMzQk5EcDRrTHNmenQweWtFRzFWQm1lTHZEUFArMGZIUk03VEgzWWt6bnZlY1VIMFIzZUkxMWk2alJXZGgxbUxwQkxLT3ZqQ0hCWVpxQnVLTjBEOUFKSjZqK09US0EvQ2FQSmpOWWVXYk11b0s2dHNUa2Y4ZEx3dWhqSGYvTm9VNEoxR0ttWnU5MDVlcE5qemp3OTdKMHhNVjZjSzlnWXk4bmF2YVl0SDJqYVNLWmxvOEFNbXhrT1lzanloZzlPbDd1RmpUSTZKRkd0Yi9UMzZha0pWZHpJZzVtZUhLMGR5ZFdHdEYyS2FrSHdFZlUwSlpBbHdQR0JBMWtqZ3hrc0ZwNlZaenJ0bUhTLzduSkNMWUppdVROWTNQdDJ5UlVQZHVRb3ZjT2FJSHFmYU9oSndxQVlrSE02; FedAuth1=bjBOUjg0ZDRuRy9SbWs2VW05L1hmL1lkYWlCQTVNZkMwbnJhRi9QMk5acCtjem0rRDNIbmEwVlFUelVtQ2k1amQ0WUFrYlFMMSt5YkJxWm1kUURmMzY5ZkpKc2w1YThRWFhoSzc2MmdOeDhJUXk2VGo4ektpb0J2amVoZnFTVGpnTDhUT1pxalZ1d0xnM3dicmdDVHZSWEp3ZElSYTFaWTU2VngrZHN1MUtlbmtocmZ5cFRDTzRlWT08L0Nvb2tpZT48L1NlY3VyaXR5Q29udGV4dFRva2VuPg==");
                request.AddParameter("undefined", "{\n\t\"UserPhoneNumber\":\"350-36-06\",\n\t\"BonusCardNumber\" :\"664\"\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
            }
            catch(Exception ex)
            {
                var tt = ex.Message;
            }
        }
    }
}
