using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Связь СПУ-тара
    /// </summary>
    public class SPUTare : BaseModel
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
        private int _sPUVersionId;
        public int SPUVersionId
        {
            get => _sPUVersionId;
            set
            {
                _sPUVersionId = value;
                OnPropertyChanged(nameof(SPUVersionId));
            }
        }
        private int _tareId;
        public int TareId
        {
            get => _tareId;
            set
            {
                _tareId = value;
                OnPropertyChanged(nameof(TareId));
            }
        }
        private int _countNeed;
        public int CountNeed
        {
            get => _countNeed;
            set
            {
                _countNeed = value;
                OnPropertyChanged(nameof(CountNeed));
            }
        }
        public SPUVersion SPUVersion { get; set; }
        public Tare Tare { get; set; }
        public ICollection<GM> GMs { get; set; }
    }
}
