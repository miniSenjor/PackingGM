using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Связующая таблица Заказ-Агрегат
    /// </summary>
    public class OrderAggregate : BaseModel
    {
        private int _orderId;
        public int OrderId
        {
            get => _orderId;
            set
            {
                _orderId = value;
                OnPropertyChanged(nameof(OrderId));
            }
        }
        private int _aggregateId;
        public int AggregateId
        {
            get => _aggregateId;
            set
            {
                _aggregateId = value;
                OnPropertyChanged(nameof(AggregateId));
            }
        }
        public Order Order { get; set; }
        public Aggregate Aggregate { get; set; }
    }
}
