using PackingGM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.ViewModel
{
    public class ManageGraphViewModel : BaseViewModel
    {
        public ManageGraphViewModel()
        {
            ListGMs = new List<Record>();
            for (int i = 0; i < 30; i++)
            {
                Record r = new Record(i.ToString());
                ListGMs.Add(r);
            }
        }
        public ICollection<Record> ListGMs { get; set; }
    }
    public class Record : BaseModel
    {
        public Record(string s)
        {
            Name = s;
            Description = s;
        }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
    }
}
