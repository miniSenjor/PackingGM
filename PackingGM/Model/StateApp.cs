using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace PackingGM.Model
{
    public class StateApp
    {
        public StateApp(string state = "Готово")
        {
            Text = state;
            Color = (SolidColorBrush)App.Current.Resources["BlueBrush"];
        }
        public static string Text { get; set; }
        public static SolidColorBrush Color { get; set; }
        public static void ChangeAll(string text, string color)
        {
            Text = text;
            ChangeColor(color);
        }
        public static void ChangeColor(string color)
        {
            switch (color.ToLower())
            {
                case "red":
                    Color = (SolidColorBrush)App.Current.Resources["RedBrush"];
                    break;
                case "blue":
                    Color = (SolidColorBrush)App.Current.Resources["BlueBrush"];
                    break;
                case "orange":
                    Color = (SolidColorBrush)App.Current.Resources["OrangeBrush"];
                    break;
                default:
                    Color = (SolidColorBrush)App.Current.Resources["BlueBrush"];
                    break;
            }
        }
    }
}
