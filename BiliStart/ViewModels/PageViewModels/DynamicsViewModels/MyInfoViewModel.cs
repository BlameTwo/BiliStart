using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ViewModels.PageViewModels.DynamicsViewModels;

public partial class MyInfoViewModel:ObservableRecipient
{
    public MyInfoViewModel()
    {
        
    }
    private List<string> TestList;

    public List<string> _TestList
    {
        get => TestList;
        set => SetProperty(ref TestList, value);
    }

}
