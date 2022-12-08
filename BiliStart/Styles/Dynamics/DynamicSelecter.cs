using System;
using System.Collections.Generic;
using System.Linq;
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
                    default:return DefaultDataTemplate;
                }
            }
            return DefaultDataTemplate;
        }
    }
}
