namespace Insait_Notes_Android;

/// <summary>
/// Shell host for the app. Registers the "tabcenter" route so MainPage can
/// open the tab overview via GoToAsync("tabcenter").
/// </summary>
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("tabcenter", typeof(TabCenterPage));
    }
}