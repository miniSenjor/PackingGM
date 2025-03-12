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
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        private int _drawingNameAggregateId;
        public int DrawingNameAggregateId
        {
            get => _drawingNameAggregateId;
            set
            {
                _drawingNameAggregateId = value;
                OnPropertyChanged(nameof(DrawingNameAggregate));
            }
        }
        public DrawingNameAggregate DrawingNameAggregate { get; set; }
        public ICollection<OrderAggregate> OrderAggregates { get; set; }
    }
}
