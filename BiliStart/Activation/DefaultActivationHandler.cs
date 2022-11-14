using BiliStart.Contracts.Services;
using BiliStart.ViewModels;

using Microsoft.UI.Xaml;

namespace BiliStart.Activation;

public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService _navigationService;
    private readonly IHotNavigationService hotNavigationService;

    public DefaultActivationHandler(INavigationService navigationService,IHotNavigationService hotNavigationService)
    {
        _navigationService = navigationService;
        this.hotNavigationService = hotNavigationService;
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        // None of the ActivationHandlers has handled the activation.
        return _navigationService.Frame?.Content == null;
    }

    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        _navigationService.NavigateTo(typeof(HomeViewModel).FullName!, args.Arguments);

        await Task.CompletedTask;
    }
}
