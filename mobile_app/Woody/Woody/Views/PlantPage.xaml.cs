using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Maui;
using Woody.DataRepos;

namespace Woody.Views;

public partial class PlantPage : ContentPage
{
    public ObservableCollection<CartesianChart> Charts { get; set; }

    public PlantPage()
    {
        InitializeComponent();
        Charts = new ObservableCollection<CartesianChart>
        {
            ChartsRepo.GetTemperatureChart(App.PlantRepo.TemperatureLevels),
            ChartsRepo.GetHumidityChart(App.PlantRepo.HumidityLevels),
            ChartsRepo.GetSoilMoistureChart(App.PlantRepo.SoilMoistureLevels)
        };

        BindingContext = App.PlantRepo;
        ChartCarousel.BindingContext = this;
        IndicatorView.BindingContext = this;
    }

    private void FanSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        App.PlantRepo.FanState.Value = e.Value;
    }

    private void LightSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        App.PlantRepo.LightState.Value = e.Value;
    }
}
