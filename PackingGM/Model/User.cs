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
            set => SetField(ref _id, value);
        }
        private string _login;
        //[Column(TypeName = "varchar(100)")]
        public string Login
        {
            get => _login;
            set => SetField(ref _login, value);
        }
        private string _password;
        //[Column(TypeName = "varchar(100)")]
        public string Password
        {
            get => _password;
            set => SetField(ref _password, value);
        }
        private int _roleId;
        public int RoleId
        {
            get => _roleId;
            set => SetField(ref _roleId, value);
        }
        private int? _manufactoryId;
        public int? ManufactoryId
        {
            get => _manufactoryId;
            set => SetField(ref _manufactoryId, value);
        }
        public Role Role { get; set; }
        public Manufactory Manufactory { get; set; }
    }
}
