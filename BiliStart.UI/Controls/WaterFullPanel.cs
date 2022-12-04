using System.Collections.Generic;
using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace BiliStart.UI.Controls;
public class WaterFullPanel:Panel
{


    public int PanelRowOrColumn
    {
        get
        {
            return (int)GetValue(PanelRowOrColumnProperty);
        }
        set
        {
            SetValue(PanelRowOrColumnProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for PanelRowOrColumn.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PanelRowOrColumnProperty =
        DependencyProperty.Register("PanelRowOrColumn", typeof(int), typeof(WaterFullPanel), new PropertyMetadata(2));




    public Orientation Orientation
    {
        get
        {
            return (Orientation )GetValue(OrientationProperty);
        }
        set
        {
            SetValue(OrientationProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register("Orientation", typeof(Orientation ), typeof(WaterFullPanel), new PropertyMetadata(Orientation.Vertical));


    protected override Size MeasureOverride(Size availableSize)
    {
        if (PanelRowOrColumn < 1)

        {

            throw (new ArgumentOutOfRangeException("NumberOfColumnsOrRows", "NumberOfColumnsOrRows must >0"));//太窄

        }

        var LenList = new List<double>();

        for (int i = 0; i < PanelRowOrColumn; i++)

        {

            LenList.Add(0);

        }



        if (Orientation == Orientation.Vertical)

        {
            //如果等于垂直布局，那么子项最大宽度的就等于空间宽度除以定义的列数
            double maxWidth = availableSize.Width / PanelRowOrColumn;

            Size maxSize = new Size(maxWidth, double.PositiveInfinity);

            foreach (var item in Children)

            {
                //在这里最大大小
                item.Measure(maxSize);
                //这里取子项高度
                var itemHeight = item.DesiredSize.Height;

                //最小行
                var minLen = LenList[0];

                int minP = 0;

                for (int i = 1; i < PanelRowOrColumn; i++)
                {
                    if (LenList[i] < minLen)

                    {

                        minLen = LenList[i];

                        minP = i;

                    }

                }
                LenList[minP] += itemHeight;

            }

            var maxLen = LenList[0];

            int maxP = 0;

            for (int i = 1; i < PanelRowOrColumn; i++)

            {

                if (LenList[i] > maxLen)

                {

                    maxLen = LenList[i];

                    maxP = i;

                }
            }
            return new Size(availableSize.Width, LenList[maxP]);
        }
        else
        {
            double maxHeight = availableSize.Height / PanelRowOrColumn;

            Size maxSize = new Size(double.PositiveInfinity, maxHeight);

            foreach (var item in Children)

            {

                item.Measure(maxSize);

                var itemWidth = item.DesiredSize.Width;

                var minLen = LenList[0];

                int minP = 0;

                for (int i = 1; i < PanelRowOrColumn; i++)

                {

                    if (LenList[i] < minLen)

                    {

                        minLen = LenList[i];

                        minP = i;

                    }

                }

                LenList[minP] += itemWidth;

            }

            var maxLen = LenList[0];

            int maxP = 0;

            for (int i = 1; i < PanelRowOrColumn; i++)

            {

                if (LenList[i] > maxLen)

                {

                    maxLen = LenList[i];

                    maxP = i;

                }

            }

            return new Size(LenList[maxP], availableSize.Height);

        }
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        if (PanelRowOrColumn < 1)
        {
            throw (new ArgumentOutOfRangeException("NumberOfColumnsOrRows", "NumberOfColumnsOrRows must >0"));//太窄
        }
        var LenList = new List<double>();
        var posXorYList = new List<double>();
        if (Orientation == Orientation.Vertical)
        {
            double maxWidth = finalSize.Width / PanelRowOrColumn;

            //列的长度和左上角的x值

            for (int i = 0; i < PanelRowOrColumn; i++)

            {

                LenList.Add(0);

                posXorYList.Add(i * maxWidth);

            }

            foreach (var item in Children)

            {

                var itemHeight = item.DesiredSize.Height;

                var minLen = LenList[0];

                int minP = 0;

                for (int i = 1; i < PanelRowOrColumn; i++)

                {

                    if (LenList[i] < minLen)

                    {

                        minLen = LenList[i];

                        minP = i;

                    }

                }

                item.Arrange(new Rect(posXorYList[minP], LenList[minP], item.DesiredSize.Width, item.DesiredSize.Height));

                LenList[minP] += item.DesiredSize.Height;

            }

            var maxLen = LenList[0];

            int maxP = 0;

            for (int i = 1; i < PanelRowOrColumn; i++)

            {

                if (LenList[i] > maxLen)

                {

                    maxLen = LenList[i];

                    maxP = i;

                }

            }

            return new Size(finalSize.Width, LenList[maxP]);

        }

        else

        {

            double maxHeight = finalSize.Height / PanelRowOrColumn;

            //行的长度和左上角的y值

            for (int i = 0; i < PanelRowOrColumn; i++)

            {

                LenList.Add(0);

                posXorYList.Add(i * maxHeight);

            }

            foreach (var item in Children)

            {

                var itemWidth = item.DesiredSize.Width;

                var minLen = LenList[0];

                int minP = 0;

                for (int i = 1; i < PanelRowOrColumn; i++)

                {

                    if (LenList[i] < minLen)

                    {

                        minLen = LenList[i];

                        minP = i;

                    }

                }

                item.Arrange(new Rect(LenList[minP], posXorYList[minP], item.DesiredSize.Width, item.DesiredSize.Height));

                LenList[minP] += item.DesiredSize.Width;

            }

            var maxLen = LenList[0];

            int maxP = 0;

            for (int i = 1; i < PanelRowOrColumn; i++)

            {

                if (LenList[i] > maxLen)

                {

                    maxLen = LenList[i];

                    maxP = i;

                }

            }

            return new Size(LenList[maxP], finalSize.Height);

        }
    }
}
