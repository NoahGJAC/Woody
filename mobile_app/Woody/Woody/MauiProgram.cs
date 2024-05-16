using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Controls.PlatformConfiguration;
using SkiaSharp.Views.Maui.Controls;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Syncfusion.Maui.Core.Hosting;

#if ANDROID
using Android.OS;
#endif

namespace Woody
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureSyncfusionCore()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("OpenSans-Bold.ttf", "OpenSansBold");
                    fonts.AddFont("OpenSans-Medium.ttf", "OpenSansMedium");
                    fonts.AddFont("OpenSans-ExtraBold.ttf", "OpenSansExtraBold");
                    fonts.AddFont("OpenSans-Light.ttf", "OpenSansLight");
                })
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    handlers.AddHandler(typeof(Shell), typeof(CustomShellRenderer));
#endif
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }

    // BUG FIX FOR TOP BAR SWIPING WORKING IMPROPERLY FOR ANDROID
    // SOLUTION FOUND HERE: https://github.com/dotnet/maui/issues/10182#issuecomment-1879833309
#if ANDROID

    public class CustomShellRenderer : ShellRenderer
    {
        protected override IShellSectionRenderer CreateShellSectionRenderer(
            ShellSection shellSection
        )
        {
            return new CustomShellSectionRenderer(this);
        }
    }

    public class CustomShellSectionRenderer : ShellSectionRenderer
    {
        public CustomShellSectionRenderer(IShellContext shellContext)
            : base(shellContext) { }

        public override Android.Views.View OnCreateView(
            Android.Views.LayoutInflater inflater,
            Android.Views.ViewGroup container,
            Bundle savedInstanceState
        )
        {
            var result = base.OnCreateView(inflater, container, savedInstanceState);
            SetViewPager2UserInputEnabled(false);
            return result;
        }

        protected override void SetViewPager2UserInputEnabled(bool value)
        {
            base.SetViewPager2UserInputEnabled(false);
        }
    }
#endif
}
