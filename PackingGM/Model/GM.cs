using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Грузовое место (существующее)
    /// </summary>
    public class GM : BaseModel
    {
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
        private int _gMNumberId;
        public int GMNumberId
        {
            get => _gMNumberId;
            set
            {
                _gMNumberId = value;
                OnPropertyChanged(nameof(GMNumberId));
            }
        }
        private int _sPUTareId;
        public int SPUTareId
        {
            get => _sPUTareId;
            set
            {
                _sPUTareId = value;
                OnPropertyChanged(nameof(SPUTareId));
            }
        }
        private int _countGet;
        public int CountGet
        {
            get => _countGet;
            set
            {
                if (value > SPUTare.CountNeed)
                    throw new ArgumentOutOfRangeException("Нельзя задать количество полученой тары больше требуемой");
                _countGet = value;
                OnPropertyChanged(nameof(CountGet));
            }
        }
        public int Deficit
        {
            get => SPUTare.CountNeed - CountGet;
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
        public GMNumber GMNumber { get; set; }
        public SPUTare SPUTare { get; set; }
        public OrderAggregate OrderAggregate { get; set; }
        public ICollection<ManufactoryGM> ManufactoryGMs { get; set; }
    }
}
