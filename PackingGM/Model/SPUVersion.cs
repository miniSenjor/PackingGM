using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Версия СПУ
    /// </summary>
    public class SPUVersion : Version
    {
        private int _sPUId;
        public int SPUId
        {
            get => _sPUId;
            set
            {
                _sPUId = value;
                OnPropertyChanged(nameof(SPUId));
            }
        }
        public SPU SPU { get; set; }
        public ICollection<SPUTare> SPUTares { get; set; }
    }
}
