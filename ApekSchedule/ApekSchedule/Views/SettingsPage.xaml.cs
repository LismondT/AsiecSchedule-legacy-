using ApekSchedule.Data;
using ApekSchedule.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApekSchedule.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			ResourceDictionary theme = Application.Current.Resources.MergedDictionaries.FirstOrDefault();

			if (theme != null)
			{
				Resources.MergedDictionaries.Clear();
				Resources.MergedDictionaries.Add(theme);
				ThemePicker.TitleColor = (Color)theme["PrimaryTextColor"];
			}
		}

		void OnThemePickerSelectionChanged(object sender, EventArgs e)
		{
			Picker picker = sender as Picker;
			Theme theme = (Theme)picker.SelectedItem;

			ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
			if (mergedDictionaries != null)
			{
				mergedDictionaries.Clear();

				switch (theme)
				{
					case Theme.Dark:
						mergedDictionaries.Add(new DarkTheme());
						Preferences.Set(SettingKeys.Theme, (int)Theme.Dark);
						break;
					case Theme.Light:
					default:
						mergedDictionaries.Add(new LightTheme());
						Preferences.Set(SettingKeys.Theme, (int)Theme.Light);
						break;
				}
				
				picker.TextColor = (Color)mergedDictionaries.FirstOrDefault()["PrimaryTextColor"];
			}
		}
	}
}