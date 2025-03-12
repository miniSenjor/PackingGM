using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Чертёжное обозначение агрегата
    /// </summary>
    public class DrawingNameAggregate : BaseModel
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
        private int _aggregateTypeId;
        public int AggregateTypeId
        {
            get => _aggregateTypeId;
            set
            {
                _aggregateTypeId = value;
                OnPropertyChanged(nameof(AggregateTypeId));
            }
        }
        public AggregateType AggregateType { get; set; }
        public ICollection<Aggregate> Aggregates { get; set; }
        public ICollection<DrawingNameAggregateD3> DrawingNameAggregateD3s { get; set; }
    }
}
