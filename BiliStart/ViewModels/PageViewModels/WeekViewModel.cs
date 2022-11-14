using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ViewModels.PageViewModels;
public class WeekViewModel:ObservableRecipient
{
    public WeekViewModel()
    {
        IsActive= true;
    }
}
