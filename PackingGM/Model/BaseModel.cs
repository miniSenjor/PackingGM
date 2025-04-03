using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Базовая модель-родитель для бд
    /// </summary>
    public class BaseModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Задает значение для поля. Доступен только для производных классов сборки
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">Название поля</param>
        /// <param name="newValue">Значение</param>
        private protected virtual void SetField<T>(ref T field, T newValue, string publicField)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(publicField);
            }
        }
        /// <summary>
        /// Ивент сообщающий об обновлении поля. Используется для обновления Binding
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Метод вызывающий ивент PropertyChanged
        /// </summary>
        /// <param name="propertyName">Имя измененного поля. Лучше НЕ передавать</param>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual string NormalizeText(string text)
        {
            var replacementMap = new Dictionary<char, char>
            {
                { 'а', 'a' },  // Русская 'а' -> английская 'a'
                { 'е', 'e' },  // Русская 'е' -> английская 'e'
                { 'о', '0' },  // Русская 'о' -> Цифра '0'
                { 'с', 'c' },  // Русская 'с' -> английская 'c'
                { 'o', '0' },  // английская 'o' -> Цифра '0'
                { 'н', 'h' },  // Русская 'н' -> английская 'h'
                { 'т', 't' },  // Русская 'т' -> английская 't'
                { 'у', 'y' },   // Русская 'у' -> английская 'y'
                { 'р', 'p' },   // Русская 'р' -> английская 'p'
                { 'х', 'x' },   // Русская 'х' -> английская 'x'
                { 'в', 'b' },   // Русская 'в' -> английская 'b'
                { 'м', 'm' },   // Русская 'м' -> английская 'm'
                { 'к', 'k' },   // Русская 'к' -> английская 'k'
                { 'з', '3' }   // Русская 'з' -> цифра '3'
            };
            return new string(text.Select(c => replacementMap.ContainsKey(c) ? replacementMap[c] : c).ToArray());
        }
    }
}
