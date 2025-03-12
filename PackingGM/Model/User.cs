using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    public class User : BaseModel
    {
        public User() { }
        public User(int roleId)
        {
            Login = roleId.ToString();
            Password = roleId.ToString();
            RoleId = roleId;
        }
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string _login;
        //[Column(TypeName = "varchar(100)")]
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        private string _password;
        //[Column(TypeName = "varchar(100)")]
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private int _roleId;
        public int RoleId
        {
            get => _roleId;
            set
            {
                _roleId = value;
                OnPropertyChanged(nameof(RoleId));
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
        public Role Role { get; set; }
        public Manufactory Manufactory { get; set; }
    }
}
