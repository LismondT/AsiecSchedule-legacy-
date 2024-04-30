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
		//public static string RequestId { get; set; }
		//public static AsiecData.RequestBy RequestType { get; set; }
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
			//string theme = Preferences.Get(SettingKeys.Theme, ThemeStyle.ThemesNames.First());

			ThemeStyle.SetTheme(ref mergedDictionaries, /*theme*/ AppSettings.Theme);

			//RequestId = Preferences.Get(SettingKeys.RequestId, string.Empty);
			//RequestType = (AsiecData.RequestBy)Preferences.Get(SettingKeys.RequestType, (int)AsiecData.RequestBy.GroupId);
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
