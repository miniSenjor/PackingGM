using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    public class BaseModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Задает значение для поля. Доступен только для производных классов сборки
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">Название поля</param>
        /// <param name="newValue">Значение</param>
        private protected virtual void SetField<T>(ref T field, T newValue)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                //OnPropertyChanged();
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
    }
}
