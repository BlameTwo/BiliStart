using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart.Interfaces
{
    public interface IPageBase
    {
        /// <summary>
        /// 传递参数
        /// </summary>
        /// <param name="ExtraData">任意类型参数</param>
        void SetExtraData(object ExtraData);
    }
}
