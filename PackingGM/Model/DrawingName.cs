using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Чертёжное обозначение агрегата
    /// </summary>
    public class DrawingName : NMK
    {
        private int? _aggregateTypeId;
        public int? AggregateTypeId
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
        public ICollection<DrawingNameVersion> DrawingNameVersions { get; set; }
    }
}
