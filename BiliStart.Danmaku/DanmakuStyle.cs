using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace BiliStart.Danmaku
{
    /// <summary>
    /// DanmakuStyle Class, Contains some basic danmaku style properties.
    /// Including Color, Font, Outline, Shadow, FontFamily, Position etc.
    /// </summary>

    public enum DanmakuDirection {
        R2L,
        L2R
    }
    public class DanmakuStyle {

        /// <summary>
        /// Create a object represents danmaku style.
        /// </summary>
        /// <param name="Duration">Danmaku duration for disapper (ms)</param>
        /// <param name="ColorR">Danmaku Color - Red</param>
        /// <param name="ColorG">Danmaku Color - Green</param>
        /// <param name="ColorB">Danmaku Color - Blue</param>
        /// <param name="FontSize">Danmaku font size</param>
        /// <param name="OutlineEnabled">Enable danmaku outline stroke</param>
        /// <param name="ShadowEnabled">Enable danmaku shadow (low performance)</param>
        /// <param name="Font">Danmaku font style (Default: Microsoft YaHei)</param>
        /// <param name="PositionX">Danmaku start position - X</param>
        /// <param name="PositionY">Danmaku start position - Y</param>
        public DanmakuStyle(
            TimeSpan Duration,
            byte ColorR = 255, byte ColorG = 255, byte ColorB = 255,
            int FontSize = 30, bool OutlineEnabled = true, bool ShadowEnabled = false, FontFamily Font = null,
            double PositionX = 0, double PositionY = 0,
            DanmakuDirection direction = DanmakuDirection.R2L) {

            this.PositionX = PositionX;
            this.PositionY = PositionY;

            this.ColorR = ColorR;
            this.ColorG = ColorG;
            this.ColorB = ColorB;

            this.FontFamily = Font;
            this.FontSize = FontSize;

            this.OutlineEnabled = OutlineEnabled;
            this.ShadowEnabled = ShadowEnabled;

            this.Duration = Duration;
            this.Direction = direction;
        }

        public double PositionX;
        public double PositionY;
        private byte _ColorR;
        private byte _ColorG;
        private byte _ColorB;
        private FontFamily _FontFamily;
        private int _FontSize;
        public bool OutlineEnabled;
        public bool ShadowEnabled;
        private TimeSpan _Duration;
        public DanmakuDirection Direction;

        public byte ColorR {
            set {
                if (value >= 0 && value <= 255) {
                    this._ColorR = value;
                } else {
                    throw new ArgumentException("Color Range should between 0 and 255.");
                }
            }
            get { return this._ColorR; }
        }
        public byte ColorG {
            set {
                if (value >= 0 && value <= 255) {
                    this._ColorG = value;
                } else {
                    throw new ArgumentException("Color Range should between 0 and 255.");
                }
            }
            get { return this._ColorG; }
        }
        public byte ColorB {
            set {
                if (value >= 0 && value <= 255) {
                    this._ColorB = value;
                } else {
                    throw new ArgumentException("Color Range should between 0 and 255.");
                }
            }
            get { return this._ColorB; }
        }
        public int FontSize {
            set {
                if (value > 0) {
                    this._FontSize = value;
                } else {
                    throw new ArgumentException("FontSize should larger than zero.");
                }
            }
            get { return this._FontSize; }
        }
        public FontFamily FontFamily {
            set {
                if (value == null) {
                    this._FontFamily = (FontFamily)new FontFamilyConverter().ConvertFromString("Microsoft YaHei");
                } else {
                    this._FontFamily = value;
                }
            }
            get { return this._FontFamily; }
        }
        public TimeSpan Duration {
            set {
                if (value != null) {
                    this._Duration = value;
                } else {
                    throw new ArgumentException("Duration should more than zero");
                }
            }
            get { return this._Duration; }
        }
    }

    public enum DanmakuType
    {
        Bottom,
        Top,
        Scroll
    }
}
