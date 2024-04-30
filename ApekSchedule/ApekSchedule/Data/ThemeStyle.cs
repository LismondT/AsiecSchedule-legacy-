using ApekSchedule.Themes;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ApekSchedule.Data
{
    public class ThemeStyle
    {
        public static List<string> ThemesNames { get; } = new List<string>()
        {
            "Светлая",
            "Тёмная",
            "Винтаж"
        };

        public static ResourceDictionary ThemeDictionary { get; set; }
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
            ThemeDictionary = theme;

            foreach (var themeItem in theme)
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

        public static void SetTheme(ref ICollection<ResourceDictionary> mergedDictionaries, string theme)
        {
            if (mergedDictionaries == null)
                return;

            ResourceDictionary themeDict;

            mergedDictionaries.Clear();

            switch (theme)
            {
                case "Тёмная": themeDict = new DarkTheme(); break;
                case "Винтаж": themeDict = new VintageTheme(); break;
                case "Светлая": themeDict = new LightTheme(); break;

                default:
                    theme = ThemesNames.First();
                    themeDict = new LightTheme();
                    break;
            }

            mergedDictionaries.Add(themeDict);
            Load(mergedDictionaries.FirstOrDefault());

            AppSettings.Theme = theme;
            //Preferences.Set(SettingKeys.Theme, theme);
        }
    }
}