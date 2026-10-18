using Insait_Notes_Android.Services;

namespace Insait_Notes_Android;

/// <summary>
/// Main editor screen: shows the currently selected tab and its note text.
/// Text edits are debounced before being persisted so typing stays smooth.
/// </summary>
public partial class MainPage : ContentPage
{
    private const string AppVersion = "1.0.1";

    // Key under which the selected UI language is stored in Preferences.
    private const string LangPrefKey = "insait_lang";

    private readonly NotesService _service = NotesService.Instance;

    // Debounce timer: saves the note shortly after the user stops typing.
    private readonly IDispatcherTimer _saveTimer;
    private string _lang = "EN";

    // Guards against reacting to programmatic text changes in SyncEditor.
    private bool _loading;
    private bool _subscribed;

    public MainPage()
    {
        InitializeComponent();

        // Restore the saved language, falling back to English when unknown.
        _lang = Preferences.Get(LangPrefKey, "EN");
        if (Array.IndexOf(Localization.Codes, _lang) < 0)
            _lang = "EN";

        _saveTimer = Dispatcher.CreateTimer();
        _saveTimer.Interval = TimeSpan.FromMilliseconds(700);
        _saveTimer.Tick += async (_, _) =>
        {
            _saveTimer.Stop();
            if (_service.Current is { } cur)
                await _service.SaveAsync(cur);
        };

        ApplyLanguage();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _service.InitAsync();

        // Subscribe to service changes only once for the lifetime of the page.
        if (!_subscribed)
        {
            _service.Changed += OnServiceChanged;
            _subscribed = true;
        }
        if (_service.Current is null && _service.Tabs.Count > 0)
            _service.SetCurrent(_service.Tabs[0]);
        SyncEditor();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        NoteEditor.Unfocus();
        _ = FlushAsync();
    }

    private void OnServiceChanged(object? sender, EventArgs e) => SyncEditor();

    /// <summary>Pushes the current tab's name/content into the editor controls.</summary>
    private void SyncEditor()
    {
        var tab = _service.Current;
        _loading = true;
        NoteEditor.Text = tab?.Content ?? "";
        NoteTitleLabel.Text = tab?.Name ?? "";
        _loading = false;
    }

    private void OnEditorTextChanged(object? sender, TextChangedEventArgs e)
    {
        // Ignore changes triggered by SyncEditor, not by the user.
        if (_loading || _service.Current is null)
            return;
        _service.Current.Content = e.NewTextValue ?? "";
        _saveTimer.Stop();
        _saveTimer.Start();
    }

    /// <summary>Immediately persists the current tab, bypassing the debounce timer.</summary>
    private async Task FlushAsync()
    {
        _saveTimer.Stop();
        if (_service.Current is { } cur)
            await _service.SaveAsync(cur);
    }

    private async void OnTabsCenterTapped(object? sender, TappedEventArgs e)
    {
        await FlushAsync();
        await Shell.Current.GoToAsync("tabcenter");
    }

    /// <summary>Cycles to the next UI language and retranslates the welcome text if untouched.</summary>
    private void OnLangTapped(object? sender, TappedEventArgs e)
    {
        var idx = Array.IndexOf(Localization.Codes, _lang);
        _lang = Localization.Codes[(idx + 1) % Localization.Codes.Length];
        Preferences.Set(LangPrefKey, _lang);

        if (_service.Current is { } cur && Localization.IsWelcome(_lang, NoteEditor.Text ?? ""))
        {
            NoteEditor.Text = Localization.T(_lang, "welcomeBody");
            cur.Content = NoteEditor.Text;
            _ = _service.SaveAsync(cur);
        }
        ApplyLanguage();
    }

    private void ApplyLanguage()
    {
        LangLabel.Text = _lang;
        StatusLabel.Text = $"Insait Notes  v{AppVersion}";
    }

    /// <summary>Hardware back closes an open overlay first, otherwise default behavior.</summary>
    protected override bool OnBackButtonPressed()
    {
        if (AboutOverlay.IsVisible)
        {
            AboutOverlay.IsVisible = false;
            return true;
        }
        if (LicenseOverlay.IsVisible)
        {
            LicenseOverlay.IsVisible = false;
            return true;
        }
        return base.OnBackButtonPressed();
    }

    private void OnEditorTapped(object? sender, TappedEventArgs e)
        => NoteEditor.Focus();

    private async void OnInfoTapped(object? sender, TappedEventArgs e)
    {
        NoteEditor.Unfocus();
        AboutTitleLabel.Text = Localization.T(_lang, "aboutTitle");
        AboutHeadingLabel.Text = Localization.T(_lang, "aboutTitle");
        AboutDevLabel.Text = Localization.T(_lang, "aboutDev");
        AboutDateLabel.Text = Localization.T(_lang, "aboutDate");
        AboutLangLabel.Text = Localization.T(_lang, "aboutLang");
        AboutVersionLabel.Text = Localization.T(_lang, "aboutVersion") + AppVersion;
        AboutOverlay.IsVisible = true;
    }

    private void OnCloseAbout(object? sender, TappedEventArgs e)
        => AboutOverlay.IsVisible = false;

    private void OnLicenseTapped(object? sender, TappedEventArgs e)
    {
        NoteEditor.Unfocus();
        LicenseTitleLabel.Text = Localization.T(_lang, "licTitle");
        LicHeadingLabel.Text = Localization.T(_lang, "licTitle");
        LicSubtitleLabel.Text = Localization.T(_lang, "licSubtitle");
        LicP1Label.Text = Localization.T(_lang, "licP1");
        LicP2Label.Text = Localization.T(_lang, "licP2");
        LicP3Label.Text = Localization.T(_lang, "licP3");
        LicP5Label.Text = Localization.T(_lang, "licP5");
        LicenseOverlay.IsVisible = true;
    }

    private void OnCloseLicense(object? sender, TappedEventArgs e)
        => LicenseOverlay.IsVisible = false;
}
