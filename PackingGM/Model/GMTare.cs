using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    public class GMTare : BaseModel
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value, nameof(Id));
        }
        private int _countGet = 0;
        /// <summary>
        /// Кол-во получено
        /// Тара
        /// </summary>
        public int CountGet
        {
            get => _countGet;
            set
            {
                if (SPUTare != null && value > SPUTare.CountNeed)
                    throw new ArgumentOutOfRangeException("Нельзя задать количество полученой тары больше требуемой");
                _countGet = value;
                OnPropertyChanged(nameof(CountGet));
            }
        }
        /// <summary>
        /// Дефицит
        /// Тара
        /// </summary>
        public int Deficit
        {
            get
            {
                if (SPUTare != null)
                    return SPUTare.CountNeed - CountGet;
                else return 0;
            }
        }
        private string _demind;
        /// <summary>
        /// Требование
        /// Тара
        /// </summary>
        public string Demind
        {
            get => _demind;
            set => SetField(ref _demind, value, nameof(Demind));
        }
        private DateTime? _demindDate;
        /// <summary>
        /// Дата требования
        /// Тара
        /// </summary>
        public DateTime? DemindDate
        {
            get => _demindDate;
            set => SetField(ref _demindDate, value, nameof(DemindDate));
        }
        private DateTime? _promisedProvisionPeriod;
        /// <summary>
        /// Обещанный срок обеспечения СМТС
        /// Тара
        /// </summary>
        public DateTime? PromisedProvisionPeriod
        {
            get => _promisedProvisionPeriod;
            set => SetField(ref _promisedProvisionPeriod, value, nameof(PromisedProvisionPeriod));
        }
        private string _comment;
        /// <summary>
        /// Примечание
        /// Тара
        /// </summary>
        public string Comment
        {
            get => _comment;
            set => SetField(ref _comment, value, nameof(Comment));
        }
        private string _serviceNote;
        /// <summary>
        /// Служебная записка на запуск ППО
        /// Тара
        /// </summary>
        public string ServiceNote
        {
            get => _serviceNote;
            set => SetField(ref _serviceNote, value, nameof(ServiceNote));
        }
        private string _reserveField;
        public string ReserveField
        {
            get => _reserveField;
            set => SetField(ref _reserveField, value, nameof(ReserveField));
        }
        private int? _gMId;
        public int? GMId
        {
            get => _gMId;
            set => SetField(ref _gMId, value, nameof(GMId));
        }
        private int? _sPUTareId;
        public int? SPUTareId
        {
            get => _sPUTareId;
            set => SetField(ref _sPUTareId, value, nameof(SPUTareId));
        }
        public GM GM { get; set; }
        public SPUTare SPUTare { get; set; }
    }
}
