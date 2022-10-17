using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BiliStart.ViewModel
{
    public class HomeVideoVM:ObservableObject
    {
		public HomeVideoVM()
		{

        }
        private BiliBiliAPI.Models.HomeVideo.Item Item;

		public BiliBiliAPI.Models.HomeVideo.Item _Item
		{
			get { return Item; }
			set=>SetProperty(ref Item, value);
		}

    }
}
