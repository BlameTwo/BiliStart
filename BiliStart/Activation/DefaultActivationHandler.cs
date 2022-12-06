using BiliStart.Contracts.Services;
using BiliStart.ViewModels;

using Microsoft.UI.Xaml;

namespace BiliStart.Activation;

public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly IAppNavigationService _navigationService;

    public DefaultActivationHandler(IAppNavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        // 激活位置
        return _navigationService.ShellFrame?.Content == null;
    }

    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        _navigationService.NavigationTo(AppNavigationViewsEnum.ShellFrame,typeof(HomeViewModel).FullName!, args.Arguments);

        await Task.CompletedTask;
    }
}
