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
    /// Interaction logic for UpdateParking.xaml
    /// </summary>
    public partial class UpdateParking : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public UpdateParking()
        {
            client.BaseAddress = new Uri("http://loadbalancer-245950494.us-east-1.elb.amazonaws.com");
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

                newParking.LotId = textLotId.Text;
                newParking.LotStreetNumber = int.Parse(textLotStreetNo.Text);
                newParking.LotDailyRate = decimal.Parse(textLotDailyRate.Text);
                newParking.LotHourlyRate = decimal.Parse(textLotHourlyRate.Text);
                newParking.LotWeeklyRate = decimal.Parse(textWeeklyRate.Text);
                newParking.LotMonthlyRate = decimal.Parse(textLotMonthlyRate.Text);
                newParking.LotYearlyRate = decimal.Parse(textLotYearlyRate.Text);

                if (textLotId.Text == "" || textStreetName.Text == "" || textName.Text == "")
                {
                    throw new Exception("Please fill all blanks");
                }

                string updateJson = JsonConvert.SerializeObject(newParking);
                var ParkingToUpdate = new StringContent(updateJson, Encoding.UTF8, "application/json");
                var updateResult = client.PutAsync("/api/parkinglots/" + newParking.LotId.ToString(), ParkingToUpdate).Result;

                if (updateResult.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed || updateResult.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("Please enter correct parking lot ID.");
                }
                MessageBox.Show(updateResult.ToString());
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
