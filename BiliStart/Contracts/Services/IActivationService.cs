namespace BiliStart.Contracts.Services;

public interface IActivationService
{
    //异步激活
    Task ActivateAsync(object activationArgs);
}
