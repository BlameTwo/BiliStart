using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Account.Dynamic;
using BiliStart.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.ItemsViewModel;

[ObservableObject]
public partial class DefaultDynamicViewModel: DynamicDataList
{
    public DefaultDynamicViewModel()
    {

    }


    public RichTextBlock RichTextBlockparame => RichTextBlockHelper.DynamicTextParse(this.Modules.Module_More.Desc.Text_Nodes);
}
