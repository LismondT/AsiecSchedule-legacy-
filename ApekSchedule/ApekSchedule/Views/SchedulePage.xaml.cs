using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ApekSchedule.ViewModels;
using ApekSchedule.Models;
using Xamarin.Forms.StyleSheets;
using Xamarin.Essentials;
using ApekSchedule.Data;

namespace ApekSchedule.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SchedulePage : ContentPage
	{
		public SchedulePage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			//ResourceDictionary theme = Application.Current.Resources.MergedDictionaries.FirstOrDefault();

			//if (theme != null)
			//{
				Resources.MergedDictionaries.Clear();
				Resources.MergedDictionaries.Add(ThemeStyle.ThemeDictionary);
				//DaysCollectionView.SetTheme(theme);
			//}

			FirstDatePicker.Date = DateTime.Now;
			LastDatePicker.Date = DateTime.Now.AddDays(1);
			
			if (App.Schedule == null)
				App.Schedule = await App.AsiecParser.GetSchedule(App.RequestId, DateTime.Now, DateTime.Now.AddDays(1));

			LoadSchedule(App.Schedule);

			GetScheduleButton.Clicked += GetScheduleButton_Clicked;
		}

		private async void GetScheduleButton_Clicked(object sender, EventArgs e)
		{
			DateTime firstDate = FirstDatePicker.Date;
			DateTime lastDate = LastDatePicker.Date;
			string requestId = App.RequestId;

			if (firstDate > lastDate)
			{
				await DisplayAlert("Некорректная дата", "Первая дата должна быть меньше второй", "ОК");
				return;
			}

			App.Schedule = await App.AsiecParser.GetSchedule(requestId, firstDate, lastDate);
			
			LoadSchedule(App.Schedule);
		}

		private void LoadSchedule(Schedule schedule)
		{
			List<DayViewModel> daysCollection = new List<DayViewModel>();

			if (schedule == null || schedule.Days == null)
				return;

			foreach (Day day in schedule.Days)
			{
				daysCollection.Add(new DayViewModel(day));
			}

			DaysCollectionView.ItemsSource = daysCollection;
		}
	}
}