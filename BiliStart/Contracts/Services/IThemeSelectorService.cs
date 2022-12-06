using Microsoft.UI.Xaml;

namespace BiliStart.Contracts.Services;

/// <summary>
/// 设置主题
/// </summary>
public interface IThemeSelectorService
{
    ElementTheme Theme
    {
        get;
    }

    Task InitializeAsync();

    Task SetThemeAsync(ElementTheme theme);

    Task SetRequestedThemeAsync();
}
