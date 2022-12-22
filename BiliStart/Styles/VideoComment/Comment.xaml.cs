// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliStart.ItemsViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
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
                RichTextBlock text = new RichTextBlock() { TextWrapping = Microsoft.UI.Xaml.TextWrapping.Wrap };
                Paragraph parag = new Paragraph();
                if (this.Data.Content.Emote.Items.Count == 0)
                {
                    //无表情包评论
                    Run run = new Run() { Text = this.Data.Content.Message };
                    parag.Inlines.Add(run);
                    text.Blocks.Add(parag);
                    return text;
                }
                foreach (var item in this.Data.Content.Emote.Items)
                {
                    string[] first = this.Data.Content.Message.Split(item.Text);
                    foreach (var item2 in first)
                    {
                        parag.Inlines.Add(new Run() { Text = item2 });
                        InlineUIContainer inlineUI = new InlineUIContainer() { Child = new Image() { Source = new BitmapImage(new Uri(item.EmoteUrl)), Height = 20, Width = 20 } };
                        parag.Inlines.Add(inlineUI);
                    }
                }
                text.Blocks.Add(parag);
                return text;
            }
        }
    }


    // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(CommentItemViewModel), typeof(Comment), new PropertyMetadata(null));


}
