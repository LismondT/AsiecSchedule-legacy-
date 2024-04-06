using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ApekSchedule.Data
{
	public enum Theme
	{
		Light,
		Dark
	}

	public class ThemeStyle
	{
        public static Color PageBackgroundColor { get; private set; }
        public static Color PrimaryColor { get; private set; }
        public static Color PrimaryTextColor { get; private set; }
        public static Color SecondaryColor { get; private set; }
        public static Color SecondaryTextColor { get; private set; }

        public static Color NavigationBarBackgroundColor { get; private set; }
        public static Color NavigationBarSelectedTextColor { get; private set; }
        public static Color NavigationBarUnselectedTextColor { get; private set; }

        public static void Load(ResourceDictionary theme)
        {
            foreach(var themeItem in theme)
            {
                string key = themeItem.Key;
                Color value = (Color)themeItem.Value;

				var property = typeof(ThemeStyle).GetProperty(key);
				if (property != null && property.PropertyType == typeof(Color))
				{
					property.SetValue(null, value);
				}
			}
        }
    }
}