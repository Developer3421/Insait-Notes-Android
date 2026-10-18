using Microsoft.Extensions.DependencyInjection;

namespace Insait_Notes_Android;

/// <summary>
/// Application entry point. Creates the single shell window that hosts the
/// editor page (MainPage) and the tab center page (TabCenterPage).
/// </summary>
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}