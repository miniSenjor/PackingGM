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
            set => SetField(ref _id, value, nameof(Id));
        }
        private protected string _normalizedText;
        public virtual string NormalizedText
        {
            get => _normalizedText;
            set => SetField(ref _normalizedText, NormalizeText(value), nameof(NormalizedText));
        }
        private protected string _note;
        public virtual string Note
        {
            get => _note;
            set => SetField(ref _note, value, nameof(Note));
        }
        private protected string _name;
        public virtual string Name
        {
            get => _name;
            set => SetField(ref _name, value, nameof(Name));
        }
    }
}
