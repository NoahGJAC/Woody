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
