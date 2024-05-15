using The49.Maui.BottomSheet;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 05/14/2024
 * 420-6A6 App Dev III
 */

namespace Woody.Views;

/// <summary>
/// Represents a bottom sheet for the map page containing the container's location details.
/// </summary>
public partial class MapBottomSheet : BottomSheet
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MapBottomSheet"/> class.
    /// </summary>
    public MapBottomSheet()
    {
        InitializeComponent();
        BindingContext = App.GeoLocationRepo;
    }
}
