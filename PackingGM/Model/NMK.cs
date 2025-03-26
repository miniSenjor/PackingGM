using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Класс-родитель для номенклатуры
    /// </summary>
    public class NMK : BaseModel
    {
        private protected int _id;
        public virtual int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private protected string _normalizedText;
        public virtual string NormalizedText
        {
            get => _normalizedText;
            set
            {
                _normalizedText = NormalizeText(value);
                OnPropertyChanged(nameof(NormalizedText));
            }
        }
        private protected string _note;
        public virtual string Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged(nameof(Note));
            }
        }
        private protected string _name;
        public virtual string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}
