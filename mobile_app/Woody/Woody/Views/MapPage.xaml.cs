using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */

namespace Woody.Views;

/// <summary>
/// Represents a map page for geolocation.
/// </summary>
public partial class MapPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MapPage"/> class.
    /// </summary>
    public MapPage()
	{
		InitializeComponent();

        BindingContext = App.GeoLocationRepo;
        ConfigureMap();
        
    }
    private void ConfigureMap()
    {
        
        var location = new Location(App.GeoLocationRepo.GPS.Value.Latitude.Value, App.GeoLocationRepo.GPS.Value.Longitude.Value);
        var pin = new Pin
        {
            Label = "container",
            Type = PinType.SavedPin,
            Location = location
        };
        map.MoveToRegion(new MapSpan(location,0.01, 0.01));
        map.Pins.Add(pin);
    }
}