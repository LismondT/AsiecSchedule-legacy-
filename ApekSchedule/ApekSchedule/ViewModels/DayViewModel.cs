using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using ApekSchedule.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ApekSchedule.ViewModels
{
	public class DayViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private readonly Day _day;
		private ObservableCollection<LessonViewModel> _lessons;


		public DayViewModel() { _day = new Day(); }
		public DayViewModel(Day day)
		{
			_day = day;
			
			_lessons = new ObservableCollection<LessonViewModel>();

            foreach (Lesson lesson in day.Lessons)
            {
                _lessons.Add(new LessonViewModel(lesson));
            }
        }


		public string Date
		{
			get
			{
				DayOfWeek cDay = _day.Date.DayOfWeek;
				string dayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(cDay);
				return $"{_day.Date:d}, {dayOfWeek}";
			}
		}

		public ObservableCollection<LessonViewModel> Lessons
		{
			get { return _lessons; }
			set
			{
				_lessons = value;
				OnPropertyChanged(nameof(Lessons));
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
