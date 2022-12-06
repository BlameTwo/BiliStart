namespace BiliStart.Contracts.Services;


/// <summary>
/// 设置服务
/// </summary>
public interface ILocalSettingsService
{
    Task<T?> ReadSettingAsync<T>(string key);

    Task SaveSettingAsync<T>(string key, T value);
}
