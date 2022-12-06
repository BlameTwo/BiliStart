namespace BiliStart.Contracts.ViewModels;


/// <summary>
/// 导航
/// </summary>
public interface INavigationAware
{
    void OnNavigatedTo(object parameter);

    void OnNavigatedFrom();
}
