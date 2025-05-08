using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Заводской номер агрегата
    /// </summary>
    public class Aggregate : BaseModel
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
        private string _number;
        public string Number
        {
            get => _number;
            set
            {
                SetField(ref _number, value, nameof(Number));
            }
        }
        private int? _aggregateTypeId;
        public int? AggregateTypeId
        {
            get => _aggregateTypeId;
            set => SetField(ref _aggregateTypeId, value, nameof(AggregateTypeId));
        }
        public AggregateType AggregateType { get; set; }
        public ICollection<OrderAggregate> OrderAggregates { get; set; }
    }
}
