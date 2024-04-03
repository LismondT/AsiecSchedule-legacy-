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
			ResourceDictionary theme = Application.Current.Resources.MergedDictionaries.FirstOrDefault();
			
			SetNavBarIsVisible(this, false);

			if (theme != null)
			{
				Color backgroundColor = (Color)theme["NavigationBarBackgroundColor"];
				Color textColor = (Color)theme["NavigationBarSelectedTextColor"];
				Color unselectedTextColor = (Color)theme["NavigationBarUnselectedTextColor"];


				SetTabBarBackgroundColor(this, backgroundColor);
				SetTabBarTitleColor(this, textColor);
				SetTabBarUnselectedColor(this, unselectedTextColor);
			}
		}
	}
}