using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order : BaseModel
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
        private int _number;
        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        private int _contragentId;
        public Nullable<int> ContragentId
        {
            get => _contragentId;
            set
            {
                _contragentId = (int)value;
                OnPropertyChanged(nameof(ContragentId));
            }
        }
        public Contragent Contragent { get; set; }
        public ICollection<OrderAggregate> OrderAggregates { get; set; }
    }
}
