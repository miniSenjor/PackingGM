using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    public class Request : BaseModel
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value, nameof(Id));
        }
        private string _text;
        public string Text
        {
            get => _text;
            set => SetField(ref _text, value, nameof(Text));
        }
        private DateTime? _date;
        public DateTime? Date
        {
            get => _date;
            set => SetField(ref _date, value, nameof(Date));
        }
        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetField(ref _userId, value, nameof(UserId));
        }
        public User User { get; set; }
        public ICollection<Response> Responses { get; set; }
    }
}
