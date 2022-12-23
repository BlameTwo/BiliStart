// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using BiliStart.ItemsViewModel;
using CommunityToolkit.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BiliStart.Styles.VideoComment;
public sealed partial class Comment : UserControl
{
    public Comment()
    {
        this.InitializeComponent();
        Loaded += Comment_Loaded;
    }

    private void Comment_Loaded(object sender, RoutedEventArgs e)
    {
        rich.Content = GetRichTextBlock;
    }

    public CommentItemViewModel ViewModel
    {
        get;set;
    }


    public CommentItemViewModel Data
    {
        get
        {
            return (CommentItemViewModel)GetValue(DataProperty);
        }
        set
        {
            SetValue(DataProperty, value);
        }
    }

    RichTextBlock GetRichTextBlock
    {
        get
        {
            {
                string returntext = this.Data.Content.Message;
                RichTextBlock text = new RichTextBlock() { TextWrapping = Microsoft.UI.Xaml.TextWrapping.Wrap };
                Paragraph parag = new Paragraph();

                #region 处理一些字符串的问题
                returntext = returntext.Replace("&", "&amp;");
                returntext = returntext.Replace("<", "&lt;");
                returntext = returntext.Replace(">", "&gt;");
                returntext = returntext.Replace("\r\n", "<LineBreak/>");
                returntext = returntext.Replace("\n", "<LineBreak/>");
                #endregion

                #region 提取超链接
                Regex linkregex = new Regex(@"(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]");
                MatchCollection links = linkregex.Matches(returntext);
                if (links.Count != 0)
                {
                    foreach (Match linkitem in links)
                    {
                        string link =string.Format("<InlineUIContainer><HyperlinkButton Content=\"{0}\" NavigateUri=\"{1}\" /></InlineUIContainer>", "链接",linkitem.Value);
                        returntext = returntext.Replace(linkitem.Value, link);
                    }
                }
                #endregion

                #region 替换表情
                //替换表情的正则
                string pattern = @"\[(.*?)\]";

                // 使用正则表达式提取文本中的[]包围的字符串列表
                MatchCollection matches = Regex.Matches(this.Data.Content.Message, pattern);
                foreach (Match item in matches)
                {
                    foreach (var item2 in this.Data.Content.Emote.Items)
                    {
                        if(item2.Text == item.Value)
                        {
                            var emoji =  string.Format(@"<InlineUIContainer><Border  Margin=""0 -4 4 -4""><Image Source=""{0}"" Width=""{1}"" Height=""{1}"" /></Border></InlineUIContainer>", item2.EmoteUrl, int.Parse(item2.Meta.Size) == 1 ? "20" : "36");
                            returntext =  returntext.Replace(item2.Text,emoji);
                        }
                    }
                }
                #endregion
                    return CommentFormat(returntext);
            }
        }
    }

    public RichTextBlock CommentFormat(string text)
    {

        var xaml = string.Format(@"<RichTextBlock HorizontalAlignment=""Stretch"" TextWrapping=""Wrap""  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                                            xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
                                            xmlns:mc = ""http://schemas.openxmlformats.org/markup-compatibility/2006"" LineHeight=""20"">
                                          <Paragraph>{0}</Paragraph>
                                      </RichTextBlock>", text);
        var p = (RichTextBlock)XamlReader.Load(xaml);
        return p;
    }

    // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(CommentItemViewModel), typeof(Comment), new PropertyMetadata(null));


}
