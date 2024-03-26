using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using ApekSchedule.Models;
using System.Collections.ObjectModel;

namespace ApekSchedule.ViewModels
{
	public class DayViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private Day _day;
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


		public DateTime Date
		{
			get { return _day.Date; }
			set
			{
				if (_day.Date != value)
				{
					_day.Date = value;
					OnPropertyChanged(nameof(Date));
				}
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
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
