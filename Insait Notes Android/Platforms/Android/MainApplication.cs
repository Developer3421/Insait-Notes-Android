using Android.App;
using Android.Runtime;

namespace Insait_Notes_Android;

/// <summary>
/// Android application object. Bootstraps the MAUI app created by MauiProgram.
/// </summary>
[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}