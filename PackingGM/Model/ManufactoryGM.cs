using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    public class ManufactoryGM : BaseModel
    {
        private int _manufactoryId;
        public int ManyfactoryId
        {
            get => _manufactoryId;
            set
            {
                _manufactoryId = value;
                OnPropertyChanged(nameof(ManyfactoryId));
            }
        }
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
        public Manufactory Manufactory { get; set; }
        public GM GM { get; set; }
        public SPU SPU { get; set; }
    }
}
