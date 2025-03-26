using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.ComponentModel;
using PackingGM.ViewModel;
using System.Windows.Controls;

namespace PackingGM.Model
{
    /// <summary>
    /// Состояние приложения
    /// </summary>
    public class StateApp : BaseModel
    {
        public StateApp(string state = "Готово")
        {
            Text = state;
            Color = (SolidColorBrush)App.Current.Resources["BlueBrush"];
        }
        private static readonly StateApp _instance = new StateApp();
        public static StateApp Instance => _instance;
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        private SolidColorBrush _color;
        public SolidColorBrush Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        public void ChangeAll(string text, string color)
        {
            Text = text;
            ChangeColor(color);
        }
        public void ChangeColor(string color)
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
