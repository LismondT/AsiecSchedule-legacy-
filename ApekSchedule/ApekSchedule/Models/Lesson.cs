using System;
using System.Collections.Generic;
using System.Text;

namespace ApekSchedule.Models
{
	public class Lesson
	{
		public int Number { get; set; }
		public string Name { get; set; }
		public string Group { get; set; }
		public string Teacher { get; set; }
		public string Classroom { get; set; }
		public string Territory { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public TimeSpan Duration => EndTime - StartTime;

		public override string ToString()
		{
			string result = "";

			result += $"[]Number: {Number}\n";
			result += $"| Name: {Name}\n";
			result += $"| Group: {Group}\n";
			result += $"| Teacher: {Teacher}\n";
			result += $"| Classroom: {Classroom}\n";
			result += $"| Territory: {Territory}\n";
			result += $"| StartTime: {StartTime}\n";
			result += $"| EndTime: {EndTime}\n";
			result += $"= Duration: {Duration}\n";

			return result;
		}
	}
}
