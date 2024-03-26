﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ApekSchedule.Data;

namespace ApekSchedule
{


	public partial class App : Application
	{
		static AsiecParser asiecParser;
		public static AsiecParser AsiecParser
		{
			get
			{
				if (asiecParser == null)
					asiecParser = new AsiecParser();
				return asiecParser;
			}
		}


		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}