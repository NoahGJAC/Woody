using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using LiveChartsCore.SkiaSharpView.Maui;
using Woody.DataRepos;
using Woody.Enums;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */

namespace Woody.Views;

/// <summary>
/// Represents a security page that displays charts and controls for security settings.
/// </summary>
public partial class SecurityPage : ContentPage
{
    /// <summary>
    /// Gets or sets the collection of Cartesian charts displayed on the page.
    /// </summary>
    public ObservableCollection<CartesianChart> Charts { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityPage"/> class.
    /// </summary>
    public SecurityPage()
    {
        InitializeComponent();
        App.FarmRepo.SecurityRepo.PropertyChanged += SecurityRepo_PropertyChanged;
        App.FarmRepo.SecurityRepo.NoiseLevels.CollectionChanged += Levels_CollectionChanged;
        App.FarmRepo.SecurityRepo.LuminosityLevels.CollectionChanged += Levels_CollectionChanged;
        Charts = new ObservableCollection<CartesianChart>
        {
            ChartsRepo.GetNoiseChart(App.FarmRepo.SecurityRepo.NoiseLevels),
            ChartsRepo.GetLuminosityChart(App.FarmRepo.SecurityRepo.LuminosityLevels)
        };
        SetBindingContext();
    }

    private void SetBindingContext()
    {
        BindingContext = App.FarmRepo.SecurityRepo;

        frame_latest_readings.BindingContext = App.UserRepo.User.UserType;
        frame_graphs.BindingContext = App.UserRepo.User.UserType;
        frame_motion.BindingContext = App.UserRepo.User.UserType;
        grid_buzzer.BindingContext = App.UserRepo.User.UserType;

        ChartCarousel.BindingContext = this;
        IndicatorView.BindingContext = this;

        label_lum_current.BindingContext = App.FarmRepo.SecurityRepo;
        label_noise_current.BindingContext = App.FarmRepo.SecurityRepo;
        label_motion.BindingContext = App.FarmRepo.SecurityRepo;
        BuzzerSwitch.BindingContext = App.FarmRepo.SecurityRepo;
    }

    private async void BuzzerSwitch_Toggled(object sender, ToggledEventArgs e)
    {

        if (e.Value)
        {
            App.FarmRepo.SecurityRepo.BuzzerState.Command.Value = "on";
        }
        else
        {
            App.FarmRepo.SecurityRepo.BuzzerState.Command.Value = "off";
        }
        App.FarmRepo.SecurityRepo.BuzzerState.Value = e.Value;
        await App.IoTDevice.SendCommandAsync(App.FarmRepo.SecurityRepo.BuzzerState.Command);
    }

    private async void ButtonLock_Clicked(object sender, EventArgs e)
    {

        App.FarmRepo.SecurityRepo.LockState.Value = !App.FarmRepo.SecurityRepo.LockState.Value;

        if (App.FarmRepo.SecurityRepo.LockState.Value)
        {
            App.FarmRepo.SecurityRepo.LockState.Command.Value = "1";
        }
        else
        {
            App.FarmRepo.SecurityRepo.LockState.Command.Value = "0";
        }

        await App.IoTDevice.SendCommandAsync(App.FarmRepo.SecurityRepo.LockState.Command);
    }

    private void SecurityRepo_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if ((e.PropertyName == nameof(App.FarmRepo.SecurityRepo.NoiseLevels) ||
            e.PropertyName == nameof(App.FarmRepo.SecurityRepo.LuminosityLevels)))
        {
            UpdateSecurityCharts();
        }
    }

    private void Levels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateSecurityCharts();
    }

    private void UpdateSecurityCharts()
    {
        if (App.FarmRepo.SecurityRepo.LuminosityLevels.Count % 1000 == 0)
        {
            Charts = new ObservableCollection<CartesianChart>
            {
                ChartsRepo.GetNoiseChart(App.FarmRepo.SecurityRepo.NoiseLevels),
                ChartsRepo.GetLuminosityChart(App.FarmRepo.SecurityRepo.LuminosityLevels)
            };
        }

    }
}
