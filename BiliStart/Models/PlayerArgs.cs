using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart.Models
{
    public class PlayerArgs
    {
        public PlayEnum PlayEnum { get; set; }

        /// <summary>
        /// 视频的BV、AV、EP号等
        /// </summary>
        public string ID { get; set; }

    }

    public enum PlayEnum
    {
        AV,BV, EpVideo
    }
}
