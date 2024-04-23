using ApekSchedule.Data;

using Xamarin.Forms;

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