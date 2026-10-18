namespace Insait_Notes_Android;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("tabcenter", typeof(TabCenterPage));
    }
}