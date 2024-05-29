using LiveChartsCore.SkiaSharpView.Maui;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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

        App.FarmRepo.PlantRepo.PropertyChanged += PlantRepo_PropertyChanged;
        App.FarmRepo.PlantRepo.TemperatureLevels.CollectionChanged += Levels_CollectionChanged;
        App.FarmRepo.PlantRepo.HumidityLevels.CollectionChanged += Levels_CollectionChanged;
        App.FarmRepo.PlantRepo.SoilMoistureLevels.CollectionChanged += Levels_CollectionChanged;
        App.FarmRepo.SecurityRepo.PropertyChanged += Security_PropertyChanged;
        App.FarmRepo.SecurityRepo.NoiseLevels.CollectionChanged += Levels_CollectionChanged;
        App.FarmRepo.SecurityRepo.LuminosityLevels.CollectionChanged += Levels_CollectionChanged;



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

    private void UpdatePlantCharts()
    {
        if (App.FarmRepo.PlantRepo.TemperatureLevels.Count % 100 == 0)
        {
            ChartsPlant = new ObservableCollection<CartesianChart>
            {
                ChartsRepo.GetTemperatureChart(App.FarmRepo.PlantRepo.TemperatureLevels),
                ChartsRepo.GetHumidityChart(App.FarmRepo.PlantRepo.HumidityLevels),
                ChartsRepo.GetSoilMoistureChart(App.FarmRepo.PlantRepo.SoilMoistureLevels)
            };
        }


    }
    private void UpdateSecuritytCharts()
    {
        if (App.FarmRepo.SecurityRepo.NoiseLevels.Count % 100 == 0)
        {
            ChartsSecurity = new ObservableCollection<CartesianChart>
            {
                ChartsRepo.GetNoiseChart(App.FarmRepo.SecurityRepo.NoiseLevels),
                ChartsRepo.GetLuminosityChart(App.FarmRepo.SecurityRepo.LuminosityLevels)
            };
        }

    }
    private void PlantRepo_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "TemperatureLevels" ||
            e.PropertyName == "HumidityLevels" ||
            e.PropertyName == "SoilMoistureLevels")
        {
            UpdatePlantCharts();
        }
    }

    private void Security_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "NoiseLevels" ||
            e.PropertyName == "LuminosityLevels")
        {
            UpdateSecuritytCharts();
        }
    }

    private void Levels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        UpdatePlantCharts();
        UpdateSecuritytCharts();
    }
}