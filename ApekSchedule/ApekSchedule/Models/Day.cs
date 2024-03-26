using System;
using System.Collections.Generic;
using System.Text;

namespace ApekSchedule.Models
{
	public class Day
	{
		public DateTime Date { get; set; }
		public List<Lesson> Lessons { get; set; }

		public override string ToString()
		{
			string result = "";

			result += $"[] Date: {Date}\n";

			foreach (Lesson lesson in Lessons)
			{
				string lessonStr = lesson.ToString();

				lessonStr = lessonStr.Replace("|", "    |").Replace("=", "    =").Replace("[]", "   L[]");

				result += lessonStr;
			}

			return result;
		}
	}
}
