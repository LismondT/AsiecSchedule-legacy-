using System;
using System.Linq;
using Xamarin.Essentials;

namespace ApekSchedule.Data
{
    public class AppSettings
    {
        class SettingKeys
        {
            public const string Theme = "Theme";
            public const string RequestId = "RequestId";
            public const string RequestType = "RequestType";
        }

        private static string _theme;
        private static string _requestId;
        private static AsiecData.RequestBy _requestType;

        public static string Theme
        {
            get
            {
                if (_theme == null)
                {
                    try
                    {
                        _theme = Preferences.Get(SettingKeys.Theme, ThemeStyle.ThemesNames.First());
                    }
                    catch (Exception)
                    {
                        string defaultValue = ThemeStyle.ThemesNames.First();
                        Preferences.Set(SettingKeys.Theme, defaultValue);
                        _theme = defaultValue;
                    }
                }

                return _theme;
            }
            set
            {
                Preferences.Set(SettingKeys.Theme, value);
                _theme = value;
            }
        }

        public static string RequestId
        {
            get
            {
                if (_requestId == null)
                {
                    try
                    {
                        _requestId = Preferences.Get(SettingKeys.RequestId, string.Empty);
                    }
                    catch(Exception)
                    {
                        Preferences.Set(SettingKeys.RequestId, string.Empty);
                        _requestId = string.Empty;
                    }
                }

                return _requestId;
            }
            set
            {
                Preferences.Set(SettingKeys.RequestId, value);
                _requestId = value;
            }
        }

        public static AsiecData.RequestBy RequestType
        {
            get
            {
                if (_requestType == AsiecData.RequestBy.None)
                {
                    try
                    {
                        _requestType = (AsiecData.RequestBy)Preferences.Get(SettingKeys.RequestType, (int)AsiecData.RequestBy.None);
                    }
                    catch (Exception)
                    {
                        AsiecData.RequestBy defaultValue = AsiecData.RequestBy.None;
                        Preferences.Set(SettingKeys.RequestType, (int)defaultValue);
                        _requestType = defaultValue;
                    }
                }

                return _requestType;
            }

            set
            {
                Preferences.Set(SettingKeys.RequestType, (int)value);
                _requestType = value;
            }
        }
    }


}
