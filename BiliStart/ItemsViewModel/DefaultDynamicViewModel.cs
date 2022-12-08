using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Account.Dynamic;
using BiliStart.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;

namespace BiliStart.ItemsViewModel;

[ObservableObject]
public partial class DefaultDynamicViewModel: DynamicDataList
{
    public DefaultDynamicViewModel()
    {

    }


    public RichTextBlock RichTextBlockparame
    {
        get
        {
            switch (this.DynamicType)
            {
                case "DYNAMIC_TYPE_DRAW":
                    return  RichTextBlockHelper.DynamicTextParse(this.Modules.Module_More.Desc.Text_Nodes, this.ID);
                case "DYNAMIC_TYPE_AV":
                    if(this.Modules.Module_More.Desc == null)       //如果没有简介内容,先给一个默认简介
                    {
                        RichTextBlock rich = new();
                        Paragraph par = new();
                        par.Inlines.Add(new Run()
                        {
                            Text = this.Modules.Module_More.Module_Major.Major_Acrchive.Desc
                        });
                        rich.Blocks.Add(par);
                        return rich;
                    }
                    else
                    {
                        return RichTextBlockHelper.DynamicTextParse(this.Modules.Module_More.Desc.Text_Nodes, this.ID);
                    }
                case "DYNAMIC_TYPE_WORD":
                    return RichTextBlockHelper.DynamicTextParse(this.Modules.Module_More.Desc.Text_Nodes, this.ID);
                default:
                    return new RichTextBlock();
            }
        }
    }
}
