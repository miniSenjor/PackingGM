using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Версия комплектовочной ведомости Д3
    /// </summary>
    public class D3Version : Version
    {
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
        public D3 D3 { get; set; }
        public ICollection<GMNumber> GMNumbers { get; set; }
    }
}
