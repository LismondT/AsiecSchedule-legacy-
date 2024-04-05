using ApekSchedule.Data;
using ApekSchedule.Themes;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ApekSchedule
{


	public partial class App : Application
	{
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

			if (mergedDictionaries != null)
			{
				mergedDictionaries.Clear();

				switch (theme)
				{
					case Theme.Dark:
						mergedDictionaries.Add(new DarkTheme());
						break;
					case Theme.Light:
					default:
						mergedDictionaries.Add(new LightTheme());
						break;
				}

			}

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
