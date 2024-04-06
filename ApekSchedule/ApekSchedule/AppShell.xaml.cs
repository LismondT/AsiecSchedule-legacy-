using ApekSchedule.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApekSchedule
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{

			InitializeComponent();
			
			SetNavBarIsVisible(this, false);
			SetTabBarBackgroundColor(this, ThemeStyle.NavigationBarBackgroundColor);
			SetTabBarTitleColor(this, ThemeStyle.NavigationBarSelectedTextColor);
			SetTabBarUnselectedColor(this, ThemeStyle.NavigationBarUnselectedTextColor);
		}
	}
}