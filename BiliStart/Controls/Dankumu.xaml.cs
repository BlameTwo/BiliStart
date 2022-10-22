using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace BiliStart.Controls
{
    /// <summary>
    /// Dankumu.xaml 的交互逻辑
    /// </summary>
    public partial class Dankumu : UserControl
    {
        public Dankumu()
        {
            InitializeComponent();
        }

        public List<Storyboard> ScrollBoards { get; set; } = new List<Storyboard>();

        public List<Storyboard> TopBoards { get; set; } = new List<Storyboard>();

        public List<Storyboard> BottomBoards { get; set; } = new List<Storyboard>();

        public void PauseScroll()
        {
            foreach (var item in ScrollBoards.ToArray())
            {
                item.Pause();
            }
        }

        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            ScrollBoards.Clear();
            scroll.Children.Clear();
        }

        public void StartScroll()
        {
            foreach (var item in ScrollBoards.ToArray())
            {
                item.Resume();
            }
        }
        TopManager topManager;

        public void CreateScrollText(string Text,DankumuTextStyle style)
        {
            topManager = new TopManager(scroll.ActualHeight,style.Size);
            int slot = topManager.getIdleSlot();
            if (slot > scroll.ActualHeight) slot = (int)style.Size;
            Storyboard storyboard = new Storyboard();
            DankumuText text = CreateText(style,Text);
            Canvas.SetTop(text, slot * style.Size);
            Canvas.SetLeft(text, scroll.ActualWidth - text.ActualWidth);
            DoubleAnimation doubleAnimation = new DoubleAnimation() { Duration = new Duration(TimeSpan.FromSeconds(8))};
            doubleAnimation.From = scroll.ActualWidth - text.ActualWidth;
            doubleAnimation.To =  -scroll.ActualWidth;
            Storyboard.SetTarget(doubleAnimation,text);
            Storyboard.SetTargetProperty(doubleAnimation,new PropertyPath(Canvas.LeftProperty));
            scroll.Children.Add(text);
            storyboard.Children.Add(doubleAnimation);
            ScrollBoards.Add(storyboard);
            storyboard.Completed += (s, e) =>
            {
                scroll.Children.Remove(text);
                ScrollBoards.Remove(storyboard);
                text = null;
                storyboard = null;
            };
            storyboard.Begin();
        }

        public void CreateTopText(string Text, DankumuTextStyle style)
        {
            Storyboard storyboard = new Storyboard();

            DankumuText text = CreateText(style, Text);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Top;
            
            RowDefinition row = new RowDefinition();
            top.RowDefinitions.Add(row);
            int toprow = top.RowDefinitions.Count == 0 ? 0 : top.RowDefinitions.Count - 1;
            Grid.SetRow(text,toprow);
            DoubleAnimation doubleAnimation = new DoubleAnimation() { Duration = new Duration(TimeSpan.FromSeconds(4)) };
            doubleAnimation.From = 1;
            doubleAnimation.To = 1;
            Storyboard.SetTarget(doubleAnimation, text);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(FrameworkElement.OpacityProperty));
            top.Children.Add(text);
            storyboard.Children.Add(doubleAnimation);
            TopBoards.Add(storyboard);
            storyboard.Completed += (s, e) =>
            {
                top.Children.Remove(text);
                TopBoards.Remove(storyboard);
                text = null;
                storyboard = null;
                top.RowDefinitions.Remove(row);
            };
            storyboard.Begin();
        }

        public void CreateBootomText(string Text, DankumuTextStyle style)
        {
            Storyboard storyboard = new Storyboard();
            DankumuText text = CreateText(style, Text);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Bottom;
            RowDefinition row = new RowDefinition();
            bootom.RowDefinitions.Add(row);
            int toprow = bootom.RowDefinitions.Count == 0 ? 0 : bootom.RowDefinitions.Count - 1;
            Grid.SetRow(text, toprow);
            DoubleAnimation doubleAnimation = new DoubleAnimation() { Duration = new Duration(TimeSpan.FromSeconds(4)) };
            doubleAnimation.From = 1;
            doubleAnimation.To = 1;
            Storyboard.SetTarget(doubleAnimation, text);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(FrameworkElement.OpacityProperty));
            bootom.Children.Add(text);
            storyboard.Children.Add(doubleAnimation);
            BottomBoards.Add(storyboard);
            storyboard.Completed += (s, e) =>
            {
                bootom.Children.Remove(text);
                TopBoards.Remove(storyboard);
                text = null;
                storyboard = null;
                bootom.RowDefinitions.Remove(row);
            };
            storyboard.Begin();
        }


        DankumuText CreateText(DankumuTextStyle style,string text)
        {
            DankumuText text1 = new DankumuText() { Text = text, Foreground = style.Color,FontSize = style.Size
            ,FontFamily = style.FontFamily,FontWeight = style.FontWeight};
            return text1;
        }
    }

    public class DankumuText: TextBlock
    {

    }
}
