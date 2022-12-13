using System.Reflection;
using System.Windows.Input;

using BiliStart.Contracts.Services;
using BiliStart.Helpers;
using BiliStart.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel;
using Windows.Globalization;

namespace BiliStart.ViewModels;

public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private ElementTheme _elementTheme;
    private string _versionDescription;

    public ElementTheme ElementTheme
    {
        get => _elementTheme;
        set => SetProperty(ref _elementTheme, value);
    }

    public string VersionDescription
    {
        get => _versionDescription;
        set => SetProperty(ref _versionDescription, value);
    }


    public ICommand SwitchThemeCommand
    {
        get;
    }

    public SettingsViewModel(IThemeSelectorService themeSelectorService,ITipShow tipShow,ILocalSettingsService localSettingsService)
    {
        _themeSelectorService = themeSelectorService;
        TipShow = tipShow;
        LocalSettingsService = localSettingsService;
        _elementTheme = _themeSelectorService.Theme;
        _versionDescription = GetVersionDescription();
        SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) =>
            {
                if (ElementTheme != param)
                {
                    ElementTheme = param;
                    await _themeSelectorService.SetThemeAsync(param);
                }
            });
        ChangedLanguage = new RelayCommand<ComboBox>((arg) =>
        {
            switch (arg.SelectedIndex)
            {
                case 0:
                    ApplicationLanguages.PrimaryLanguageOverride = "zh-CN";
                    break;
            }
            TipShow.SendMessage("静态资源已经改变，请重启应用", Symbol.Refresh);
        });

       
    }

    [RelayCommand]
    public async Task OnLoaded()
    {
       
    }


    public RelayCommand<ComboBox> ChangedLanguage
    {
        get;set;
    }
    public ITipShow TipShow
    {
        get;
    }
    public ILocalSettingsService LocalSettingsService
    {
        get;
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
