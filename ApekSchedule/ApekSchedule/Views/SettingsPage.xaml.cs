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

				ThemePicker.TitleColor = ThemeStyle.PrimaryTextColor;
				IdPicker.TitleColor = ThemeStyle.PrimaryTextColor;
			}

			IdPicker.ItemsSource = AsiecData.GroupId.Keys.ToList();
			IdPicker.Title = Preferences.Get(SettingKeys.RequestId, "Выбрать");
		}


		private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
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

				ThemeStyle.Load(mergedDictionaries.FirstOrDefault());

				picker.TextColor = ThemeStyle.PrimaryTextColor;
				IdPicker.TitleColor = ThemeStyle.PrimaryTextColor;
				IdPicker.TextColor = ThemeStyle.PrimaryTextColor;

				Shell.SetTabBarBackgroundColor(Shell.Current, ThemeStyle.NavigationBarBackgroundColor);
				Shell.SetTabBarUnselectedColor(Shell.Current, ThemeStyle.NavigationBarUnselectedTextColor);
				Shell.SetTabBarTitleColor(Shell.Current, ThemeStyle.NavigationBarSelectedTextColor);
			}
		}


		private void IdPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			Picker picker = sender as Picker;
			string selectedItem = (string)picker.SelectedItem;

			if (selectedItem == "Выбрать" || selectedItem == null)
				return;

			picker.TextColor = ThemeStyle.PrimaryTextColor;

			Preferences.Set(SettingKeys.RequestId, selectedItem);
			App.RequestId = selectedItem;
			App.Schedule = null;
        }
    }
}