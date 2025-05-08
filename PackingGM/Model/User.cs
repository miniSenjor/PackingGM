using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    public class User : BaseModel
    {
        public User()
        {
            IsAlowedViewing = true;
            IsAlowedWriting = true;
            IsAlowedAdmining = false;
        }
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public User(string login, string password, bool isAlowedViewing = false, bool isAlowedWriting = false, bool isAlowedAdmining = false) : this(login, password)
        {
            try
            {
                if (false == isAlowedViewing && true == isAlowedWriting || false == isAlowedWriting && true == isAlowedAdmining || false == isAlowedViewing && true == isAlowedAdmining)
                    throw new ArgumentException("Не правильно указаны права доступа для пользователя");
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
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value, nameof(Id));
        }
        private string _login;
        //[Column(TypeName = "varchar(100)")]
        public string Login
        {
            get => _login;
            set => SetField(ref _login, value, nameof(Login));
        }
        private string _password;
        //[Column(TypeName = "varchar(100)")]
        public string Password
        {
            get => _password;
            set => SetField(ref _password, value, nameof(Password));
        }
        private bool _isAlowedViewing;
        public bool IsAlowedViewing
        {
            get => _isAlowedViewing;
            set
            {
                if (!value)
                    IsAlowedWriting = value;
                SetField(ref _isAlowedViewing, value, nameof(IsAlowedViewing));
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
                SetField(ref _isAlowedWriting, value, nameof(IsAlowedWriting));
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
                SetField(ref _isAlowedAdmining, value, nameof(IsAlowedAdmining));
            }
        }
        private int? _manufactoryId;
        public int? ManufactoryId
        {
            get => _manufactoryId;
            set
            {
                _manufactoryId = value;
                OnPropertyChanged(nameof(Manufactory));
            }
        }
        //public Role Role { get; set; }
        public Manufactory Manufactory { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<Response> Responses { get; set; }
    }
}
