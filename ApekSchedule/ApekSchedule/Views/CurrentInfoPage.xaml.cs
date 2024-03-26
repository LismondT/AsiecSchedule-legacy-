using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApekSchedule.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CurrentInfoPage : ContentPage
	{
		//private readonly AsiecParser _asiecParser;
		//private Day _currentDay;
		//private bool is_studyTime;

		public CurrentInfoPage()
		{
			InitializeComponent();

			//_asiecParser = new AsiecParser();
			//is_studyTime = true;
		}

		//protected async override void OnAppearing()
		//{
		//	//Schedule curDaySchedule = await _asiecParser.GetSchedule("9ИСиП231", DateTime.Now, DateTime.Now);
		//	//_currentDay = curDaySchedule.Days.Count != 0 ? curDaySchedule.Days[0] : new Day() { Date = DateTime.Now };

		//	//await SetCurrentInfo();
		//}

		//private async Task SetCurrentInfo()
		//{
		//	if (_currentDay.Date.Date != DateTime.Now.Date)
		//	{
		//		await DisplayAlert("Ошибка", "Текущая дата не совпадает с полученной", "ОК");
		//		return;
		//	}

		//	if (_currentDay.Lessons == null)
		//	{
		//		CurrentInfoLabel.Text = "Сегодня выходной";
		//		return;
		//	}

		//	TimeSpan prevEnd = TimeSpan.Zero;
		//	is_studyTime = true;

		//	foreach (Lesson lesson in _currentDay.Lessons)
		//	{

		//		TimeSpan start = lesson.StartTime;
		//		TimeSpan end = lesson.EndTime;
		//		TimeSpan current = DateTime.Now.TimeOfDay;

		//		bool is_lessonTime = current >= start && current <= end;
		//		bool is_restTime = (current >= prevEnd && current <= start) && prevEnd != TimeSpan.Zero;

		//		prevEnd = lesson.EndTime;

		//		if (is_lessonTime || is_restTime)
		//		{
		//			TimeSpan endTimer = is_lessonTime ? end : start;
		//			string infoMessage = is_lessonTime ? lesson.ToString() : "Перемена";

		//			CurrentInfoLabel.Text = infoMessage;
		//			StartTimer(endTimer);
		//			return;
		//		}
		//	}

		//	is_studyTime = false;
		//	CurrentInfoFrame.IsVisible = false;
		//	CurrentInfoLabel.Text = "Сейчас не учебное время";
		//}




		//private void StartTimer(TimeSpan end)
		//{
		//	if (!is_studyTime)
		//	{
		//		return;
		//	}

		//	Device.StartTimer(TimeSpan.FromSeconds(1), () =>
		//	{
		//		TimeSpan duration = end - DateTime.Now.TimeOfDay;
		//		EndTimerLabel.Text = duration.ToString(@"hh\:mm\:ss");

		//		if (duration.TotalSeconds <= 0)
		//		{
		//			Task.Run(async () =>
		//			{
		//				await SetCurrentInfo();
		//			});
		//			return false;
		//		}

		//		return true;
		//	});
		//}
	}
}