using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Номер грузового места
    /// </summary>
    public class GMNumber : BaseModel
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
        private int _d3VersionId;
        public int D3VersionId
        {
            get => _d3VersionId;
            set
            {
                _d3VersionId = value;
                OnPropertyChanged(nameof(D3VersionId));
            }
        }
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
        private string _numberGM;
        public string NumberGM
        {
            get => _numberGM;
            set
            {
                _numberGM = value;
                OnPropertyChanged(nameof(NumberGM));
            }
        }
        public D3Version D3Version { get; set; }
        public SPU SPU { get; set; }
        public ICollection<GM> GMs { get; set; }
    }
}
