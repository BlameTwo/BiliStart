using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unosquare.FFME;
using Unosquare.FFME.Common;
using MediaElement = Unosquare.FFME.MediaElement;

namespace BiliStart.Controls
{
    /// <summary>
    /// PlayMediaElment.xaml 的交互逻辑
    /// </summary>
    public partial class PlayMediaElment : UserControl
    {
        public PlayMediaElment()
        {
            InitializeComponent();
        }
        public object Source { get; set; }
        private void MediaElement_MediaInitializing(object sender, Unosquare.FFME.Common.MediaInitializingEventArgs e)
        {
            e.Configuration.PrivateOptions.Add("referer", "https://bilibili.com");
            e.Configuration.PrivateOptions["user_agent"] = $"{typeof(ContainerConfiguration).Namespace}/{typeof(ContainerConfiguration).Assembly.GetName().Version}";
        }

        /// <summary>
        /// 设置播放源
        /// </summary>
        public void SetSource()
        {

        }

        public object GetSource()
        {
            return Source;
        }

    }
}
