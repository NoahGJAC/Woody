using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Woody.Views;

public partial class MapPage : ContentPage
{
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