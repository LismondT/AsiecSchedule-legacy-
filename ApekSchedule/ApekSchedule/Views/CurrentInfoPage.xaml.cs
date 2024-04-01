using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ApekSchedule.ViewModels;
using ApekSchedule.Models;

namespace ApekSchedule.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CurrentInfoPage : ContentPage
	{
		private static Day _currentDay;

		private bool _isDebug;

		public static Day CurrentDay
		{
			set => _currentDay = value;
		}

		public CurrentInfoPage()
		{
			InitializeComponent();
			_isDebug = true;
		}

		protected async override void OnAppearing()
		{
			Schedule curDaySchedule = await App.AsiecParser.GetSchedule("9ИСиП231", DateTime.Now, DateTime.Now);
			_currentDay = curDaySchedule.Days.Count != 0 ? curDaySchedule.Days[0] : new Day() { Date = DateTime.Now };

			await SetCurrentInfo();
		}

		private async Task SetCurrentInfo()
		{
			if (_currentDay.Date.Date != DateTime.Now.Date)
			{
				await DisplayAlert("Ошибка", "Текущая дата не совпадает с полученной", "ОК");
				return;
			}

			if (_currentDay.Lessons == null)
			{
				SetOnlyCurrentLessonName("Сегодня выходной");
				return;
			}

			TimeSpan prevEnd = TimeSpan.Zero;
			TimeSpan firstTime = _currentDay.Lessons[0].StartTime;

			TimeSpan currentTime = _isDebug ? DatePicker.Time : DateTime.Now.TimeOfDay;

			foreach (Lesson lesson in _currentDay.Lessons)
			{

				TimeSpan start = lesson.StartTime;
				TimeSpan end = lesson.EndTime;

				bool is_lessonTime = currentTime >= start && currentTime <= end;
				bool is_restTime = (currentTime >= prevEnd && currentTime <= start) && prevEnd != TimeSpan.Zero;

				prevEnd = lesson.EndTime;

				if (is_lessonTime || is_restTime)
				{
					TimeSpan endTimer = is_lessonTime ? end : start;
					
					if (!is_lessonTime)
					{
						SetOnlyCurrentLessonName("Перемена");
					}
					else
					{
						SetCurrentLessonInfo(lesson);
					}

					
					StartTimer(endTimer);
					return;
				}
			}

			if (currentTime < firstTime)
			{
				SetOnlyCurrentLessonName("Пары ещё не начались");
			}

			if (prevEnd < currentTime)
			{
				SetOnlyCurrentLessonName("Пары закончились");
			}
		}

		private void SetCurrentLessonInfo(Lesson currentLesson)
		{
			CurrentLessonNameLabel.Text = $"Пара: {currentLesson.Name}";
			CurrentLessonTeacherLabel.Text = $"Преподаватель: {currentLesson.Teacher}";
			CurrentLessonClassroomLabel.Text = $"Аудитория: {currentLesson.Classroom}";
			CurrentLessonTimeLabel.Text = $"Время: {currentLesson.StartTime:hh\\:mm} - {currentLesson.EndTime:hh\\:mm}";

			CurrentLessonNameLabel.IsVisible = true;
			CurrentLessonTeacherLabel.IsVisible = true;
			CurrentLessonClassroomLabel.IsVisible = true;
			CurrentLessonTimeLabel.IsVisible = true;
			EndTimerFrame.IsVisible = true;
		}

		private void SetOnlyCurrentLessonName(string name)
		{
			HideCurrentLessonInfo();
			CurrentLessonNameLabel.IsVisible = true;
			CurrentLessonNameLabel.Text = name;
		}

		private void HideCurrentLessonInfo()
		{
			CurrentLessonNameLabel.IsVisible = false;
			CurrentLessonTeacherLabel.IsVisible = false;
			CurrentLessonClassroomLabel.IsVisible = false;
			CurrentLessonTimeLabel.IsVisible = false;
			EndTimerFrame.IsVisible = false;
		}


		private void StartTimer(TimeSpan end)
		{
			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{
				TimeSpan currentTime = _isDebug ? DatePicker.Time : DateTime.Now.TimeOfDay;
				TimeSpan duration = end - currentTime;
				EndTimerLabel.Text = duration.ToString(@"hh\:mm\:ss");

				if (duration.TotalSeconds <= 0)
				{
					Task.Run(async () =>
					{
						await SetCurrentInfo();
					});
					return false;
				}

				return true;
			});

			
		}

		private async void SetTimeButton_Clicked(object sender, EventArgs e)
		{
			await SetCurrentInfo();
		}

		private async void DebugButton_Clicked(object sender, EventArgs e)
		{
			if (_isDebug)
			{
				_isDebug = false;
				DatePicker.IsVisible = false;
				SetTimeButton.IsVisible = false;
			}
			else
			{
				_isDebug = true;
				DatePicker.IsVisible = true;
				SetTimeButton.IsVisible = true;
			}

			await SetCurrentInfo();
		}
	}
}