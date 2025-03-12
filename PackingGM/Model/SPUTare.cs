using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    public class SPUTare : BaseModel
    {
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
        private int _tareId;
        public int TareId
        {
            get => _tareId;
            set
            {
                _tareId = value;
                OnPropertyChanged(nameof(TareId));
            }
        }
        private int _countNeed;
        public int CountNeed
        {
            get => _countNeed;
            set
            {
                _countNeed = value;
                OnPropertyChanged(nameof(CountNeed));
            }
        }
        private int _countGet;
        public int CountGet
        {
            get => _countGet;
            set
            {
                if (value > _countNeed)
                    throw new ArgumentOutOfRangeException("Нельзя задать количество полученой тары больше требуемой");
                _countGet = value;
                OnPropertyChanged(nameof(CountGet));
            }
        }
        public int Deficit
        {
            get => CountNeed - CountGet;
        }
        public SPU SPU { get; set; }
        public Tare Tare { get; set; }
    }
}
