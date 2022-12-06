namespace BiliStart.Activation;

// Extend this class to implement new ActivationHandlers. See DefaultActivationHandler for an example.
// https://github.com/microsoft/TemplateStudio/blob/main/docs/WinUI/activation.md
public abstract class ActivationHandler<T> : IActivationHandler
    where T : class
{
    // 是否处理激活逻辑
    protected virtual bool CanHandleInternal(T args) => true;
    
    //重写激活逻辑
    protected abstract Task HandleInternalAsync(T args);
    

    public bool CanHandle(object args) => args is T && CanHandleInternal((args as T)!);

    public async Task HandleAsync(object args) => await HandleInternalAsync((args as T)!);
}
