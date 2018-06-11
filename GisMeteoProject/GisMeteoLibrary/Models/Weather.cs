namespace GisMeteoLibrary.Models
{   /// <summary>
    /// Строка БД из таблицы weather
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
