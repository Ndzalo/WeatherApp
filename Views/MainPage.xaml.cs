using WeatherAppp.Models;
using WeatherAppp.Services;

namespace WeatherAppp
{
    public partial class MainPage : ContentPage
    {
        private readonly WeatherService _weatherService;
        private readonly IGeolocation _geolocation;

        public Command GetLocationCommand { get; }
        public Command<string> SearchCommand { get; }

        public MainPage()
        {
            InitializeComponent();
            _weatherService = new WeatherService();
            _geolocation = Geolocation.Default;

            GetLocationCommand = new Command(async () => await GetCurrentLocation());
            SearchCommand = new Command<string>(async (city) => await SearchCity(city));

            BindingContext = this;
        }

        // getting current location
        private async Task GetCurrentLocation()
        {
            try
            {
                var location = await _geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });

                if (location != null)
                {
                    var weather = await _weatherService.GetWeatherByCoordinates(
                        location.Latitude, location.Longitude);
                    UpdateUI(weather);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        // method to search a city
        private async Task SearchCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return;

            try
            {
                var weather = await _weatherService.GetWeatherByCity(city);
                UpdateUI(weather);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }


        // updating the labels
        private void UpdateUI(WeatherModel.WeatherResponse weather)
        {
            CityLabel.Text = weather.Name;
            TemperatureLabel.Text = $"{weather.Main.Temp:F1}°C";
            WeatherDescriptionLabel.Text = weather.Weather[0].Description;
            HumidityLabel.Text = $"Humidity: {weather.Main.Humidity}%";

            LatitudeLabel.Text = $"Lat: {weather.Coord.Lat:F2}";
            LongitudeLabel.Text = $"Long: {weather.Coord.Lon:F2}";
        }
    }


}
