using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Комплектовочная ведомость Д3
    /// </summary>
    public class D3 : NMK
    {
        private int? _aggregateTypeId;
        public int? AggregateTypeId
        {
            get => _aggregateTypeId;
            set => SetField(ref _aggregateTypeId, value, nameof(AggregateTypeId));
        }
        public AggregateType AggregateType { get; set; }
        //public ICollection<DrawingNameD3> DrawingNameD3s { get; set; }
        public ICollection<D3Version> D3Versions { get; set; }
    }
}
