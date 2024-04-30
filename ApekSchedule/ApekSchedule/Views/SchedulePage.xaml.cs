using ApekSchedule.Data;
using ApekSchedule.Models;
using ApekSchedule.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
			Resources.MergedDictionaries.Clear();
			Resources.MergedDictionaries.Add(ThemeStyle.ThemeDictionary);

			if (App.Schedule == null && AppSettings.RequestId != string.Empty)
				App.Schedule = await App.AsiecParser.GetSchedule(AppSettings.RequestId, AppSettings.RequestType, DateTime.Now, DateTime.Now.AddDays(1));
			
			IdLabel.Text = AppSettings.RequestId != string.Empty ? AppSettings.RequestId : "Выберите группу в настройках";

			if (App.Schedule != null)
			{
				LoadSchedule(App.Schedule);
				FirstDatePicker.Date = App.Schedule.FirstDate;
				LastDatePicker.Date = App.Schedule.LastDate;
			}

			GetScheduleButton.Clicked += GetScheduleButton_Clicked;
		}

		private async void GetScheduleButton_Clicked(object sender, EventArgs e)
		{
			DateTime firstDate = FirstDatePicker.Date;
			DateTime lastDate = LastDatePicker.Date;
			string requestId = AppSettings.RequestId;
			AsiecData.RequestBy requestType = AppSettings.RequestType;

			if (requestId == string.Empty) return;

			if (firstDate > lastDate)
			{
				await DisplayAlert("Некорректная дата", "Первая дата должна быть меньше второй", "ОК");
				return;
			}

			App.Schedule = await App.AsiecParser.GetSchedule(requestId, requestType, firstDate, lastDate);

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