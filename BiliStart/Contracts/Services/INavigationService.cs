using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BiliStart.Contracts.Services;

public interface INavigationService
{
    event NavigatedEventHandler Navigated;

    bool CanGoBack
    {
        get;
    }

    Frame? Frame
    {
        get; set;
    }

   Frame? RootFrame
    {

        get;set;    
    }

    bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false);

    bool GoBack();

    bool RootBack();


    bool RootNavigationTo(string pageKey, object? parameter = null, bool clearNavigation = false);
}


public interface IHotNavigationService:INavigationService
{

}