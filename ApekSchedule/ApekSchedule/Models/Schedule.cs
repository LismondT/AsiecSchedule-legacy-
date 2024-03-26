using System;
using System.Collections.Generic;
using System.Text;

namespace ApekSchedule.Models
{
	public class Schedule
	{
		public DateTime FirstDate { get; set; }
		public DateTime LastDate { get; set; }

		public List<Day> Days { get; set; }

		public override string ToString()
		{
			string result = "";

			result += $"[] FirstDate: {FirstDate}\n";
			result += $"[] LastDate: {LastDate}\n";

			if (Days == null)
			{
				result += "Days: Null";
				return result;
			}

			foreach (Day day in Days)
			{
				string dayStr = day.ToString();
				dayStr = dayStr.Replace("\t", "\t\t");
				result += " L" + dayStr;
			}

			return result;
		}
	}
}
