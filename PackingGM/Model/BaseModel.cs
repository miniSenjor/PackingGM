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
        private protected virtual bool SetField<T>(ref T field, T newValue, string publicField)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(publicField);
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Ивент сообщающий об обновлении поля. Используется для обновления Binding
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Метод вызывающий ивент PropertyChanged
        /// </summary>
        /// <param name="propertyName">Имя измененного поля. Лучше НЕ передавать</param>
        protected internal virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

        protected static void OnStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual string NormalizeText(string text)
        {
            var replacementMap = new Dictionary<char, char>
            {
                //{ 'А', 'A' },  // Русская 'А' -> английская 'A'
                //{ 'В', 'B' },  // Русская 'В' -> английская 'B'
                //{ 'Е', 'E' },  // Русская 'Е' -> английская 'E'
                //{ 'З', '3' },  // Русская 'З' -> Цифра '3'
                //{ 'К', 'K' },  // Русская 'К' -> английская 'K'
                //{ 'М', 'M' },  // Русская 'М' -> английская 'M'
                //{ 'Н', 'H' },  // Русская 'Н' -> английская 'H'
                //{ 'О', '0' },  // Русская 'О' -> цифра '0'
                //{ 'O', '0' },  // Английская 'O' -> цифра '0'
                //{ 'Р', 'P' },  // Русская 'Р' -> английская 'P'
                //{ 'С', 'C' },  // Русская 'С' -> английская 'C'
                //{ 'Т', 'T' },  // Русская 'Т' -> английская 'T'
                //{ 'У', 'Y' },  // Русская 'У' -> английская 'Y'
                //{ 'а', 'a' },  // Русская 'а' -> английская 'a'
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
            return new string(text.Trim(' ').ToLower().Select(c => replacementMap.ContainsKey(c) ? replacementMap[c] : c).ToArray());
        }
    }
}
