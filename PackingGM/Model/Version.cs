using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Класс-родитель для версий
    /// </summary>
    public class Version : BaseModel
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
        private protected int _idTCS;
        public virtual int IdTCS
        {
            get => _idTCS;
            set
            {
                _idTCS = value;
                OnPropertyChanged(nameof(IdTCS));
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
        private protected short _state;
        public virtual short State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }
    }
}
