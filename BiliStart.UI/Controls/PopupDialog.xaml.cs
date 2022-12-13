// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BiliStart.UI.Controls;
public sealed partial class PopupDialog : UserControl
{
    
    //存放弹出框中的信息
    private string _popupContent;
    private readonly Panel uIElement;

    //创建一个popup对象
    private Popup _popup = null;
    public PopupDialog()
    {
        this.InitializeComponent();

        //将当前的长和框 赋值给控件

        //将当前的控价赋值给弹窗的Child属性  Child属性是弹窗需要显示的内容 当前的this是一个Grid控件。
        _popup = new Popup();
        _popup.Child = this;

        //给当前的grid添加一个loaded事件，当使用了ShowAPopup()的时候，也就是弹窗显示了，这个弹窗的内容就是我们的grid，所以我们需要将动画打开了。
        this.Loaded += PopupNoticeLoaded;
    }

    /// <summary>
    /// 重载
    /// </summary>
    /// <param name="popupContentString">需要弹出的内容</param>
    /// <param name="uIElement">Panel对象</param>
    public PopupDialog(string popupContentString,Panel uIElement,Symbol symbol) : this()
    {
        PopupIcon.Symbol = symbol;
        _popupContent = popupContentString;
        this.uIElement = uIElement;
        _popup.XamlRoot = uIElement.XamlRoot;
    }

    /// <summary>
    /// 显示一个popup弹窗 当需要显示一个弹窗时，执行此方法
    /// </summary>
    public void ShowAPopup()
    {
        _popup.IsOpen = true;
    }

    private void PopupNoticeLoaded(object sender, RoutedEventArgs e)
    {
        PopupContent.Text = _popupContent;
        //打开动画
        this.PopupIn.Begin();
        //当进入动画执行之后，代表着弹窗已经到指定位置了，再指定位置等一秒 就可以消失回去了
        this.PopupIn.Completed += PopupInCompleted;
        this.Width = uIElement.ActualWidth;
        this.Height = uIElement.ActualHeight;
    }


    /// <summary>
    /// 当进入动画完成后 到此
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public async void PopupInCompleted(object sender, object e)
    {
        //在原地续一秒
        await Task.Delay(1000);

        //将消失动画打开
        this.PopupOut.Begin();
        //popout 动画完成后 触发
        this.PopupOut.Completed += PopupOutCompleted;
    }

    //弹窗退出动画结束 代表整个过程结束 将弹窗关闭
    public void PopupOutCompleted(object sender, object e)
    {
        _popup.IsOpen = false;
    }
}
