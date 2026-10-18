using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Insait_Notes_Android;

/// <summary>
/// Main (and only) Android activity. SingleTop launch mode prevents duplicate
/// instances, and ConfigurationChanges lets the UI handle rotation itself.
/// </summary>
[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
}