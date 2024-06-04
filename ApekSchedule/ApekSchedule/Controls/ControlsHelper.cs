using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ApekSchedule.Controls
{
    public class ControlsHelper
    {
        public static void ResizePicker(ref Picker picker)
        {
            double fontSize = picker.FontSize;
            double maxItemSize = 0;
            
            foreach(string item in picker.Items)
            {
                int itemLength = item.Length;
                maxItemSize = itemLength > maxItemSize
                    ? itemLength
                    : maxItemSize;
            }

            picker.WidthRequest = maxItemSize * fontSize;
        }

        public static void ResizeLabel(ref Label label)
        {
            double fontSize = label.FontSize;
            label.MinimumWidthRequest = fontSize * label.Text.Length;
        }
    }
}
