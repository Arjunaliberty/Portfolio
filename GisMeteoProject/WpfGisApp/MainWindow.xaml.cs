using Newtonsoft.Json;
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
using WpfGisApp.GisService;

namespace WpfGisApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GisService.GisServiceClient client;
        private List<Info> info;
         
        public MainWindow()
        {
            InitializeComponent();
            InitializeClient();
            LoadCity();
        }

        private void InitializeClient()
        {
            client = new GisServiceClient();
        }

        private void LoadCity()
        {
            string jsonData = client.GetAllCity();

            if (jsonData != null)
            {
                info = new List<Info>();
                info = JsonConvert.DeserializeObject<List<Info>>(jsonData);

                foreach (var item in info)
                {
                    DropList.Items.Add(item.City);
                }

                StateInfo.Foreground = Brushes.Green;
                StateInfo.Text = "Загрузка списка городов: Ok!";
            }
            else
            {
                StateInfo.Foreground = Brushes.Red;
                StateInfo.Text = "Ошибка при запросе к сервису";
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string city = DropList.Text;
            Info getCurrentCity = info.Select(i => i).Where(i => i.City.Equals(city)).FirstOrDefault();
            string jsonData = client.GetWeatherCity(getCurrentCity.Id);

            if (jsonData != null)
            {
                ModelDatabase row = new ModelDatabase();
                row = JsonConvert.DeserializeObject<ModelDatabase>(jsonData);

                DataInfo.Content = row.Weather.Date;
                CityInfo.Content = row.Info.City;
                ConditionInfo.Content = row.Weather.WeatherCondition;
                TempMinInfo.Content = row.Weather.TemperatureMin;
                TempMaxInfo.Content = row.Weather.TemperatureMax;
                PrecipitationInfo.Content = row.Weather.Precipitation;

                StateInfo.Foreground = Brushes.Green;
                StateInfo.Text = "Загрузка данных о погоде: Ok!";
            }
            else
            {
                StateInfo.Foreground = Brushes.Red;
                StateInfo.Text = "Ошибка при запросе к сервису";
            }
        }
    }

    /// <summary>
    /// Модель строки из базы данных
    /// </summary>
    public class ModelDatabase
    {
        public Info Info { get; set; }
        public Weather Weather { get; set; }
    }
    /// <summary>
    /// Модель таблицы info
    /// </summary>
    public class Info
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Link { get; set; }
    }
    /// <summary>
    /// Модель таблицы weather
    /// </summary>
    public class Weather
    {
        public int Id { get; set; }
        public string WeatherCondition { get; set; }
        public string Date { get; set; }
        public string TemperatureMin { get; set; }
        public string TemperatureMax { get; set; }
        public string Precipitation { get; set; }
        public int? InfoId { get; set; }
    }
}
