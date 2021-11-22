using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ParkingClient
{
    /// <summary>
    /// Interaction logic for PostParking.xaml
    /// </summary>
    public partial class PostParking : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public PostParking()
        {
            client.BaseAddress = new Uri("https://localhost:44370");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                Parking newParking = new Parking();
                newParking.LotCity = textCity.Text;
                newParking.LotName = textName.Text;
                newParking.LotStreetName = textStreetName.Text;

                newParking.LotId = "50007";
                newParking.LotStreetNumber = 000;
                newParking.LotDailyRate = 49;
                newParking.LotHourlyRate = 4;
                newParking.LotWeeklyRate = 20;
                newParking.LotMonthlyRate = 200;
                newParking.LotYearlyRate = 1900;

                if (textCity.Text == "" || textName.Text == "" || textStreetName.Text == "")
                {
                    throw new Exception("Please fill all blanks");
                }
                string newJsonString = JsonConvert.SerializeObject(newParking);
                var newParkingToPost = new StringContent(newJsonString, Encoding.UTF8, "application/json");
                var postResult = client.PostAsync("/api/parkinglots", newParkingToPost).Result;
               

                MessageBox.Show(postResult.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

