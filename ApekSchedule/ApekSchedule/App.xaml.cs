using ApekSchedule.Data;
using ApekSchedule.Models;
using ApekSchedule.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ApekSchedule
{


	public partial class App : Application
	{
		public static string RequestId { get; set; }
		public static Schedule Schedule { get; set; }
		
		static AsiecParser asiecParser;
		public static AsiecParser AsiecParser
		{
			get
			{
				if (asiecParser == null)
					asiecParser = new AsiecParser();
				return asiecParser;
			}
		}


		public App()
		{
			InitializeComponent();

			ICollection<ResourceDictionary> mergedDictionaries = Current.Resources.MergedDictionaries;
			Theme theme;

			if (Preferences.ContainsKey(SettingKeys.Theme))
			{
				theme = (Theme)Preferences.Get(SettingKeys.Theme, 0);

			}
			else
			{
				theme = Theme.Light;
			}

			ThemeStyle.SetTheme(ref mergedDictionaries, theme);

			RequestId = Preferences.Get(SettingKeys.RequestId, "9ИСиП231");
			Schedule = null;

			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
			
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
