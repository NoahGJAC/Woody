using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Maui;
using Woody.DataRepos;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */

namespace Woody.Views;

/// <summary>
/// Represents an plant page for plant data.
/// </summary>
public partial class PlantPage : ContentPage
{
    /// <summary>
    /// Gets or sets the collection of Cartesian charts displayed on the page.
    /// </summary>
    public ObservableCollection<CartesianChart> Charts { get; set; }



    /// <summary>
    /// Initializes a new instance of the <see cref="AboutUsPage"/> class.
    /// </summary>
    public PlantPage()
    {
        InitializeComponent();
        Charts = new ObservableCollection<CartesianChart>
        {
            ChartsRepo.GetTemperatureChart(App.FarmRepo.PlantRepo.TemperatureLevels),
            ChartsRepo.GetHumidityChart(App.FarmRepo.PlantRepo.HumidityLevels),
            ChartsRepo.GetSoilMoistureChart(App.FarmRepo.PlantRepo.SoilMoistureLevels)
        };

        BindingContext = App.FarmRepo.PlantRepo;
        ChartCarousel.BindingContext = this;
        IndicatorView.BindingContext = this;
    }

    private async void FanSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            App.FarmRepo.PlantRepo.FanState.Command.Value = "on";
        }
        else
        {
            App.FarmRepo.PlantRepo.FanState.Command.Value = "off";
        }
        App.FarmRepo.PlantRepo.FanState.Value = e.Value;
        await App.IoTDevice.SendCommandAsync(App.FarmRepo.PlantRepo.FanState.Command);
    }

    private async void LightSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            App.FarmRepo.PlantRepo.LightState.Command.Value = "on";
        }
        else
        {
            App.FarmRepo.PlantRepo.LightState.Command.Value = "off";
        }
        App.FarmRepo.PlantRepo.LightState.Value = e.Value;
        await App.IoTDevice.SendCommandAsync(App.FarmRepo.PlantRepo.LightState.Command);
    }
}
