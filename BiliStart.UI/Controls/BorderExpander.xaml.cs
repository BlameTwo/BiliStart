// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace BiliStart.UI.Controls;
public sealed partial class BorderExpander : UserControl
{
    public BorderExpander()
    {
        this.InitializeComponent();
    }




    public object Icon
    {
        get
        {
            return (object)GetValue(IconProperty);
        }
        set
        {
            SetValue(IconProperty, value);
        }
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(object), typeof(BorderExpander), new PropertyMetadata(null));



    public object SubTitle
    {
        get
        {
            return (object)GetValue(SubTitleProperty);
        }
        set
        {
            SetValue(SubTitleProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for SubTitle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SubTitleProperty =
        DependencyProperty.Register("SubTitle", typeof(object), typeof(BorderExpander), new PropertyMetadata(null));



    public string Header
    {
        get
        {
            return (string)GetValue(HeaderProperty);
        }
        set
        {
            SetValue(HeaderProperty, value);
        }
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register("Header", typeof(string), typeof(BorderExpander), new PropertyMetadata(null));




    public object Description
    {
        get
        {
            return (object)GetValue(DescriptionProperty);
        }
        set
        {
            SetValue(DescriptionProperty, value);
        }
    }

    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register("Description", typeof(object), typeof(BorderExpander), new PropertyMetadata(null));



}
