using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Account.Dynamic;
using BiliStart.Contracts.Services;
using BiliStart.ViewModels;
using CommunityToolkit.Common.Parsers.Markdown.Inlines;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.System;

namespace BiliStart.Helpers;
public static class RichTextBlockHelper
{
    public static RichTextBlock DynamicTextParse(List<DescNodes> descNodes,string dynamicid)
    {
        RichTextBlock richTextBlock =new RichTextBlock();
        Paragraph paragraph= new Paragraph();
        foreach (var item in descNodes)
        {
            switch (item.Type)
            {
                case "RICH_TEXT_NODE_TYPE_TEXT":
                    paragraph.Inlines.Add(new Run() { Text = item.Text });
                    break;
                case "RICH_TEXT_NODE_TYPE_AT":
                    var hayperat = new Hyperlink();
                    hayperat.Inlines.Add(new Run() { Text = item.Text });
                    paragraph.Inlines.Add(hayperat);
                    hayperat.Click += (s, e) =>
                    {
                        App.GetService<ITipShow>().SendMessage("此功能在开发中……",Symbol.Emoji);
                    };
                    break;
                case "RICH_TEXT_NODE_TYPE_EMOJI":
                    InlineUIContainer inlineUI = new InlineUIContainer() {Child = new Image() {Source=new BitmapImage(new Uri(item.Emoji.Cover)) ,Height=20,Width= 20} };
                    paragraph.Inlines.Add(inlineUI);
                    break;
                case "RICH_TEXT_NODE_TYPE_TOPIC":
                    var haypertopic = new Hyperlink();
                    haypertopic.Inlines.Add(new Run() { Text = item.Text });
                    paragraph.Inlines.Add(haypertopic);
                    haypertopic.Click += (s, e) =>
                    {
                        App.GetService<ITipShow>().SendMessage("此功能在开发中……", Symbol.Emoji);
                    };
                    break;
                case "RICH_TEXT_NODE_TYPE_LOTTERY":
                    //https://api.vc.bilibili.com/lottery_svr/v1/lottery_svr/lottery_notice?dynamic_id=723153565172891720
                    //互动抽奖的api
                    paragraph.Inlines.Add(new Run() { Text = item.Text, Foreground = new SolidColorBrush(Colors.Green) });
                    break;
                case "RICH_TEXT_NODE_TYPE_WEB":
                    var hayper = new Hyperlink() { NavigateUri = new Uri(item.OrigeText)};
                    hayper.Inlines.Add(new Run() { Text= item.Text });
                    paragraph.Inlines.Add(hayper);
                    break;

            }
        }
        richTextBlock.Blocks.Add(paragraph);
        return richTextBlock;
    }
}
