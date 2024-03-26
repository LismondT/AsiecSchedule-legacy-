using HtmlAgilityPack;
using ApekSchedule.Models;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ApekSchedule.Data
{
	public class AsiecParser
	{
		private static HttpClient client;

		public AsiecParser()
		{
			HttpClientHandler clientHandler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
			};

			client = new HttpClient(clientHandler);
		}

		public async Task<string> GetSheduleBody(string group, DateTime firstDate, DateTime secondDate)
		{
			string firstDateFormat = firstDate.ToString("yyyy-MM-dd");
			string secondDateFormat = secondDate.ToString("yyyy-MM-dd");

			bool getGroupSuccess = AsiecData.GroupId.TryGetValue(group, out string groupId);

			if (getGroupSuccess == false || groupId == null)
			{
				return "";
			}

			var data = new Dictionary<string, string>
			{
				{ "dostup", "True" },
				{ "gruppa", groupId },
				{ "calendar", firstDateFormat },
				{ "calendar2", secondDateFormat },
				{ "Content-Type", "application/x-www-form-urlencoded" },
				{ "ras", "GRUP" }
			};

			var content = new FormUrlEncodedContent(data);

			client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
			client.DefaultRequestHeaders.Add("User-Agent", "Defined");
			client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

			var response = await client.PostAsync("https://www.asiec.ru/ras/ras.php", content);
			response.EnsureSuccessStatusCode();

			string body = await response.Content.ReadAsStringAsync();

			return body;
		}

		public async Task<Schedule> GetSchedule(string group, DateTime firstDate, DateTime secondDate)
		{
			Schedule schedule = new Schedule()
			{
				FirstDate = firstDate,
				LastDate = secondDate,
				Days = new List<Day>()
			};

			HtmlDocument document = new HtmlDocument();
			string body = await GetSheduleBody(group, firstDate, secondDate);

			document.LoadHtml(body);

			HtmlNode tbody = document.DocumentNode.SelectSingleNode("//tbody");

			if (tbody.ChildNodes.Count < 2)
				return schedule;

			bool firstIter = true;
			Day day = new Day();
			Lesson lesson = new Lesson();

			foreach (HtmlNode node in tbody.SelectNodes("//td"))
			{
				bool hasDataLabelAttr = node.Attributes.Contains("data-label");
				string dataLabelValue = hasDataLabelAttr ? node.Attributes["data-label"].Value : "";

				if (node.HasClass("den"))
				{

					string innerText = node.InnerText;
					string[] parts = innerText.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

					string dateString = parts[1];
					DateTime date = DateTime.ParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture);

					if (!firstIter)
					{
						schedule.Days.Add(day);
					}
					firstIter = false;

					day = new Day()
					{
						Date = date,
						Lessons = new List<Lesson>()
					};
				}

				if (node.HasClass("para_b"))
				{
					string innerText = node.InnerText;
					string[] parts = innerText.Split(new char[] { '(', '-', ')' }, StringSplitOptions.RemoveEmptyEntries);

					int number = int.Parse(parts[0].Trim());

					TimeSpan.TryParse(parts[1].Trim(), out TimeSpan startTime);
					TimeSpan.TryParse(parts[2].Trim(), out TimeSpan endTime);

					lesson = new Lesson()
					{
						Number = number,
						StartTime = startTime,
						EndTime = endTime
					};
				}

				if (node.HasClass("group_b"))
				{
					lesson.Group = node.InnerText;
				}

				if (dataLabelValue == "Дисциплина")
				{
					lesson.Name = node.InnerText;
				}

				if (dataLabelValue == "Преподаватель")
				{
					lesson.Teacher = node.InnerText;
				}

				if (node.HasClass("ter_pc"))
				{
					lesson.Territory = node.InnerText;
				}

				if (node.HasClass("aud_pc"))
				{
					lesson.Classroom = node.InnerText;

					day.Lessons.Add(lesson);
				}
			}

			schedule.Days.Add(day);

			return schedule;
		}


	}
}
