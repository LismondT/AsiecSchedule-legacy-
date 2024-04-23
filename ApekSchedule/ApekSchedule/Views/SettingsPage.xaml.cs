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

			Resources.MergedDictionaries.Clear();
			Resources.MergedDictionaries.Add(ThemeStyle.ThemeDictionary);

			ThemePicker.TitleColor = ThemeStyle.PrimaryTextColor;
			IdPicker.TitleColor = ThemeStyle.PrimaryTextColor;

			IdPicker.ItemsSource = AsiecData.GroupId.Keys.ToList();
			ThemePicker.ItemsSource = ThemeStyle.ThemesNames;

			IdPicker.Title = Preferences.Get(SettingKeys.RequestId, "Выбрать");
		}


		private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			Picker picker = sender as Picker;
			string theme = (string)picker.SelectedItem;

			ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;

			ThemeStyle.SetTheme(ref mergedDictionaries, theme);

			picker.TextColor = ThemeStyle.PrimaryTextColor;
			IdPicker.TitleColor = ThemeStyle.PrimaryTextColor;
			IdPicker.TextColor = ThemeStyle.PrimaryTextColor;

			Shell.SetTabBarBackgroundColor(Shell.Current, ThemeStyle.NavigationBarBackgroundColor);
			Shell.SetTabBarUnselectedColor(Shell.Current, ThemeStyle.NavigationBarUnselectedTextColor);
			Shell.SetTabBarTitleColor(Shell.Current, ThemeStyle.NavigationBarSelectedTextColor);
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