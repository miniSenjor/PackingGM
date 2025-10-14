using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        private int? _orderAggregate_OrderId;
        public int? OrderAggregate_OrderId
        {
            get => _orderAggregate_OrderId;
            set
            {
                _orderAggregate_OrderId = value;
                OnPropertyChanged(nameof(OrderAggregate_OrderId));
            }
        }
        private int? _orderAggregate_AggregateId;
        public int? OrderAggregate_AggregateId
        {
            get => _orderAggregate_AggregateId;
            set
            {
                _orderAggregate_AggregateId = value;
                OnPropertyChanged(nameof(OrderAggregate_AggregateId));
            }
        }


        private int? _gMNumberId;
        public int? GMNumberId
        {
            get => _gMNumberId;
            set
            {
                _gMNumberId = value;
                OnPropertyChanged(nameof(GMNumberId));
            }
        }
        private int? _manufactoryId;
        public int? ManufactoryId
        {
            get => _manufactoryId;
            set => SetField(ref _manufactoryId, value, nameof(ManufactoryId));
        }
        private string _pr;
        /// <summary>
        /// Признак актуальности
        /// Д3
        /// </summary>
        public string PR
        {
            get => _pr;
            set => SetField(ref _pr, value, nameof(PR));
        }
        private DateTime? _plannedDeadline;
        /// <summary>
        /// Плановый срок сдачи ГМ
        /// Д3 Тара
        /// </summary>
        public DateTime? PlannedDeadline
        {
            get => _plannedDeadline;
            set
            {
                _plannedDeadline = value;
                OnPropertyChanged(nameof(PlannedDeadline));
            }
        }
        /// <summary>
        /// Плановый срок сдачи ГМ (неделя)
        /// Д3
        /// </summary>
        public int PlannedDeadlineWeek
        {
            get
            {
                if (PlannedDeadline == null)
                    return 0;
                else
                {
                    int year = PlannedDeadline.HasValue ? PlannedDeadline.Value.Year : 0;
                    int days = PlannedDeadline.HasValue ? PlannedDeadline.Value.DayOfYear : 0;
                    return (days + (int)(new DateTime(year, 1, 1).DayOfWeek) - 2) / 7 + 1;
                }
            }
        }
        /// <summary>
        /// Необходимый срок обеспечения
        /// Тара
        /// </summary>
        public DateTime? NecessaryProvisionPeriod
        {
            get
            {
                try
                {
                    return PlannedDeadline?.AddDays(-14);
                }
                catch
                {
                    return PlannedDeadline;
                }
            }
        }
        private int? _waybill;
        /// <summary>
        /// Накладная
        /// Д3 Тара
        /// </summary>
        public int? Waybill
        {
            get => _waybill;
            set
            {
                SetField(ref _waybill, value, nameof(Waybill));
            }
        }
        private DateTime? _waybillDate;
        /// <summary>
        /// Дата накладной
        /// Д3 Тара
        /// </summary>
        public DateTime? WaybillDate
        {
            get => _waybillDate;
            set
            {
                SetField(ref _waybillDate, value, nameof(WaybillDate));
            }
        }
        /// <summary>
        /// Факт-неделя накладной
        /// </summary>
        public int FactWeek
        {
            get
            {
                if (WaybillDate == null)
                    return 0;
                else
                {
                    int year = WaybillDate.HasValue ? WaybillDate.Value.Year : 0;
                    int days = WaybillDate.HasValue ? WaybillDate.Value.DayOfYear : 0;
                    return (days + (int)(new DateTime(year, 1, 1).DayOfWeek) - 2) / 7 + 1;
                }
            }
        }
        private string _whyDelay;
        /// <summary>
        /// Причина задержки
        /// Д3
        /// </summary>
        public string WhyDelay
        {
            get => _whyDelay;
            set
            {
                SetField(ref _whyDelay, value, nameof(WhyDelay));
            }
        }

        public GMNumber GMNumber { get; set; }
        public ICollection<GMTare> GMTares { get; set; }
        [ForeignKey("OrderAggregate_OrderId, OrderAggregate_AggregateId")]
        public OrderAggregate OrderAggregate { get; set; }
        public Manufactory Manufactory { get; set; }
    }
}
