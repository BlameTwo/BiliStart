using BiliStart.Contracts.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Pages.Settings;
public sealed partial class PlayerSetting :UserControl
{
    public PlayerSetting()
    {
        LocalSettingsService = App.GetService<ILocalSettingsService>();
        this.InitializeComponent();
        Loaded += PlayerSetting_Loaded;
    }

    private async void PlayerSetting_Loaded(object sender, RoutedEventArgs e)
    {
        var value = await LocalSettingsService.ReadSettingAsync<int>(BiliStart.Models.Settings.Player_Supper_Supper);
        if (value != null)
        {
            select.SelectedIndex = value;
        }
    }

    public ILocalSettingsService LocalSettingsService
    {
        get;
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        LocalSettingsService.SaveSettingAsync(BiliStart.Models.Settings.Player_Supper_Supper, (sender as ComboBox)!.SelectedIndex);
    }
}
