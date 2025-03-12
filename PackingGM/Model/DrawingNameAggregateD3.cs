using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    public class DrawingNameAggregateD3 : BaseModel
    {
        private int _drawingNameAggregateId;
        public int DrawingNameAggregateId
        {
            get => _drawingNameAggregateId;
            set
            {
                _drawingNameAggregateId = value;
                OnPropertyChanged(nameof(DrawingNameAggregateId));
            }
        }
        private int _d3Id;
        public int D3Id
        {
            get => _d3Id;
            set
            {
                _d3Id = value;
                OnPropertyChanged(nameof(D3Id));
            }
        }
        public DrawingNameAggregate DrawingNameAggregate { get; set; }
        public D3 D3 { get; set; }
    }
}
