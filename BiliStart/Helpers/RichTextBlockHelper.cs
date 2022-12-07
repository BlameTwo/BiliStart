using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Account.Dynamic;
using CommunityToolkit.Common.Parsers.Markdown.Inlines;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace BiliStart.Helpers;
public static class RichTextBlockHelper
{
    public static RichTextBlock DynamicTextParse(this List<DescNodes> descNodes)
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
                    paragraph.Inlines.Add(new Run() { Text = item.Text,Foreground = new SolidColorBrush(Colors.LightBlue)});
                    break;
                case "RICH_TEXT_NODE_TYPE_EMOJI":
                    InlineUIContainer inlineUI = new InlineUIContainer() {Child = new Image() {Source=new BitmapImage(new Uri(item.Emoji.Cover)) ,Height=20,Width= 20} };
                    paragraph.Inlines.Add(inlineUI);
                    break;
                case "RICH_TEXT_NODE_TYPE_TOPIC":
                    paragraph.Inlines.Add(new Run() { Text = item.Text,Foreground = new SolidColorBrush(Colors.LightBlue) });
                    break;
                case "RICH_TEXT_NODE_TYPE_LOTTERY":
                    paragraph.Inlines.Add(new Run() { Text = item.Text, Foreground = new SolidColorBrush(Colors.Green) });
                    break;
                case "RICH_TEXT_NODE_TYPE_WEB":
                    paragraph.Inlines.Add(new Hyperlink() {NavigateUri= new Uri(item.OrigeText)});
                    break;

            }
        }
        richTextBlock.Blocks.Add(paragraph);
        return richTextBlock;
    }
}
