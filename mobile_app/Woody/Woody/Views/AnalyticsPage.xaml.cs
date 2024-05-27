using LiveChartsCore.SkiaSharpView.Maui;
using System.Collections.ObjectModel;
using Woody.DataRepos;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */
namespace Woody.Views;

/// <summary>
/// Represents an analytics page for historical data.
/// </summary>
public partial class AnalyticsPage : ContentPage
{
    /// <summary>
    /// Gets or sets the collection of security Cartesian charts displayed on the page.
    /// </summary>
    public ObservableCollection<CartesianChart> ChartsSecurity { get; set; }

    /// <summary>
    /// Gets or sets the collection of plant Cartesian charts displayed on the page.
    /// </summary>
    public ObservableCollection<CartesianChart> ChartsPlant { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AnalyticsPage"/> class.
    /// </summary>
    public AnalyticsPage()
	{
		InitializeComponent();

        ChartsSecurity = new ObservableCollection<CartesianChart>
        {
            ChartsRepo.GetNoiseChart(App.FarmRepo.SecurityRepo.NoiseLevels),
            ChartsRepo.GetLuminosityChart(App.FarmRepo.SecurityRepo.LuminosityLevels)
        };
        


        ChartsPlant = new ObservableCollection<CartesianChart>
        {
            ChartsRepo.GetTemperatureChart(App.FarmRepo.PlantRepo.TemperatureLevels),
            ChartsRepo.GetHumidityChart(App.FarmRepo.PlantRepo.HumidityLevels),
            ChartsRepo.GetSoilMoistureChart(App.FarmRepo.PlantRepo.SoilMoistureLevels)
        };
        ChartCarouselSecurity.BindingContext = this;
        IndicatorViewSecurity.BindingContext = this;

        ChartCarouselPlant.BindingContext = this;
        IndicatorViewPlant.BindingContext = this;
	}
}