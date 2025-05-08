using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Производство цех
    /// </summary>
    public class Manufactory : BaseModel
    {
        public Manufactory()
        {
            this.Users = new HashSet<User>();
            //Is_alowed_admining = false;
        }
        
        private int _id;
        //[Key]
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
        private string _number;
        //[Column(TypeName = "varchar(100)")]
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public ICollection<User> Users { get; set; }
        public ICollection<GM> GMs { get; set; }
    }
}
