using ApekSchedule.Data;
using ApekSchedule.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ApekSchedule.Data.AsiecData;

namespace ApekSchedule.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		private Dictionary<string, RequestBy> RequestTypeStrToEnum { get; } = new Dictionary<string, RequestBy>()
		{
			{ "группе", RequestBy.GroupId },
			{ "преподавателю", RequestBy.TeacherId },
		};

		public SettingsPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

            Resources.MergedDictionaries.Clear();
			Resources.MergedDictionaries.Add(ThemeStyle.ThemeDictionary);

			InitPickers();

			UpdateIdChooseLabel();
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


		private void RequestTypePicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			Picker picker = sender as Picker;
			string selectedItem = (string)picker.SelectedItem;

            if (selectedItem == "выбрать" || selectedItem == null)
                return;

			RequestBy type = RequestTypeStrToEnum[selectedItem];

			Preferences.Set(SettingKeys.RequestType, (int)type);
			Preferences.Set(SettingKeys.RequestId, string.Empty);
			App.RequestType = type;
			App.RequestId = string.Empty;
			
			picker.TextColor = ThemeStyle.PrimaryTextColor;
            IdPicker.ItemsSource = GetIdDictByRequestType(App.RequestType).Keys.ToArray();
			IdPicker.Title = "Выбрать";

			UpdateIdChooseLabel();
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


		private void InitPickers()
		{
            string theme = Preferences.Get(SettingKeys.Theme, ThemeStyle.ThemesNames.FirstOrDefault());
            RequestBy requestType = (RequestBy)Preferences.Get(SettingKeys.RequestType, (int)RequestBy.GroupId);
            string requestTypeName = RequestTypeStrToEnum.FirstOrDefault(x => x.Value == requestType).Key;
            string requestId = Preferences.Get(SettingKeys.RequestId, "");

            ThemePicker.ItemsSource = ThemeStyle.ThemesNames;
            ThemePicker.TitleColor = ThemeStyle.PrimaryTextColor;
            ThemePicker.Title = theme;

            IdPicker.ItemsSource = GetIdDictByRequestType(App.RequestType).Keys.ToArray();
            IdPicker.TitleColor = ThemeStyle.PrimaryTextColor;
            IdPicker.Title = requestId;

            RequestTypePicker.ItemsSource = RequestTypeStrToEnum.Keys.ToArray();
            RequestTypePicker.TitleColor = ThemeStyle.PrimaryTextColor;
            RequestTypePicker.Title = requestTypeName;
        }


		private void UpdateIdChooseLabel()
		{
			string title = "";

            switch (App.RequestType)
            {
                case RequestBy.GroupId: title = "Группа:";
                    break;
                case RequestBy.TeacherId: title = "Преподаватель:";
                    break;
                case RequestBy.ClassroomId: title = "Аудитория";
                    break;
            }

			IdChooseLabel.Text = title;
        }
    }
}