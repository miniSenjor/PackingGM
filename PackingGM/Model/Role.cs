using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    public class Role : BaseModel
    {
        public Role()
        {
            this.Users = new HashSet<User>();
            //Is_alowed_admining = false;
        }
        public Role(int intNameRole)
        {
            Id = intNameRole;
            Name = intNameRole.ToString();
            IsAlowedAdmining = Convert.ToBoolean(intNameRole % 2);
        }
        public Role(string name, bool isAlowedViewing = false, bool isAlowedWriting = false, bool isAlowedAdmining = false)
        {
            try
            {
                if (false == isAlowedViewing && true == isAlowedWriting || false == isAlowedWriting && true == isAlowedAdmining || false == isAlowedViewing && true == isAlowedAdmining)
                    throw new ArgumentException("Не правильно указаны права доступа для роли");
                Name = name;
                IsAlowedViewing = isAlowedViewing;
                IsAlowedWriting = isAlowedWriting;
                IsAlowedAdmining = isAlowedAdmining;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }
        private int _id;
        [Key]
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
                //SetField(ref _id, value);
            }
        }
        private string _name;
        //[Column(TypeName = "varchar(100)")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                //SetField(ref _name, value);
            }
        }
        private bool _isAlowedViewing;
        public bool IsAlowedViewing
        {
            get => _isAlowedViewing;
            set
            {
                if (!value)
                    IsAlowedWriting = value;
                _isAlowedViewing = value;
                OnPropertyChanged(nameof(IsAlowedViewing));
                //SetField(ref _isAlowedViewing, value);
            }
        }
        private bool _isAlowedWriting;
        public bool IsAlowedWriting
        {
            get => _isAlowedWriting;
            set
            {
                if (value)
                    IsAlowedViewing = value;
                else
                    IsAlowedAdmining = value;
                _isAlowedWriting = value;
                OnPropertyChanged(nameof(IsAlowedWriting));
                //SetField(ref _isAlowedWriting, value);
            }
        }
        private bool _isAlowedAdmining;
        public bool IsAlowedAdmining
        {
            get => _isAlowedAdmining;
            set
            {
                if (value)
                    IsAlowedWriting = value;
                _isAlowedAdmining = value;
                OnPropertyChanged(nameof(IsAlowedAdmining));
                //SetField(ref _isAlowedAdmining, value);
            }
        }
        public ICollection<User> Users { get; set; }
    }
}
