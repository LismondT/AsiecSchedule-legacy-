using ApekSchedule.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ApekSchedule.ViewModels
{
	public class LessonViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private readonly Lesson lesson;

		public LessonViewModel() { lesson = new Lesson(); }
		public LessonViewModel(Lesson lesson)
		{
			this.lesson = lesson;
		}

		public string Preview
		{
			get => $"{lesson.Number}. ({lesson.StartTime:hh\\:mm}-{lesson.EndTime:hh\\:mm}) Длительность: {lesson.Duration:hh\\:mm}";
		}

		public string Name
		{
			get => $"Предмет: {lesson.Name}";
			set
			{
				if (lesson.Name != value)
				{
					lesson.Name = value;
					OnPropertyChanged(nameof(Name));
				}
			}
		}

		public string Group
		{
			get => $"Группа: {lesson.Group}";
		}

		public string Teacher
		{
			get => $"Преподаватель: {lesson.Teacher}";
		}

		public string Classroom
		{
			get => $"Аудитория: {lesson.Classroom}";
		}

		public string Territory
		{
			get => $"{Classroom} | {lesson.Territory}";
		}

		public string Duration
		{
			get => $"Длительность: {lesson.Duration:hh\\:mm}";
		}


		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
