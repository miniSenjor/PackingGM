using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    public class GM : BaseModel
    {
        private int _d3Id;
        public int D3Id
        {
            get => _d3Id;
            set
            {
                _d3Id = value;
                OnPropertyChanged(nameof(D3Id));
            }
        }
        private int _sPUId;
        public int SPUId
        {
            get => _sPUId;
            set
            {
                _sPUId = value;
                OnPropertyChanged(nameof(SPUId));
            }
        }
        private string _numberGM;
        public string NumberGM
        {
            get => _numberGM;
            set
            {
                _numberGM = value;
                OnPropertyChanged(nameof(NumberGM));
            }
        }
        private DateTime _plannedDeadline;
        public DateTime PlannedDeadline
        {
            get => _plannedDeadline;
            set
            {
                _plannedDeadline = value;
                OnPropertyChanged(nameof(PlannedDeadline));
            }
        }
        public DateTime ProvisionPeriod
        {
            get => PlannedDeadline.AddDays(-14);
        }
        public D3 D3 { get; set; }
        public SPU SPU { get; set; }
        public ICollection<ManufactoryGM> ManufactoryGMs { get; set; }
    }
}
