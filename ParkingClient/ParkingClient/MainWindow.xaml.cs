using Newtonsoft.Json;
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

namespace ParkingClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            client.BaseAddress = new Uri("https://localhost:44370");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }

        private void Btn_get_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                HttpResponseMessage responseMessage = client.GetAsync("api/parkinglots").Result;

                Parking[] resultList;
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                resultList = JsonConvert.DeserializeObject<Parking[]>(jsonResponse);
                gridDisplayParking.ItemsSource = resultList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Btn_post_Click(object sender, RoutedEventArgs e)
        {
            PostParking postParking = new PostParking();
            postParking.Show();
        }

        private void Btn_put_Click(object sender, RoutedEventArgs e)
        {
            UpdateParking updateParking = new UpdateParking();
            updateParking.Show();
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (textLotID.Text == "")
                {
                    throw new Exception("Please Enter Parking Lot ID!");
                }
                if (MessageBox.Show("Do you want to delete this parking lot?", "Warning", MessageBoxButton.OKCancel,
               MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    var deleteResult = client.DeleteAsync("api/parkinglots/" + textLotID.Text).Result;
                    if (deleteResult.StatusCode == System.Net.HttpStatusCode.NotFound || deleteResult.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
                    {
                        throw new Exception("Please enter correct parking lot ID!");
                    }

                    MessageBox.Show(deleteResult.ToString());
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Btn_getByID_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (textLotID.Text == "")
                {
                    throw new Exception("Please enter parking lot ID!");
                }
                HttpResponseMessage getByIDResult = client.GetAsync("/api/parkinglots/" + textLotID.Text).Result;
                if (getByIDResult.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("Parking Lot Not Found, Please enter correct ID!");
                }


                Parking gettedCouponByID;
                string jsonResponse = getByIDResult.Content.ReadAsStringAsync().Result;
                gettedCouponByID = JsonConvert.DeserializeObject<Parking>(jsonResponse);

                List<Parking> getParkingResult = new List<Parking>();
                getParkingResult.Add(gettedCouponByID);
                gridDisplayParking.ItemsSource = getParkingResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

    }
}

