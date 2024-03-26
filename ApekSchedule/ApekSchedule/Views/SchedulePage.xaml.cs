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
			LastDatePicker.Date = DateTime.Now.AddDays(1);
			GetScheduleButton.Clicked += GetScheduleButton_Clicked;
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

		//private void AddDay(Day day)
		//{
		//	DateTime date = day.Date;
		//	string dateStr = date.ToString("dd.MM.yyyy");
		//	string dayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(date.DayOfWeek);

		//	Frame dayFrame = new Frame()
		//	{
		//		Style = (Style)Resources["DayFrame"],
		//		Content = new Label()
		//		{
		//			Style = (Style)Resources["DayFrameDateLabel"],
		//			Text = $"{dateStr}, {dayOfWeek}",
		//		}
		//	};

		//	//ScheduleMain.Children.Add(dayFrame);

		//	foreach (var lesson in day.Lessons)
		//	{
		//		AddLesson(lesson);
		//	}
		//}



		//private void AddLesson(Lesson lesson)
		//{
		//	string startTime = lesson.StartTime.ToString("hh\\:mm");
		//	string endTime = lesson.EndTime.ToString("hh\\:mm");
		//	string previewText = $"{lesson.Number}. ({startTime}-{endTime})";
		//	string nameText = $"Пара: {lesson.Name}";
		//	string teacherText = $"Преподаватель: {lesson.Teacher}";
		//	string classroomText = $"Аудитория: {lesson.Classroom}";
		//	string territoryText = lesson.Territory;

		//	Frame lessonFrame = new Frame()
		//	{
		//		Style = (Style)Resources["LessonFrame"],
		//		Content = new StackLayout()
		//		{
		//			Children =
		//			{
		//				new Label() { Text = previewText, Style = (Style)Resources["LessonFrameText"] },
		//				new Label() { Text = nameText, Style = (Style)Resources["LessonFrameText"] },
		//				new Label() { Text = teacherText, Style = (Style)Resources["LessonFrameText"] },
		//				new Label() { Text = classroomText, Style =(Style) Resources["LessonFrameText"] },
		//				new Label() { Text = territoryText, Style =(Style) Resources["LessonFrameText"] },
		//			}
		//		}
		//	};

		//	//ScheduleMain.Children.Add(lessonFrame);
		//}
	}
}