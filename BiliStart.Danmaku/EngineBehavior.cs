using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart.Danmaku
{
    public enum DrawMode {
        WPF,
        DX
    }

    public enum CollisionPrevention {
        Enabled,
        Disabled
    }

    public class EngineBehavior {

        /// <summary>
        /// EngineBehavior
        /// </summary>
        /// <param name="DrawMode">WPF or DirectX</param>
        /// <param name="CollisionPrevention">By enable this, danmaku engine will ignore PositionY property of DanmakuStyle, and set it automatically instead.</param>
        public EngineBehavior(DrawMode DrawMode, CollisionPrevention CollisionPrevention) {
            this.DrawMode = DrawMode;
            this.CollisionPrevention = CollisionPrevention;
        }

        public DrawMode DrawMode;
        public CollisionPrevention CollisionPrevention;
    }
}
