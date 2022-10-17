using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace BiliStart.Danmaku
{
    [ContentProperty("Text")]
    public class OutlinedTextBlock : FrameworkElement, IDisposable {
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill",
            typeof(Brush),
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke",
            typeof(Brush),
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness",
            typeof(double),
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner(
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(OnFormattedTextUpdated));

        public static readonly DependencyProperty FontSizeProperty = TextElement.FontSizeProperty.AddOwner(
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(OnFormattedTextUpdated));

        public static readonly DependencyProperty FontStretchProperty = TextElement.FontStretchProperty.AddOwner(
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(OnFormattedTextUpdated));

        public static readonly DependencyProperty FontStyleProperty = TextElement.FontStyleProperty.AddOwner(
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(OnFormattedTextUpdated));

        public static readonly DependencyProperty FontWeightProperty = TextElement.FontWeightProperty.AddOwner(
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(OnFormattedTextUpdated));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(OnFormattedTextInvalidated));

        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(
            "TextAlignment",
            typeof(TextAlignment),
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(OnFormattedTextUpdated));

        public static readonly DependencyProperty TextDecorationsProperty = DependencyProperty.Register(
            "TextDecorations",
            typeof(TextDecorationCollection),
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(OnFormattedTextUpdated));

        public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.Register(
            "TextTrimming",
            typeof(TextTrimming),
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(OnFormattedTextUpdated));

        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(
            "TextWrapping",
            typeof(TextWrapping),
            typeof(OutlinedTextBlock),
            new FrameworkPropertyMetadata(TextWrapping.NoWrap, OnFormattedTextUpdated));
        public int Timer { get; set; }


        private FormattedText formattedText;
        private Geometry textGeometry;
        private bool enableOutline;

        public OutlinedTextBlock(bool enableOutline = false) {
            this.enableOutline = enableOutline;
            this.TextDecorations = new TextDecorationCollection();
            refresh_cache_render_pen();
        }

        public OutlinedTextBlock(Func<bool> p) {
            this.enableOutline = p();
            this.TextDecorations = new TextDecorationCollection();
            refresh_cache_render_pen();
        }

        public Brush Fill {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public FontFamily FontFamily {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public FontStretch FontStretch {
            get { return (FontStretch)GetValue(FontStretchProperty); }
            set { SetValue(FontStretchProperty, value); }
        }

        public FontStyle FontStyle {
            get { return (FontStyle)GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        public FontWeight FontWeight {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        public Brush Stroke {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); refresh_cache_render_pen(); }
        }

        public double StrokeThickness {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); refresh_cache_render_pen(); }
        }

        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public TextAlignment TextAlignment {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        public TextDecorationCollection TextDecorations {
            get { return (TextDecorationCollection)this.GetValue(TextDecorationsProperty); }
            set { this.SetValue(TextDecorationsProperty, value); }
        }

        public TextTrimming TextTrimming {
            get { return (TextTrimming)GetValue(TextTrimmingProperty); }
            set { SetValue(TextTrimmingProperty, value); }
        }

        public TextWrapping TextWrapping {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }

        private Pen cache_render_pen;

        private void refresh_cache_render_pen() {
            if (enableOutline) {
                cache_render_pen = new Pen(this.Stroke, this.StrokeThickness);
            }
        }

        protected override void OnRender(DrawingContext drawingContext) {
            this.EnsureGeometry();

            drawingContext.DrawGeometry(this.Fill, cache_render_pen, this.textGeometry);
        }

        protected override Size MeasureOverride(Size availableSize) {
            this.EnsureFormattedText();
            this.formattedText.MaxTextWidth = Math.Min(3579139, availableSize.Width);
            this.formattedText.MaxTextHeight = Math.Max(0.0001d, availableSize.Height);

            // return the desired size
            return new Size(this.formattedText.Width, this.formattedText.Height);
        }

        protected override Size ArrangeOverride(Size finalSize) {
            this.EnsureFormattedText();

            this.formattedText.MaxTextWidth = finalSize.Width;
            //这里区域过小会无法绘制
            this.formattedText.MaxTextHeight = finalSize.Height;

            this.textGeometry = null;

            return finalSize;
        }

        private static void OnFormattedTextInvalidated(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) {
            var outlinedTextBlock = (OutlinedTextBlock)dependencyObject;
            outlinedTextBlock.formattedText = null;
            outlinedTextBlock.textGeometry = null;

            outlinedTextBlock.InvalidateMeasure();
            outlinedTextBlock.InvalidateVisual();
        }

        private static void OnFormattedTextUpdated(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) {
            var outlinedTextBlock = (OutlinedTextBlock)dependencyObject;
            outlinedTextBlock.UpdateFormattedText();
            outlinedTextBlock.textGeometry = null;

            outlinedTextBlock.InvalidateMeasure();
            outlinedTextBlock.InvalidateVisual();
        }

        private void EnsureFormattedText() {
            if (this.formattedText != null || this.Text == null) {
                return;
            }

            this.formattedText = new FormattedText(
                this.Text,
                CultureInfo.CurrentUICulture,
                this.FlowDirection,
                new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, FontStretches.Normal),
                this.FontSize,
                Brushes.Black);

            this.UpdateFormattedText();
        }

        private void UpdateFormattedText() {
            if (this.formattedText == null) {
                return;
            }

            this.formattedText.MaxLineCount = this.TextWrapping == TextWrapping.NoWrap ? 1 : int.MaxValue;
            this.formattedText.TextAlignment = this.TextAlignment;
            this.formattedText.Trimming = this.TextTrimming;

            this.formattedText.SetFontSize(this.FontSize);
            this.formattedText.SetFontStyle(this.FontStyle);
            this.formattedText.SetFontWeight(this.FontWeight);
            this.formattedText.SetFontFamily(this.FontFamily);
            this.formattedText.SetFontStretch(this.FontStretch);
            this.formattedText.SetTextDecorations(this.TextDecorations);
        }

        private Point point_0 = new Point(0, 0);
        private Func<bool> p;

        private void EnsureGeometry() {
            if (this.textGeometry != null) {
                return;
            }

            this.EnsureFormattedText();
            this.textGeometry = this.formattedText.BuildGeometry(point_0);
        }

        public void Dispose(bool CleanManaged) {
            this.TextDecorations.Clear();
        }

        public void Dispose() {
            this.Dispose(true);
        }
    }
}
