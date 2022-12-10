using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BiliStart.ItemsViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Styles.Dynamics
{
    public class DynamicSelecter:DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplate
        {
            get;set;    
        }

        public DataTemplate DefaultDrawTemplate
        {
            get;set;
        }

        public DataTemplate DefaultSeasonTemplate
        {
            get;set;
        }

        public DataTemplate DefaultAVTemplate
        {
            get;set;
        }

        public DataTemplate DefaultArticleTemplate
        {
            get;set;
        }

        public DataTemplate DefaultForwardTemplate
        {
            get;set;
        }

        public DataTemplate DefaultWordTemplate
        {
            get;set;
        }

        public DataTemplate DefaultPGCTemplate
        {
            get;set;
        }

        public DataTemplate DefaultLiveTemplate
        {
            get;set;
        }

        //选择动态数据模板
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if(item is DefaultDynamicViewModel model)
            {
                switch (model.DynamicType)
                {
                    case "DYNAMIC_TYPE_DRAW":
                        return DefaultDrawTemplate;
                    case "DYNAMIC_TYPE_AV":
                        return DefaultAVTemplate;
                    case "DYNAMIC_TYPE_ARTICLE":
                        return DefaultArticleTemplate;
                    case "DYNAMIC_TYPE_FORWARD":
                        return DefaultForwardTemplate;
                    case "DYNAMIC_TYPE_WORD":
                        return DefaultWordTemplate;
                    case "DYNAMIC_TYPE_UGC_SEASON":
                        return DefaultSeasonTemplate;
                    case "DYNAMIC_TYPE_PGC":
                        return DefaultPGCTemplate;
                    case "DYNAMIC_TYPE_LIVE_RCMD":
                        return DefaultLiveTemplate;
                    default:return DefaultDataTemplate;
                }
            }
            return DefaultDataTemplate;
        }
    }


    public class OrigSelecterDT:ContentControl
    {
        public DefaultDynamicViewModel Data
        {
            get=>(DefaultDynamicViewModel) GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(DefaultDynamicViewModel), typeof(OrigSelecterDT), new PropertyMetadata(null,(s,e)=>Changed(s,e)));

        //这里使用获得的数据进行筛选控件模板，应用，是直接获取后台资源。
        private static void Changed(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            //套娃筛选
            if(e.NewValue is DefaultDynamicViewModel model)
            {
                if(model.Orig != null)
                {
                    switch (model.Orig.DynamicType)
                    {
                        case "DYNAMIC_TYPE_DRAW":
                            (s as OrigSelecterDT)!.DataContext = model.Orig;
                            (s as OrigSelecterDT)!.Template = (ControlTemplate)App.Current.Resources["DrawDynamicCT"];
                            break;
                        case "DYNAMIC_TYPE_AV":
                            (s as OrigSelecterDT)!.DataContext = model.Orig;
                            (s as OrigSelecterDT)!.Template = (ControlTemplate)App.Current.Resources["AVDynamicCT"];
                            break;
                        case "DYNAMIC_TYPE_WORD":
                            (s as OrigSelecterDT)!.DataContext = model.Orig;
                            (s as OrigSelecterDT)!.Template = (ControlTemplate)App.Current.Resources["WordDynamicCT"];
                            break;
                        default:
                            (s as OrigSelecterDT)!.DataContext = "未实现的控件模板";
                            break;
                    }
                }
            }
        }
    }
}
