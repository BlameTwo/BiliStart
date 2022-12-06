using BiliStart.Contracts.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Pages.Settings;
public sealed partial class PlayerAutoStart : UserControl
{
    ILocalSettingsService LocalSettingsService
    {
        get;
    }
    public PlayerAutoStart()
    {
        this.LocalSettingsService = App.GetService<ILocalSettingsService>();    
        this.InitializeComponent();
        Loaded += PlayerAutoStart_Loaded;
    }

    private async void PlayerAutoStart_Loaded(object sender, RoutedEventArgs e)
    {
        selection.SelectedIndex =await LocalSettingsService.ReadSettingAsync<int>(BiliStart.Models.Settings.Player_AutoStart);
    }

    private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await LocalSettingsService.SaveSettingAsync(BiliStart.Models.Settings.Player_AutoStart, (sender as ComboBox)!.SelectedIndex);
    }
}
