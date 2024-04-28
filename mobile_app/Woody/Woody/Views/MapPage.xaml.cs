using Microsoft.Maui.Maps;

namespace Woody.Views;

public partial class MapPage : ContentPage
{
	public MapPage()
	{
		InitializeComponent();

        BindingContext = App.GeoLocationRepo;
    }
}