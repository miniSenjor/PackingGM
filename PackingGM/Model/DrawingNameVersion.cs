using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Версия чертежного обозначения агрегата
    /// </summary>
    public class DrawingNameVersion : Version
    {
        private int _drawingNameId;
        public int DrawingNameId
        {
            get => _drawingNameId;
            set
            {
                _drawingNameId = value;
                OnPropertyChanged(nameof(DrawingNameId));
            }
        }
        public DrawingName DrawingName { get; set; }
        public ICollection<DrawingNameD3> DrawingNameD3s { get; set; }
    }
}
