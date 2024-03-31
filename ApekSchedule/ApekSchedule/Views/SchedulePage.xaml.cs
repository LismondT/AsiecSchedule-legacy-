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

namespace ApekSchedule.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SchedulePage : ContentPage
	{
		public SchedulePage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			FirstDatePicker.Date = DateTime.Now;
			LastDatePicker.Date = DateTime.Now.AddDays(1);
			GetScheduleButton.Clicked += GetScheduleButton_Clicked;

			StyleSheet styleSheet = new StyleSheet() { };

			GetScheduleButton_Clicked(this, EventArgs.Empty);
		}

		private async void GetScheduleButton_Clicked(object sender, EventArgs e)
		{
			DateTime firstDate = FirstDatePicker.Date;
			DateTime lastDate = LastDatePicker.Date;

			if (firstDate > lastDate)
			{
				await DisplayAlert("Некорректная дата", "Первая дата должна быть меньше второй", "ОК");
				return;
			}

			Schedule schedule = await App.AsiecParser.GetSchedule("9ИСиП231", firstDate, lastDate);
			List<DayViewModel> daysCollection = new List<DayViewModel>();

			if (schedule.Days == null)
				return;

			foreach(Day day in schedule.Days)
			{
				daysCollection.Add(new DayViewModel(day));
			}

			DaysCollectionView.ItemsSource = daysCollection;
		}
	}
}