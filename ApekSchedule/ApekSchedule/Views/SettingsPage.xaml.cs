using ApekSchedule.Themes;
using ApekSchedule.Data;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace ApekSchedule.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
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
						break;
					case Theme.Light:
					default:
						mergedDictionaries.Add(new LightTheme());
						break;
				}
			}

			var schedule = Shell.Current.Items[0].Items.FirstOrDefault(i => i.Title == "Test");

			if (schedule != null)
			{
				ShellContent shellContent = new ShellContent()
				{
					Title = "Test",
					ContentTemplate = new DataTemplate(typeof(SchedulePage)),
				};

				Shell.Current.Items[0].Items.Remove(schedule);
				Shell.Current.Items[0].Items.Add(shellContent);
			}

		}
	}
}