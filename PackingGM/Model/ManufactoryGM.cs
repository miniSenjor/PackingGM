using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Связь производство грузовое место
    /// </summary>
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
        private int _gMId;
        public int GMId
        {
            get => _gMId;
            set
            {
                _gMId = value;
                OnPropertyChanged(nameof(GMId));
            }
        }
        public Manufactory Manufactory { get; set; }
        public GM GM { get; set; }
    }
}
